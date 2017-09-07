namespace Gu.Wpf.UiAutomation
{
    public enum OnDispose
    {
        /// <summary>
        /// Kill the process when the <see cref="Application"/> is disposed.
        /// </summary>
        KillProcess,

        /// <summary>
        /// Leave the process running when the <see cref="Application"/> is disposed.
        /// </summary>
        LeaveOpen
    }
}