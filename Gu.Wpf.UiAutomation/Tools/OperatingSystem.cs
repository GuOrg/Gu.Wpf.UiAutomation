namespace Gu.Wpf.UiAutomation
{
    using System;
    using Microsoft.Win32;

    public static class OperatingSystem
    {
        private static readonly string CurrentProductName = GetCurrentProductName();

        public static bool CurrentContains(string name)
        {
            return CurrentProductName.Contains(name);
        }

        public static bool IsWindows7()
        {
            return CurrentContains("Windows 7");
        }

        public static bool IsWindows8_1()
        {
            return CurrentContains("Windows 8.1");
        }

        public static bool IsWindows10()
        {
            return CurrentContains("Windows 10");
        }

        public static bool IsWindowsServer2016()
        {
            return CurrentContains("Windows Server 2016");
        }

        private static string GetCurrentProductName()
        {
            using (var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
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
}
