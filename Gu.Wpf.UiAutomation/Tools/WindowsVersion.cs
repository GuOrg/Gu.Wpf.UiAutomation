#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation
{
    using System;
    using Microsoft.Win32;

    /// <summary>
    /// Provides methods for determining current windows version.
    /// </summary>
    public static class WindowsVersion
    {
        /// <summary>
        /// Get the current windows version name from registry.
        /// </summary>
        public static readonly string CurrentVersionProductName = GetCurrentProductName();

        public static bool IsRunningOnCiServer => IsAppVeyor() || IsAzureDevops();

        public static bool CurrentContains(string name) => CurrentVersionProductName.Contains(name);

        /// <summary>
        /// Returns true if running on AppVeyor.
        /// Checks if environment variable APPVEYOR is defined.
        /// </summary>
        /// <returns>True if on AppVeyor.</returns>
        public static bool IsAppVeyor() => Environment.GetEnvironmentVariable("APPVEYOR") != null;

        /// <summary>
        /// Returns true if running on Devops.
        /// Checks if environment variable TF_BUILD is defined.
        /// </summary>
        /// <returns>True if on Devops.</returns>
        public static bool IsAzureDevops() => Environment.GetEnvironmentVariable("TF_BUILD") != null;

        /// <summary>
        /// Check if the installed operating system is Windows 7.
        /// </summary>
        /// <returns>True if Windows 7.</returns>
        public static bool IsWindows7() => CurrentContains("Windows 7");

        /// <summary>
        /// Check if the installed operating system is Windows 8.
        /// </summary>
        /// <returns>True if Windows 8.</returns>
        public static bool IsWindows8() => CurrentContains("Windows 8");

        /// <summary>
        /// Check if the installed operating system is Windows 8.1.
        /// </summary>
        /// <returns>True if Windows 8.1.</returns>
        public static bool IsWindows8_1() => CurrentContains("Windows 8.1");

        /// <summary>
        /// Check if the installed operating system is Windows 10.
        /// </summary>
        /// <returns>True if Windows 10.</returns>
        public static bool IsWindows10() => CurrentContains("Windows 10");

        /// <summary>
        /// Check if the installed operating system is Windows Server 2016.
        /// </summary>
        /// <returns>True if Windows Server 2016.</returns>
        public static bool IsWindowsServer2016() => CurrentContains("Windows Server 2016");

        private static string GetCurrentProductName()
        {
            using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            if (reg == null)
            {
                throw new Exception("Could not find the registry path needed for determining the OS version.");
            }

            var productName = (string)reg.GetValue("ProductName");
            if (productName == null)
            {
                throw new Exception("Could not find the registry key needed for determining the OS version.");
            }

            return productName;
        }
    }
}
