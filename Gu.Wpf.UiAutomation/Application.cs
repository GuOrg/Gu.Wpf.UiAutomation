namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Gu.Wpf.UiAutomation.Logging;
    using Gu.Wpf.UiAutomation.UIA3;

    /// <summary>
    /// Wrapper for an application which should be automated.
    /// </summary>
    public class Application : IDisposable
    {
        /// <summary>
        /// The process of this application.
        /// </summary>
        private readonly ProcessReference processReference;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// Creates an application object with the given process id.
        /// </summary>
        /// <param name="processId">The process id.</param>
        /// <param name="isStoreApp">Flag to define if it's a store app or not.</param>
        public Application(int processId, bool isStoreApp = false)
            : this(new ProcessReference(FindProcess(processId), dispose: false), isStoreApp)
        {
        }

        /// <summary>
        /// Creates an application object with the given process.
        /// </summary>
        /// <param name="process">The process, NOTE: the process is disposed with this instance.</param>
        /// <param name="isStoreApp">Flag to define if it's a store app or not.</param>
        private Application(ProcessReference process, bool isStoreApp = false)
        {
            this.processReference = process;
            this.IsStoreApp = isStoreApp;
            this.Automation = new UIA3Automation();
        }

        public UIA3Automation Automation { get; }

        /// <summary>
        /// Flag to indicate, if the application is a windows store app.
        /// </summary>
        public bool IsStoreApp { get; }

        /// <summary>
        /// The proces Id of the application.
        /// </summary>
        public int ProcessId => this.processReference.Process.Id;

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
        /// Attach to a running process
        /// </summary>
        /// <param name="processId">The id as shown in task manager</param>
        /// <returns>The application</returns>
        public static Application Attach(int processId)
        {
            return Attach(FindProcess(processId));
        }

        /// <summary>
        /// Attach to a running process
        /// </summary>
        public static Application Attach(Process process, bool dispose = false)
        {
            Logger.Default.Debug($"[Attaching to process:{process.Id}] [Process name:{process.ProcessName}] [Process full path:{process.MainModule.FileName}]");
            return new Application(new ProcessReference(process, dispose));
        }

        /// <summary>
        /// Attach to a running process
        /// </summary>
        public static Application Attach(string executable, int index = 0)
        {
            var processes = FindProcess(executable);
            if (processes.Count == 0)
            {
                throw new ArgumentException($"Unable to find process with name: {executable}");
            }

            if (processes.Count <= index)
            {
                throw new ArgumentException($"Unable to find process with name: {executable} and index: {index}");
            }

            return Attach(processes[index]);
        }

        /// <summary>
        /// Attach to a running process or start a new if not found.
        /// </summary>
        public static Application AttachOrLaunch(ProcessStartInfo processStartInfo)
        {
            var processes = FindProcess(processStartInfo.FileName);
            return processes.Count == 0 ? Launch(processStartInfo) : Attach(processes[0]);
        }

        /// <summary>
        /// Start the application.
        /// </summary>
        /// <param name="executable">The full file name of the executable.</param>
        public static Application Launch(string executable)
        {
            var processStartInfo = new ProcessStartInfo(executable);
            return Launch(processStartInfo);
        }

        /// <summary>
        /// Start the application.
        /// </summary>
        /// <param name="executable">The full file name of the executable.</param>
        /// <param name="args">The start arguments.</param>
        public static Application Launch(string executable, string args)
        {
            var processStartInfo = new ProcessStartInfo(executable) { Arguments = args };
            return Launch(processStartInfo);
        }

        public static Application Launch(ProcessStartInfo processStartInfo)
        {
            if (string.IsNullOrEmpty(processStartInfo.WorkingDirectory))
            {
                processStartInfo.WorkingDirectory = ".";
            }

            Logger.Default.Debug(
                "[Launching process:{0}] [Working directory:{1}] [Process full path:{2}] [Current Directory:{3}]",
                processStartInfo.FileName,
                new DirectoryInfo(processStartInfo.WorkingDirectory).FullName,
                new FileInfo(processStartInfo.FileName).FullName,
                Environment.CurrentDirectory);

            Process process;
            try
            {
                process = Process.Start(processStartInfo);
            }
            catch (Win32Exception ex)
            {
                var error = $"[Failed Launching process:{processStartInfo.FileName}] [Working directory:{new DirectoryInfo(processStartInfo.WorkingDirectory).FullName}] [Process full path:{new FileInfo(processStartInfo.FileName).FullName}] [Current Directory:{Environment.CurrentDirectory}]";
                Logger.Default.Error(error, ex);
                throw;
            }

            return new Application(new ProcessReference(process, dispose: true));
        }

        public static Application LaunchStoreApp(string appUserModelId, string arguments = null)
        {
            return new Application(new ProcessReference(WindowsStoreAppLauncher.Launch(appUserModelId, arguments), dispose: true), isStoreApp: true);
        }

        /// <summary>
        /// Closes the application. Force-closes it after a small timeout.
        /// </summary>
        /// <returns>Returns true if the application was closed normally and false if it was force-closed.</returns>
        public bool Close()
        {
            Logger.Default.Debug("Closing application");
            if (this.disposed ||
                this.processReference.Process.HasExited)
            {
                return true;
            }

            this.processReference.Process.CloseMainWindow();
            if (this.IsStoreApp)
            {
                return true;
            }

            this.processReference.Process.WaitForExit(5000);
            if (!this.processReference.Process.HasExited)
            {
                Logger.Default.Info("Application failed to exit, killing process");
                this.processReference.Process.Kill();
                this.processReference.Process.WaitForExit(5000);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kills the applications and waits until it is closed.
        /// </summary>
        public void Kill()
        {
            try
            {
                if (this.processReference.Process.HasExited)
                {
                    return;
                }

                this.processReference.Process.Kill();
                this.processReference.Process.WaitForExit();
            }
            catch
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
            this.processReference.Process.WaitForInputIdle((int)waitTime);
        }

        /// <summary>
        /// Waits until the main handle is set.
        /// </summary>
        /// <param name="waitTimeout">An optional timeout. If null is passed, the timeout is infinite.</param>
        public void WaitWhileMainHandleIsMissing(TimeSpan? waitTimeout = null)
        {
            var waitTime = waitTimeout ?? TimeSpan.FromMilliseconds(-1);
            Retry.While(
                () =>
                {
                    this.processReference.Process.Refresh();
                    return this.processReference.Process.MainWindowHandle == IntPtr.Zero;
                },
                waitTime,
                TimeSpan.FromMilliseconds(50));
        }

        /// <summary>
        /// Gets the main window of the application's process.
        /// </summary>
        /// <param name="waitTimeout">An optional timeout. If null is passed, the timeout is infinite.</param>
        /// <returns>The main window object as <see cref="Window" /> or null if no main window was found within the timeout.</returns>
        public Window MainWindow(TimeSpan? waitTimeout = null)
        {
            this.ThrowIfDisposed();
            this.WaitWhileMainHandleIsMissing(waitTimeout);
            var mainWindowHandle = this.MainWindowHandle;
            if (mainWindowHandle == IntPtr.Zero)
            {
                return null;
            }

            var mainWindow = this.Automation.FromHandle(mainWindowHandle).AsWindow();
            if (mainWindow != null)
            {
                mainWindow.IsMainWindow = true;
            }

            return mainWindow;
        }

        /// <summary>
        /// Gets all top level windows from the application.
        /// </summary>
        public IReadOnlyList<Window> GetAllTopLevelWindows()
        {
            var desktop = this.Automation.GetDesktop();
            var foundElements = desktop.FindAllChildren(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(this.ProcessId)));
            return foundElements.Select(x => x.AsWindow()).ToArray();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Close();
                this.disposed = true;
                this.processReference.Dispose();
                this.Automation.Dispose();
            }

            this.disposed = true;
        }

        protected void ThrowIfDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
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

        private static IReadOnlyList<Process> FindProcess(string executable)
        {
            return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(executable));
        }

        private sealed class ProcessReference : IDisposable
        {
            private readonly bool dispose;

            public ProcessReference(Process process, bool dispose)
            {
                this.Process = process;
                this.dispose = dispose;
            }

            internal Process Process { get; }

            public void Dispose()
            {
                if (this.dispose)
                {
#pragma warning disable GU0036 // Don't dispose injected.
                    this.Process.Dispose();
#pragma warning restore GU0036 // Don't dispose injected.
                }
            }
        }
    }
}
