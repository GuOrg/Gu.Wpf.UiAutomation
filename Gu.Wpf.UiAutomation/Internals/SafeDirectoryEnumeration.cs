namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security;

    internal static class SafeDirectoryEnumeration
    {
        public static IEnumerable<string> EnumerateFilesOpportunistic(string path, string searchPattern, SearchOption searchOption)
        {
            if (searchOption == SearchOption.AllDirectories)
            {
                IEnumerable<string> subdirEntries;
                try
                {
                    subdirEntries = Directory.EnumerateDirectories(path);
                }
                catch (UnauthorizedAccessException)
                {
                    yield break;
                }
                catch (SecurityException)
                {
                    yield break;
                }

                foreach (var subdir in subdirEntries)
                {
                    var en = EnumerateFilesOpportunistic(subdir, searchPattern, searchOption);
                    foreach (var p in en)
                    {
                        yield return p;
                    }
                }
            }

            if (searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly)
            {
                foreach (var p in Directory.EnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly))
                {
                    yield return p;
                }
            }
            else
            {
                throw new ArgumentException("invalid search option", nameof(searchOption));
            }
        }
    }
}
