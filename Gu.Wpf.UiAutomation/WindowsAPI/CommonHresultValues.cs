// ReSharper disable InconsistentNaming
#pragma warning disable
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    public static class CommonHresultValues
    {
        public const long S_OK = 0x00000000; // Operation successful
        public const long E_ABORT = 0x80004004; // Operation aborted
        public const long E_ACCESSDENIED = 0x80070005; // General access denied error
        public const long E_FAIL = 0x80004005; // Unspecified failure
        public const long E_HANDLE = 0x80070006; // Handle that is not valid
        public const long E_INVALIDARG = 0x80070057; // One or more arguments are not valid
        public const long E_NOINTERFACE = 0x80004002; // No such interface supported
        public const long E_NOTIMPL = 0x80004001; // Not implemented
        public const long E_OUTOFMEMORY = 0x8007000E; // Failed to allocate necessary memory
        public const long E_POINTER = 0x80004003; // Pointer that is not valid
        public const long E_UNEXPECTED = 0x8000FFFF; // Unexpected failure
    }
}
#pragma warning restore
