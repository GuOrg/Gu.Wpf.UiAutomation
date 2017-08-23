namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Gu.Wpf.UiAutomation.AutomationElements;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Logging;
    using Gu.Wpf.UiAutomation.Tools;

    /// <summary>
    /// Wrapper for an application which should be automated.
    /// </summary>
    public class Application : IDisposable
    {
        /// <summary>
        /// The process of this application.
        /// </summary>
        private readonly Process process;
        private bool disposed;

        /// <summary>
        /// Creates an application object with the given process id.
        /// </summary>
        /// <param name="processId">The process id.</param>
        /// <param name="isStoreApp">Flag to define if it's a store app or not.</param>
        public Application(int processId, bool isStoreApp = false)
            : this(FindProcess(processId), isStoreApp)
        {
        }

        /// <summary>
        /// Creates an application object with the given process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <param name="isStoreApp">Flag to define if it's a store app or not.</param>
        public Application(Process process, bool isStoreApp = false)
        {
            this.process = process ?? throw new ArgumentNullException(nameof(process));
            this.IsStoreApp = isStoreApp;
        }

        /// <summary>
        /// Flag to indicate, if the application is a windows store app.
        /// </summary>
        public bool IsStoreApp { get; }

        /// <summary>
        /// The proces Id of the application.
        /// </summary>
        public int ProcessId => this.process.Id;

        /// <summary>
        /// The name of the application's process.
        /// </summary>
        public string Name => this.process.ProcessName;

        /// <summary>
        /// The current handle (Win32) of the application's main window.
        /// Can be IntPtr.Zero if no main window is currently available.
        /// </summary>
        public IntPtr MainWindowHandle => this.process.MainWindowHandle;

        /// <summary>
        /// Gets a value indicating whether the associated process has been terminated.
        /// </summary>
        public bool HasExited => this.process.HasExited;

        /// <summary>
        /// Gets the value that the associated process specified when it terminated.
        /// </summary>
        public int ExitCode => this.process.ExitCode;

        /// <summary>
        /// Closes the application. Force-closes it after a small timeout.
        /// </summary>
        /// <returns>Returns true if the application was closed normally and false if it was force-closed.</returns>
        public bool Close()
        {
            Logger.Default.Debug("Closing application");
            if (this.process.HasExited)
            {
                return true;
            }

            this.process.CloseMainWindow();
            if (this.IsStoreApp)
            {
                return true;
            }

            this.process.WaitForExit(5000);
            var closedNormally = true;
            if (!this.process.HasExited)
            {
                Logger.Default.Info("Application failed to exit, killing process");
                this.process.Kill();
                this.process.WaitForExit(5000);
                closedNormally = false;
            }

            this.process.Close();
            return closedNormally;
        }

        /// <summary>
        /// Kills the applications and waits until it is closed.
        /// </summary>
        public void Kill()
        {
            try
            {
                if (this.process.HasExited)
                {
                    return;
                }

                this.process.Kill();
                this.process.WaitForExit();
            }
            catch
            {
                // NOOP
            }
        }

        /// <summary>
        /// Closes the associated process.
        /// </summary>
        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;
            this.Close();
            this.process.Dispose();
        }

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
        public static Application Attach(Process process)
        {
            Logger.Default.Debug($"[Attaching to process:{process.Id}] [Process name:{process.ProcessName}] [Process full path:{process.MainModule.FileName}]");
            return new Application(process);
        }

        /// <summary>
        /// Attach to a running process
        /// </summary>
        public static Application Attach(string executable, int index = 0)
        {
            var processes = FindProcess(executable);
            if (processes.Length == 0)
            {
                throw new ArgumentException($"Unable to find process with name: {executable}");
            }

            if (processes.Length <= index)
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
            return processes.Length == 0 ? Launch(processStartInfo) : Attach(processes[0]);
        }

        public static Application Launch(string executable)
        {
            var processStartInfo = new ProcessStartInfo(executable);
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
                var error = string.Format(
                    "[Failed Launching process:{0}] [Working directory:{1}] [Process full path:{2}] [Current Directory:{3}]",
                    processStartInfo.FileName,
                    new DirectoryInfo(processStartInfo.WorkingDirectory).FullName,
                    new FileInfo(processStartInfo.FileName).FullName,
                    Environment.CurrentDirectory);
                Logger.Default.Error(error, ex);
                throw;
            }

            return new Application(process);
        }

        public static Application LaunchStoreApp(string appUserModelId, string arguments = null)
        {
            var process = WindowsStoreAppLauncher.Launch(appUserModelId, arguments);
            return new Application(process, true);
        }

        /// <summary>
        /// Waits as long as the application is busy.
        /// </summary>
        /// <param name="waitTimeout">An optional timeout. If null is passed, the timeout is infinite.</param>
        public void WaitWhileBusy(TimeSpan? waitTimeout = null)
        {
            var waitTime = (waitTimeout ?? TimeSpan.FromMilliseconds(-1)).TotalMilliseconds;
            this.process.WaitForInputIdle((int)waitTime);
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
                    this.process.Refresh();
                    return this.process.MainWindowHandle == IntPtr.Zero;
                },
                waitTime,
                TimeSpan.FromMilliseconds(50));
        }

        /// <summary>
        /// Gets the main window of the application's process.
        /// </summary>
        /// <param name="automation">The automation object to use.</param>
        /// <param name="waitTimeout">An optional timeout. If null is passed, the timeout is infinite.</param>
        /// <returns>The main window object as <see cref="Window" /> or null if no main window was found within the timeout.</returns>
        public Window GetMainWindow(AutomationBase automation, TimeSpan? waitTimeout = null)
        {
            this.WaitWhileMainHandleIsMissing(waitTimeout);
            var mainWindowHandle = this.MainWindowHandle;
            if (mainWindowHandle == IntPtr.Zero)
            {
                return null;
            }

            var mainWindow = automation.FromHandle(mainWindowHandle).AsWindow();
            if (mainWindow != null)
            {
                mainWindow.IsMainWindow = true;
            }

            return mainWindow;
        }

        /// <summary>
        /// Gets all top level windows from the application.
        /// </summary>
        public Window[] GetAllTopLevelWindows(AutomationBase automation)
        {
            var desktop = automation.GetDesktop();
            var foundElements = desktop.FindAllChildren(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(this.ProcessId)));
            return foundElements.Select(x => x.AsWindow()).ToArray();
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

        private static Process[] FindProcess(string executable)
        {
            return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(executable));
        }
    }
}
