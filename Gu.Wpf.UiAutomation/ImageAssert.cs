namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Text;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using Image = System.Drawing.Image;
    using PixelFormat = System.Windows.Media.PixelFormat;
    using Size = System.Windows.Size;

    public delegate void OnImageAssertFail(Bitmap? expected, Bitmap actual, string resource);

    public static partial class ImageAssert
    {
        /// <summary>
        /// The time to retry checking for equality. This compensates for animations etc.
        /// </summary>
        public static TimeSpan RetryTime { get; set; } = TimeSpan.FromMilliseconds(2000);

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The automation element.</param>
        public static void AreEqual(string fileName, UiElement element)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (TryGetStream(fileName, Assembly.GetCallingAssembly(), out var stream))
            {
                using (stream)
                {
                    using var expected = (Bitmap)Image.FromStream(stream);
                    if (Equal(expected, element, RetryTime))
                    {
                        return;
                    }

                    using var actual = Capture.Rectangle(element.Bounds);
                    if (Debugger.IsAttached)
                    {
                        ImageDiffWindow.Show(expected, actual);
                    }

                    throw ImageMatchException(expected, actual, fileName);
                }
            }
            else
            {
                using var actual = Capture.Rectangle(element.Bounds);
                throw MissingResourceException(actual, fileName);
            }
        }

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The automation element.</param>
        /// <param name="onFail">Useful for saving the actual image on error for example.</param>
        public static void AreEqual(string fileName, UiElement element, OnImageAssertFail onFail)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (onFail == null)
            {
                throw new ArgumentNullException(nameof(onFail));
            }

            if (TryGetStream(fileName, Assembly.GetCallingAssembly(), out var stream))
            {
                using (stream)
                {
                    using var expected = (Bitmap)Image.FromStream(stream);
                    if (Equal(expected, element, RetryTime))
                    {
                        return;
                    }

                    using var actual = Capture.Rectangle(element.Bounds);
                    if (Equal(expected, actual))
                    {
                        return;
                    }

                    onFail(expected, actual, fileName);
                    throw ImageMatchException(expected, actual, fileName);
                }
            }
            else
            {
                using var actual = Capture.Rectangle(element.Bounds);
                onFail(null, actual, fileName);
                throw MissingResourceException(actual, fileName);
            }
        }

        /// <summary>
        /// Assert that <paramref name="expected"/> is equal to <paramref name="element"/> by comparing pixels.
        /// </summary>
        /// <param name="expected">The expected <see cref="Bitmap"/>.</param>
        /// <param name="element">The actual <see cref="UiElement"/>.</param>
        public static void AreEqual(Bitmap expected, UiElement element)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (Equal(expected, element, RetryTime))
            {
                return;
            }

            using var actual = Capture.Rectangle(element.Bounds);
            if (Debugger.IsAttached)
            {
                ImageDiffWindow.Show(expected, actual);
            }

            throw ImageMatchException(expected, actual, null);
        }

        /// <summary>
        /// Assert that <paramref name="expected"/> is equal to <paramref name="actual"/> by comparing pixels.
        /// </summary>
        /// <param name="expected">The expected <see cref="Bitmap"/>.</param>
        /// <param name="actual">The actual <see cref="Bitmap"/>.</param>
        public static void AreEqual(Bitmap expected, Bitmap actual)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }

            if (actual == null)
            {
                throw new ArgumentNullException(nameof(actual));
            }

            if (!Equal(expected, actual))
            {
                if (Debugger.IsAttached)
                {
                    ImageDiffWindow.Show(expected, actual);
                }

                throw ImageMatchException(expected, actual, null);
            }
        }

        public static bool Equal(Bitmap expected, UiElement element, TimeSpan retryTime)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, retryTime))
            {
                using var actual = Capture.Rectangle(element.Bounds);
                if (Equal(expected, actual))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Equal(Bitmap expected, Bitmap actual)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }

            if (actual == null)
            {
                throw new ArgumentNullException(nameof(actual));
            }

            if (expected.Size != actual.Size)
            {
                return false;
            }

            if (EqualFast(expected, actual))
            {
                return true;
            }

            for (var x = 0; x < expected.Size.Width; x++)
            {
                for (var y = 0; y < expected.Size.Height; y++)
                {
                    // comparing names here to handle different pixel formats.
                    var ep = expected.GetPixel(x, y);
                    var ap = actual.GetPixel(x, y);
                    if (!ep.Equals(ap) &&
                        ep.Name != ap.Name)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Calls msvcrt.memcmp.
        /// </summary>
        /// <param name="expected">The expected <see cref="Bitmap"/>.</param>
        /// <param name="actual">The actual <see cref="Bitmap"/>.</param>
        /// <returns>True if equal.</returns>
        public static bool EqualFast(Bitmap expected, Bitmap actual)
        {
            // https://stackoverflow.com/a/2038515/1069200
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }

            if (actual == null)
            {
                throw new ArgumentNullException(nameof(actual));
            }

            if (expected.Size != actual.Size ||
                expected.PixelFormat != actual.PixelFormat)
            {
                return false;
            }

            var expectedBits = expected.LockBits(new Rectangle(new Point(0, 0), expected.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var actualBits = actual.LockBits(new Rectangle(new Point(0, 0), actual.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                return Msvcrt.memcmp(
                           expectedBits.Scan0,
                           actualBits.Scan0,
                           new UIntPtr((uint)(expectedBits.Stride * expectedBits.Height))) == 0;
            }
            finally
            {
                expected.UnlockBits(expectedBits);
                actual.UnlockBits(actualBits);
            }
        }

        private static string TempFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
            {
                extension = ".png";
            }

            return Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(fileName) + extension);
        }

        private static BitmapEncoder GetEncoder(string fileName)
        {
            if (string.Equals(Path.GetExtension(fileName), ".png", StringComparison.OrdinalIgnoreCase))
            {
                return new PngBitmapEncoder();
            }

            throw new ArgumentException($"Cannot save {Path.GetExtension(fileName)}");
        }

        private static PixelFormat GetPixelFormat(string fileName)
        {
            if (string.Equals(Path.GetExtension(fileName), ".png", StringComparison.OrdinalIgnoreCase))
            {
                return PixelFormats.Pbgra32;
            }

            if (string.Equals(Path.GetExtension(fileName), ".bmp", StringComparison.OrdinalIgnoreCase))
            {
                return PixelFormats.Bgr24;
            }

            throw new ArgumentException($"Cannot save {Path.GetExtension(fileName)}");
        }

        private static System.Drawing.Imaging.ImageFormat GetImageFormat(string fileName)
        {
            if (!Path.HasExtension(fileName))
            {
                return System.Drawing.Imaging.ImageFormat.Png;
            }

            if (string.Equals(Path.GetExtension(fileName), ".png", StringComparison.OrdinalIgnoreCase))
            {
                return System.Drawing.Imaging.ImageFormat.Png;
            }

            if (string.Equals(Path.GetExtension(fileName), ".bmp", StringComparison.OrdinalIgnoreCase))
            {
                return System.Drawing.Imaging.ImageFormat.Bmp;
            }

            throw new ArgumentException($"Cannot save {Path.GetExtension(fileName)}");
        }

        private static Size Size(this Image bitmap)
        {
            return new Size(bitmap.Width, bitmap.Height);
        }

        private static PixelFormat PixelFormat(this Image bitmap)
        {
            return bitmap.PixelFormat switch
            {
                System.Drawing.Imaging.PixelFormat.Format32bppArgb => PixelFormats.Pbgra32,
                _ => throw new ArgumentOutOfRangeException(nameof(bitmap), bitmap.PixelFormat, "Unhandled pixel format."),
            };
        }

        private static bool TryGetStream(string fileName, Assembly callingAssembly, [NotNullWhen(true)]out Stream? result)
        {
            if (File.Exists(fileName))
            {
                result = File.OpenRead(fileName);
                return true;
            }

            if (!Path.IsPathRooted(fileName))
            {
                var candidate = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                if (File.Exists(candidate))
                {
                    result = File.OpenRead(candidate);
                    return true;
                }

                if (Assembly.GetExecutingAssembly().CodeBase is { } codeBase &&
                    Path.GetDirectoryName(new Uri(codeBase).LocalPath) is { } codeBaseDirectory &&
                    (candidate = Path.Combine(codeBaseDirectory, fileName)) is { } &&
                    File.Exists(candidate))
                {
                    result = File.OpenRead(candidate);
                    return true;
                }

                if (Assembly.GetExecutingAssembly().Location is { } location &&
                    Path.GetDirectoryName(new Uri(location).LocalPath) is { } localDirectory &&
                    (candidate = Path.Combine(localDirectory, fileName)) is { } &&
                    File.Exists(candidate))
                {
                    result = File.OpenRead(candidate);
                    return true;
                }
            }

            foreach (var name in callingAssembly.GetManifestResourceNames())
            {
                if (name.EndsWith(fileName, ignoreCase: true, culture: CultureInfo.InvariantCulture))
                {
                    // analyzer bug
                    result = callingAssembly.GetManifestResourceStream(name)!;
                    return true;
                }
            }

            if (ResourceCache.TryFind(callingAssembly, fileName, out var bitmap))
            {
                result = new MemoryStream();
                bitmap.Save(result, GetImageFormat(fileName));
                return true;
            }

            result = null;
            return false;
        }

        private static ImageAssertException ImageMatchException(Bitmap expected, Bitmap actual, string? fileName)
        {
            var builder = new StringBuilder()
                .AppendLine("Images do not match.")
                .AppendLine($"Expected width: {expected.Width} height: {expected.Height} pixel format: {expected.PixelFormat}")
                .AppendLine($"Actual   width: {actual.Width} height: {actual.Height} pixel format: {expected.PixelFormat}");

            if (expected.Size == actual.Size)
            {
                builder.AppendLine("The following pixels are not matching:")
                       .AppendLine("x    y    Expected Actual");

                for (var x = 0; x < expected.Size.Width; x++)
                {
                    for (var y = 0; y < expected.Size.Height; y++)
                    {
                        // comparing names here to handle different pixel formats.
                        var ep = expected.GetPixel(x, y);
                        var ap = actual.GetPixel(x, y);
                        if (!ep.Equals(ap) &&
                            ep.Name != ap.Name)
                        {
                            builder.AppendLine($"{x,-4} {y,-4} {ep.Name.ToUpperInvariant()} {ap.Name.ToUpperInvariant()}");
                        }
                    }
                }
            }

            return new ImageAssertException(expected, actual, builder.ToString(), fileName);
        }

        private static ImageAssertException MissingResourceException(Bitmap actual, string fileName)
        {
            if (OnFail == OnFail.DoNothing)
            {
                return new ImageAssertException(null, actual, $"Did not find a file nor resource named {fileName}.", fileName);
            }

            actual.Save(TempFileName(fileName), GetImageFormat(fileName));
            return new ImageAssertException(null, actual, $"Did not find a file nor resource named {fileName}.\r\n Saved the element to {TempFileName(fileName)}.", fileName);
        }

        private static class ResourceCache
        {
            private static readonly ConcurrentDictionary<Assembly, List<KeyValuePair<string, Bitmap>>> Cache = new ConcurrentDictionary<Assembly, List<KeyValuePair<string, Bitmap>>>();

            internal static bool TryFind(Assembly assembly, string resourceName, out Bitmap result)
            {
                var map = Cache.GetOrAdd(assembly, CreateMergedDictionary);
                result = map.FirstOrDefault(x => x.Key.EndsWith(resourceName, StringComparison.CurrentCulture)).Value;
                return result != null;
            }

            private static List<KeyValuePair<string, Bitmap>> CreateMergedDictionary(Assembly assembly)
            {
                var map = new List<KeyValuePair<string, Bitmap>>();
                foreach (var name in assembly.GetManifestResourceNames())
                {
                    if (name.EndsWith(".resources", ignoreCase: true, culture: CultureInfo.InvariantCulture))
                    {
                        using var stream = assembly.GetManifestResourceStream(name);
                        using var reader = new ResourceReader(stream ?? throw new InvalidOperationException("Did not find a stream."));
                        foreach (var item in reader)
                        {
                            if (item is DictionaryEntry { Key: string key, Value: Bitmap bitmap })
                            {
                                map.Add(new KeyValuePair<string, Bitmap>(key, bitmap));
                            }
                        }
                    }
                }

                return map;
            }
        }
    }
}
