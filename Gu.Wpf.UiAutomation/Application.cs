namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Automation;

    using Gu.Wpf.UiAutomation.WindowsAPI;

    /// <inheritdoc />
    /// <summary>
    /// Wrapper for an application which should be automated.
    /// </summary>
    public sealed class Application : IDisposable
    {
        private static readonly List<Process> Launched = new();
        private static readonly ConcurrentDictionary<string, string?> ExeNames = new();

        private readonly ProcessReference processReference;
        private readonly object gate = new();

        private volatile Window? mainWindow;
        private bool disposed;

        /// <summary>
        /// Creates an application object with the given process.
        /// </summary>
        /// <param name="process">The process, NOTE: the process is disposed with this instance.</param>
        /// <param name="isStoreApp">Flag to define if it's a store app or not.</param>
        private Application(ProcessReference process, bool isStoreApp = false)
        {
            this.processReference = process;
            this.IsStoreApp = isStoreApp;
        }

        /// <summary>
        /// Flag to indicate, if the application is a windows store app.
        /// </summary>
        public bool IsStoreApp { get; }

#pragma warning disable CA1721 // Property names should not match get methods
        /// <summary>
        /// Calls <see cref="GetMainWindow"/> with a timeout of ten seconds.
        /// </summary>
        public Window MainWindow => this.GetMainWindow(TimeSpan.FromSeconds(WindowsVersion.IsRunningOnCiServer ? 200 : 20));
#pragma warning restore CA1721 // Property names should not match get methods

        /// <summary>
        /// The process Id of the application.
        /// </summary>
        public int ProcessId => this.processReference.Process.Id;

        /// <summary>
        /// Process.StartTime.
        /// </summary>
        public DateTime StartTime => this.processReference.Process.StartTime;

        /// <summary>
        /// The name of the application's process.
        /// </summary>
        public string Name => this.processReference.Process.ProcessName;

        /// <summary>
        /// The current handle (Win32) of the application's main window.
        /// Can be IntPtr.Zero if no main window is currently available.
        /// </summary>
        public IntPtr MainWindowHandle => this.processReference.Process.MainWindowHandle;

        /// <summary>
        /// Gets a value indicating whether the associated process has been terminated.
        /// </summary>
        public bool HasExited => this.processReference.Process.HasExited;

        /// <summary>
        /// Gets the value that the associated process specified when it terminated.
        /// </summary>
        public int ExitCode => this.processReference.Process.ExitCode;

        /// <summary>
        /// File name without path.
        /// Searches:
        /// 1. Current directory
        /// 2. First directory containing a sln file. Then finds newest file matching <paramref name="exeFileName"/> in sub-directories.
        ///  </summary>
        public static string FindExe(string exeFileName)
        {
            return FindExeOrDefault(exeFileName, null) ?? throw new ArgumentException($"Did not find a file named {exeFileName}, try building?");
        }

        /// <summary>
        /// Attach to a running process.
        /// </summary>
        /// <param name="processId">The id as shown in task manager.</param>
        /// <returns>The application.</returns>
        public static Application Attach(int processId)
        {
            return Attach(FindProcess(processId));
        }

        /// <summary>
        /// Attach to a running process.
        /// </summary>
        public static Application Attach(string exeFileName, int index = 0)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            var processes = FindProcess(exeFileName);
            if (processes.Count == 0)
            {
                throw new ArgumentException($"Unable to find process with name: {exeFileName}");
            }

            if (processes.Count <= index)
            {
                throw new ArgumentException($"Unable to find process with name: {exeFileName} and index: {index}");
            }

            return Attach(processes[index]);
        }

        /// <summary>
        /// Attach to a running process.
        /// </summary>
        public static Application Attach(Process process, OnDispose dispose = OnDispose.LeaveOpen)
        {
            //// Logger.Default.Debug($"[Attaching to process:{process.Id}] [Process name:{process.ProcessName}] [Process full path:{process.MainModule.FileName}]");
            var app = new Application(new ProcessReference(process, dispose));
            var windowHandle = app.MainWindow.NativeWindowHandle;
            if (windowHandle != new IntPtr(0))
            {
                _ = User32.SetForegroundWindow(windowHandle);
                _ = Wait.UntilResponsive(app.MainWindow);
            }

            return app;
        }

        /// <summary>
        /// Attach to a running process if found.
        /// </summary>
        public static bool TryAttach(string exeFileName, [NotNullWhen(true)] out Application? result)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            return TryAttach(new ProcessStartInfo(exeFileName), OnDispose.LeaveOpen, out result);
        }

        /// <summary>
        /// Attach to a running process if found.
        /// </summary>
        public static bool TryAttach(string exeFileName, OnDispose onDispose, [NotNullWhen(true)] out Application? result)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            return TryAttach(new ProcessStartInfo(exeFileName), onDispose, out result);
        }

        /// <summary>
        /// Attach to a running process if found.
        /// </summary>
        public static bool TryAttach(string exeFileName, string args, [NotNullWhen(true)] out Application? result)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            return TryAttach(new ProcessStartInfo(exeFileName) { Arguments = args }, OnDispose.LeaveOpen, out result);
        }

        /// <summary>
        /// Attach to a running process if found.
        /// </summary>
        public static bool TryAttach(string exeFileName, string args, OnDispose onDispose, [NotNullWhen(true)] out Application? result)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            return TryAttach(new ProcessStartInfo(exeFileName) { Arguments = args }, onDispose, out result);
        }

        /// <summary>
        /// Attach to a running process if found.
        /// </summary>
        public static bool TryAttach(ProcessStartInfo processStartInfo, [NotNullWhen(true)] out Application? result)
        {
            return TryAttach(processStartInfo, OnDispose.LeaveOpen, out result);
        }

        /// <summary>
        /// Attach to a running process if found.
        /// </summary>
        public static bool TryAttach(ProcessStartInfo processStartInfo, OnDispose onDispose, [NotNullWhen(true)] out Application? result)
        {
            if (processStartInfo is null)
            {
                throw new ArgumentNullException(nameof(processStartInfo));
            }

            var exeFileName = Path.GetFullPath(processStartInfo.FileName);
            lock (Launched)
            {
                var launched = Launched.FirstOrDefault(x => Path.GetFullPath(x.StartInfo.FileName) == exeFileName &&
                                                            x.StartInfo.Arguments == processStartInfo.Arguments);
                if (launched is { })
                {
                    result = Attach(launched, onDispose);
                    return true;
                }
            }

            var running = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(exeFileName))
                                 .FirstOrDefault(x =>
                                 {
                                     try
                                     {
                                         return x.StartInfo.Arguments == processStartInfo.Arguments;
                                     }
                                     catch (InvalidOperationException)
                                     {
                                         // This can happen for processes that we didn't start
                                         return false;
                                     }
                                 });

            if (running is { })
            {
                result = Attach(running, onDispose);
                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Attach to a running process if found and perform <paramref name="onAttached"/>.
        /// </summary>
        public static bool TryWithAttached(string exeFileName, Action<Application> onAttached)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            return TryWithAttached(new ProcessStartInfo(exeFileName), onAttached);
        }

        /// <summary>
        /// Attach to a running process if found and perform <paramref name="onAttached"/>.
        /// </summary>
        public static bool TryWithAttached(string exeFileName, string args, Action<Application> onAttached)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            return TryWithAttached(new ProcessStartInfo(exeFileName) { Arguments = args }, onAttached);
        }

        /// <summary>
        /// Attach to a running process if found and perform <paramref name="onAttached"/>.
        /// </summary>
        public static bool TryWithAttached(ProcessStartInfo processStartInfo, Action<Application> onAttached)
        {
            if (processStartInfo is null)
            {
                throw new ArgumentNullException(nameof(processStartInfo));
            }

            if (onAttached is null)
            {
                throw new ArgumentNullException(nameof(onAttached));
            }

#pragma warning disable IDISP001 // Dispose created.
            if (TryAttach(processStartInfo, OnDispose.LeaveOpen, out var app))
#pragma warning restore IDISP001 // Dispose created.
            {
                onAttached(app);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Attach to a running process or start a new if not found.
        /// </summary>
        public static Application AttachOrLaunch(string exeFileName, OnDispose onDispose = OnDispose.LeaveOpen)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            return AttachOrLaunch(new ProcessStartInfo(exeFileName), onDispose);
        }

        /// <summary>
        /// Attach to a running process or start a new if not found.
        /// </summary>
        public static Application AttachOrLaunch(string exeFileName, string args, OnDispose onDispose = OnDispose.LeaveOpen)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            return AttachOrLaunch(new ProcessStartInfo(exeFileName) { Arguments = args }, onDispose);
        }

        /// <summary>
        /// Attach to a running process or start a new if not found.
        /// </summary>
        public static Application AttachOrLaunch(ProcessStartInfo processStartInfo, OnDispose onDispose = OnDispose.LeaveOpen)
        {
            if (TryAttach(processStartInfo, onDispose, out var app))
            {
                return app;
            }

            return Launch(processStartInfo, onDispose);
        }

        /// <summary>
        /// Start the application.
        /// </summary>
        /// <param name="exeFileName">The full file name of the exeFileName.</param>
        /// <param name="onDispose">Specify if the app should be left running when this instance is disposed.</param>
        public static Application Launch(string exeFileName, OnDispose onDispose = OnDispose.KillProcess)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            var processStartInfo = new ProcessStartInfo(exeFileName);
            return Launch(processStartInfo, onDispose);
        }

        /// <summary>
        /// Start the application.
        /// </summary>
        /// <param name="exeFileName">The full file name of the exeFileName.</param>
        /// <param name="args">The start arguments.</param>
        /// <param name="onDispose">Specify if the app should be left running when this instance is disposed.</param>
        public static Application Launch(string exeFileName, string args, OnDispose onDispose = OnDispose.KillProcess)
        {
            exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
            var processStartInfo = new ProcessStartInfo(exeFileName) { Arguments = args };
            return Launch(processStartInfo, onDispose);
        }

        public static Application Launch(ProcessStartInfo processStartInfo, OnDispose onDispose = OnDispose.KillProcess)
        {
            if (processStartInfo is null)
            {
                throw new ArgumentNullException(nameof(processStartInfo));
            }

            if (string.IsNullOrEmpty(processStartInfo.WorkingDirectory))
            {
                processStartInfo.WorkingDirectory = ".";
            }

            var app = new Application(new ProcessReference(Process.Start(processStartInfo), onDispose));
            if (onDispose == OnDispose.LeaveOpen)
            {
                lock (Launched)
                {
                    Launched.Add(app.processReference.Process);
                }
            }

            return app;
        }

        public static Application LaunchStoreApp(string appUserModelId, string? arguments = null, OnDispose onDispose = OnDispose.KillProcess)
        {
            if (appUserModelId is null)
            {
                throw new ArgumentNullException(nameof(appUserModelId));
            }

#pragma warning disable CS8604 // Possible null reference argument.
            var app = new Application(new ProcessReference(WindowsStoreAppLauncher.Launch(appUserModelId, arguments), onDispose), isStoreApp: true);
#pragma warning restore CS8604 // Possible null reference argument.
            if (onDispose == OnDispose.LeaveOpen)
            {
                lock (Launched)
                {
                    Launched.Add(app.processReference.Process);
                }
            }

            return app;
        }

        /// <summary>
        /// Waits until the main handle is set.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <param name="waitTimeout">An optional timeout. If null is passed, the timeout is infinite.</param>
        public static void WaitForMainWindow(Process process, TimeSpan? waitTimeout = null)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            var start = DateTime.Now;
            do
            {
                if (process.HasExited)
                {
                    throw new InvalidOperationException("Process has exited.");
                }

                process.Refresh();
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    return;
                }

                Wait.For(Retry.PollInterval);
            }
            while (!Retry.IsTimeouted(start, waitTimeout ?? TimeSpan.MaxValue));
            throw new TimeoutException("Did not find Process.MainWindowHandle, if startup is slow try with a longer timeout.");
        }

        /// <summary>
        /// Kill any launched processes.
        /// </summary>
        public static void KillLaunched()
        {
            lock (Launched)
            {
                foreach (var process in Launched)
                {
                    process.Kill();
                    process.Dispose();
                }

                Launched.Clear();
            }
        }

        /// <summary>
        /// Kill any launched processes.
        /// </summary>
        /// <param name="exeFileName">The full file name of the exeFileName.</param>
        public static void KillLaunched(string exeFileName)
        {
            lock (Launched)
            {
                exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
                exeFileName = Path.GetFullPath(exeFileName);
                var launched = Launched.Where(x => exeFileName == Path.GetFullPath(x.StartInfo.FileName)).ToArray();
                foreach (var process in launched)
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                        _ = process.WaitForExit(1000);
                    }

                    Launched.Remove(process);
                    process.Dispose();
                }
            }
        }

        /// <summary>
        /// Kill any launched processes.
        /// </summary>
        /// <param name="exeFileName">The full file name of the exeFileName.</param>
        /// <param name="args">Startup arguments.</param>
        public static void KillLaunched(string exeFileName, string args)
        {
            lock (Launched)
            {
                exeFileName = FindExeOrDefault(exeFileName, exeFileName)!;
                exeFileName = Path.GetFullPath(exeFileName);
                var launched = Launched.Where(x => exeFileName == Path.GetFullPath(x.StartInfo.FileName) &&
                                                   x.StartInfo.Arguments == args).ToArray();
                foreach (var process in launched)
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                        _ = process.WaitForExit(1000);
                    }

                    Launched.Remove(process);
                    process.Dispose();
                }
            }
        }

        /// <summary>
        /// Closes the application. Force-closes it after a small timeout.
        /// </summary>
        /// <returns>Returns true if the application was closed normally and false if it was force-closed.</returns>
        public bool Close()
        {
            lock (Launched)
            {
                Launched.Remove(this.processReference.Process);
            }

            if (this.disposed ||
                this.processReference.Process.HasExited)
            {
                return true;
            }

            _ = this.processReference.Process.CloseMainWindow();
            if (this.IsStoreApp)
            {
                return true;
            }

            _ = this.processReference.Process.WaitForExit(5000);
            if (!this.processReference.Process.HasExited)
            {
                this.processReference.Process.Kill();
                _ = this.processReference.Process.WaitForExit(5000);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kills the applications and waits until it is closed.
        /// </summary>
        public void Kill()
        {
            lock (Launched)
            {
                Launched.Remove(this.processReference.Process);
            }

            try
            {
                if (this.processReference.Process.HasExited)
                {
                    return;
                }

                this.processReference.Process.Kill();
                this.processReference.Process.WaitForExit();
            }
#pragma warning disable CA1031 // Do not catch general exception types, not sure what exceptions can happen here. Not very important.
            catch
#pragma warning restore CA1031 // Do not catch general exception types
            {
                // NOOP
            }
        }

        /// <summary>
        /// Waits as long as the application is busy.
        /// </summary>
        /// <param name="waitTimeout">An optional timeout. If null is passed, the timeout is infinite.</param>
        public void WaitWhileBusy(TimeSpan? waitTimeout = null)
        {
            this.ThrowIfDisposed();
            var waitTime = (waitTimeout ?? TimeSpan.FromMilliseconds(-1)).TotalMilliseconds;
            _ = this.processReference.Process.WaitForInputIdle((int)waitTime);
        }

        /// <summary>
        /// Waits until the main handle is set.
        /// </summary>
        /// <param name="waitTimeout">An optional timeout. If null is passed, the timeout is infinite.</param>
        public void WaitForMainWindow(TimeSpan? waitTimeout = null)
        {
            WaitForMainWindow(this.processReference.Process, waitTimeout);
        }

        /// <summary>
        /// Gets the main window of the application's process.
        /// </summary>
        /// <param name="waitTimeout">An optional timeout. If null is passed, the timeout is infinite.</param>
        /// <returns>The main window object as <see cref="Window" /> or null if no main window was found within the timeout.</returns>
        public Window GetMainWindow(TimeSpan? waitTimeout = null)
        {
            this.ThrowIfDisposed();
            if (this.mainWindow is { })
            {
                return this.mainWindow;
            }

            var start = DateTime.Now;
            this.WaitForMainWindow(waitTimeout);
            if (this.mainWindow is { })
            {
                return this.mainWindow;
            }

            lock (this.gate)
            {
                if (this.mainWindow is { })
                {
                    return this.mainWindow;
                }

                var mainWindowHandle = this.MainWindowHandle;
                if (mainWindowHandle == IntPtr.Zero)
                {
                    throw new InvalidOperationException($"Did not find main window for {this.processReference.Process.ProcessName}. If startup is slow try with a longer wait.");
                }

                if (waitTimeout is { })
                {
                    waitTimeout = waitTimeout - (DateTime.Now - start);
                }

                _ = Wait.UntilResponsive(mainWindowHandle, waitTimeout ?? TimeSpan.MaxValue);
                return this.mainWindow = new Window(AutomationElement.FromHandle(mainWindowHandle), isMainWindow: true);
            }
        }

        /// <summary>
        /// Gets all top level windows from the application.
        /// </summary>
        public IReadOnlyList<Window> GetAllTopLevelWindows()
        {
            var desktop = AutomationElement.RootElement;
            var foundElements = desktop.FindAllChildren(
                new AndCondition(
                    Conditions.Window,
                    Conditions.ByProcessId(this.ProcessId)));
            return foundElements.OfType<AutomationElement>().Select((x, i) => new Window(x, i == 0)).ToArray();
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            if (this.processReference.OnDispose == OnDispose.KillProcess)
            {
                _ = this.Close();
                this.processReference.Dispose();
                Launched.Remove(this.processReference.Process);
            }

            this.disposed = true;
        }

        private static Process FindProcess(int processId)
        {
            try
            {
                var process = Process.GetProcessById(processId);
                return process;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find process with id: " + processId, ex);
            }
        }

        private static IReadOnlyList<Process> FindProcess(string exeFileName)
        {
            return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(exeFileName));
        }

        private static string? FindExeOrDefault(string exeFileName, string? @default)
        {
            return ExeNames.GetOrAdd(exeFileName, Create) ?? @default;

            static string? Create(string fileName)
            {
                if (Path.GetExtension(fileName) != ".exe")
                {
                    return null;
                }

                if (Path.IsPathRooted(fileName))
                {
                    return fileName;
                }

                var match = SafeDirectoryEnumeration.EnumerateFilesOpportunistic(Directory.GetCurrentDirectory(), fileName, SearchOption.AllDirectories)
                                                    .FirstOrDefault();
                if (match is { })
                {
                    return match;
                }

                if (Assembly.GetExecutingAssembly().CodeBase is { } codeBase)
                {
                    var dir = new DirectoryInfo(Path.GetDirectoryName(new Uri(codeBase).LocalPath));
                    while (dir?.Parent is { })
                    {
                        if (dir.EnumerateFiles("*.sln", SearchOption.TopDirectoryOnly).Any())
                        {
                            var files = dir.EnumerateFiles(fileName, SearchOption.AllDirectories).ToArray();
                            if (files.Length == 0)
                            {
                                return null;
                            }

                            var latest = files[0];
                            foreach (var file in files)
                            {
                                if (file.LastWriteTime > latest.LastWriteTime)
                                {
                                    latest = file;
                                }
                            }

                            return latest.FullName;
                        }

                        dir = dir.Parent;
                    }
                }

                return null;
            }
        }

        private void ThrowIfDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(typeof(Application).FullName);
            }
        }

        private sealed class ProcessReference : IDisposable
        {
            internal ProcessReference(Process process, OnDispose onDispose)
            {
                this.Process = process;
                this.OnDispose = onDispose;
            }

            internal Process Process { get; }

            internal OnDispose OnDispose { get; private set; }

            public void Dispose()
            {
                if (this.OnDispose == OnDispose.KillProcess)
                {
                    this.OnDispose = OnDispose.LeaveOpen;
#pragma warning disable IDISP007 // Don't dispose injected.
                    this.Process.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected.
                }
            }
        }
    }
}
