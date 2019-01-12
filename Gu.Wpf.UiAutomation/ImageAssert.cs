namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using Image = System.Drawing.Image;
    using Size = System.Windows.Size;

    public static class ImageAssert
    {
        public static OnFail OnFail { get; set; }

        public static TimeSpan RetryTime { get; set; } = TimeSpan.FromMilliseconds(2000);

        public static TimeSpan StartAnimation { get; set; } = WindowsVersion.IsWindows10() ||
                                                              WindowsVersion.IsWindows8() ||
                                                              WindowsVersion.IsWindows8_1()
            ? TimeSpan.FromMilliseconds(1000)
            : TimeSpan.Zero;

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The UIElement.</param>
        public static void AreEqual(string fileName, System.Windows.UIElement element)
        {
            if (TryGetStream(fileName, Assembly.GetCallingAssembly(), out var stream))
            {
                using (stream)
                {
                    using (var expected = (Bitmap)Image.FromStream(stream))
                    {
                        switch (OnFail)
                        {
                            case OnFail.DoNothing:
                                AreEqual(expected, element);
                                break;
                            case OnFail.SaveImageToTemp:
                                using (var actual = element.ToBitmap(expected.Size(), expected.PixelFormat()))
                                {
                                    AreEqual(expected, actual, (bitmap) => bitmap.Save(TempFileName(fileName), GetImageFormat(fileName)));
                                }

                                break;
                            default:
                                throw new InvalidOperationException($"Not handling OnFail {OnFail}");
                        }
                    }
                }
            }
            else
            {
                using (var actual = element.ToBitmap(element.RenderSize, GetPixelFormat(fileName)))
                {
                    actual.Save(TempFileName(fileName), GetImageFormat(fileName));
                }

                throw AssertException.Create($"Did not find a file nor resource named {fileName}");
            }
        }

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The UIElement.</param>
        /// <param name="onFail">Useful for saving the actual image on error for example.</param>
        public static void AreEqual(string fileName, System.Windows.UIElement element, Action<Exception, Bitmap> onFail)
        {
            if (TryGetStream(fileName, Assembly.GetCallingAssembly(), out var stream))
            {
                using (stream)
                {
                    using (var expected = (Bitmap)Image.FromStream(stream))
                    {
                        using (var actual = element.ToBitmap(expected.Size(), expected.PixelFormat()))
                        {
                            try
                            {
                                AreEqual(expected, actual);
                            }
                            catch (Exception e)
                            {
                                onFail(e, actual);
                                throw;
                            }
                        }
                    }
                }
            }
            else
            {
                var exception = AssertException.Create($"Did not find a file nor resource named {fileName}");
                using (var actual = element.ToBitmap(element.RenderSize, GetPixelFormat(fileName)))
                {
                    onFail(exception, actual);
                }

                throw exception;
            }
        }

        public static void AreEqual(Bitmap expected, System.Windows.UIElement actual)
        {
            using (var actualBmp = actual.ToBitmap(expected.Size(), expected.PixelFormat()))
            {
                if (Equal(expected, actualBmp))
                {
                    return;
                }

                var start = DateTime.Now;
                while (!Retry.IsTimeouted(start, RetryTime))
                {
                    if (Equal(expected, actualBmp))
                    {
                        return;
                    }

                    Wait.For(Retry.PollInterval);
                }

                AreEqual(expected, actualBmp);
            }
        }

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The automation element.</param>
        public static void AreEqual(string fileName, UiElement element)
        {
            WaitForStartAnimation(element);
            if (TryGetStream(fileName, Assembly.GetCallingAssembly(), out var stream))
            {
                using (stream)
                {
                    using (var expected = (Bitmap)Image.FromStream(stream))
                    {
                        if (Equal(expected, element, RetryTime))
                        {
                            return;
                        }

                        using (var actual = Capture.Rectangle(element.Bounds))
                        {
                            switch (OnFail)
                            {
                                case OnFail.DoNothing:
                                    AreEqual(expected, actual);
                                    break;
                                case OnFail.SaveImageToTemp:
                                    AreEqual(
                                        expected,
                                        actual,
                                        (bitmap) => bitmap.Save(TempFileName(fileName), GetImageFormat(fileName)));
                                    break;
                                default:
                                    throw new InvalidOperationException($"Not handling OnFail {OnFail}");
                            }
                        }
                    }
                }
            }
            else
            {
                Capture.ElementToFile(element, TempFileName(fileName));
                throw AssertException.Create($"Did not find a file nor resource named {fileName}");
            }
        }

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The automation element.</param>
        /// <param name="onFail">Useful for saving the actual image on error for example.</param>
        public static void AreEqual(string fileName, UiElement element, Action<Exception, Bitmap> onFail)
        {
            WaitForStartAnimation(element);
            if (TryGetStream(fileName, Assembly.GetCallingAssembly(), out var stream))
            {
                using (stream)
                {
                    using (var expected = (Bitmap)Image.FromStream(stream))
                    {
                        if (Equal(expected, element, RetryTime))
                        {
                            return;
                        }

                        using (var actual = Capture.Rectangle(element.Bounds))
                        {
                            try
                            {
                                AreEqual(expected, actual);
                            }
                            catch (Exception e)
                            {
                                onFail(e, actual);
                                throw;
                            }
                        }
                    }
                }
            }
            else
            {
                var exception = AssertException.Create($"Did not find a file nor resource named {fileName}");
                using (var actual = Capture.Rectangle(element.Bounds))
                {
                    onFail(exception, actual);
                }

                throw exception;
            }
        }

        public static void AreEqual(UiElement expected, UiElement actual)
        {
            WaitForStartAnimation(actual);

            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, RetryTime))
            {
                using (var expectedBmp = Capture.Rectangle(expected.Bounds))
                {
                    using (var actualBmp = Capture.Rectangle(actual.Bounds))
                    {
                        if (Equal(expectedBmp, actualBmp))
                        {
                            return;
                        }
                    }
                }

                Wait.For(Retry.PollInterval);
            }

            using (var expectedBmp = Capture.Rectangle(expected.Bounds))
            {
                using (var actualBmp = Capture.Rectangle(actual.Bounds))
                {
                    AreEqual(expectedBmp, actualBmp);
                }
            }
        }

        public static void AreEqual(Bitmap expected, UiElement element)
        {
            WaitForStartAnimation(element);
            if (Equal(expected, element, RetryTime))
            {
                return;
            }

            using (var actualBmp = Capture.Rectangle(element.Bounds))
            {
                AreEqual(expected, actualBmp);
            }
        }

        public static void AreEqual(Bitmap expected, Bitmap actual)
        {
            if (expected.Size != actual.Size)
            {
                if (Debugger.IsAttached)
                {
                    ImageDiffWindow.Show(expected, actual);
                }

                throw AssertException.Create(
                    "Sizes did not match\r\n" +
                    $"Expected: {expected.Size}\r\n" +
                    $"Actual:   {actual.Size}");
            }

            for (var x = 0; x < expected.Size.Width; x++)
            {
                for (var y = 0; y < expected.Size.Height; y++)
                {
                    if (expected.GetPixel(x, y).Name == actual.GetPixel(x, y).Name)
                    {
                        continue;
                    }

                    if (Debugger.IsAttached)
                    {
                        ImageDiffWindow.Show(expected, actual);
                    }

                    throw AssertException.Create("Images do not match.");
                }
            }
        }

        public static void AreEqual(Bitmap expected, Bitmap actual, Action<Bitmap> onFail)
        {
            if (expected.Size != actual.Size)
            {
                if (Debugger.IsAttached)
                {
                    ImageDiffWindow.Show(expected, actual);
                }

                onFail(actual);
                throw AssertException.Create(
                    "Sizes did not match\r\n" +
                    $"Expected: {expected.Size}\r\n" +
                    $"Actual:   {actual.Size}");
            }

            for (var x = 0; x < expected.Size.Width; x++)
            {
                for (var y = 0; y < expected.Size.Height; y++)
                {
                    if (expected.GetPixel(x, y).Name == actual.GetPixel(x, y).Name)
                    {
                        continue;
                    }

                    if (Debugger.IsAttached)
                    {
                        ImageDiffWindow.Show(expected, actual);
                    }

                    onFail(actual);
                    throw AssertException.Create("Images do not match.");
                }
            }
        }

        public static bool Equal(Bitmap expected, UiElement element, TimeSpan retryTime)
        {
            var start = DateTime.Now;
            while (!Retry.IsTimeouted(start, retryTime))
            {
                using (var actual = Capture.Rectangle(element.Bounds))
                {
                    if (Equal(expected, actual))
                    {
                        return true;
                    }
                }

                Wait.For(Retry.PollInterval);
            }

            return false;
        }

        public static bool Equal(Bitmap expected, Bitmap actual)
        {
            if (expected.Size != actual.Size)
            {
                return false;
            }

            for (var x = 0; x < expected.Size.Width; x++)
            {
                for (var y = 0; y < expected.Size.Height; y++)
                {
                    if (expected.GetPixel(x, y).Name != actual.GetPixel(x, y).Name)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static Bitmap ToBitmap(this System.Windows.UIElement element, Size size, PixelFormat pixelFormat)
        {
            return element.ToRenderTargetBitmap(size, pixelFormat)
                          .ToBitmap();
        }

        public static Bitmap ToBitmap(this System.Windows.UIElement element, Size size)
        {
            return element.ToRenderTargetBitmap(size, PixelFormats.Pbgra32)
                          .ToBitmap();
        }

        public static RenderTargetBitmap ToRenderTargetBitmap(this System.Windows.UIElement element, Size size)
        {
            return element.ToRenderTargetBitmap(size, PixelFormats.Pbgra32);
        }

        public static RenderTargetBitmap ToRenderTargetBitmap(this System.Windows.UIElement element, Size size, PixelFormat pixelFormat)
        {
            var result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, pixelFormat);
            element.Measure(size);
            element.Arrange(new System.Windows.Rect(size));
            result.Render(element);
            return result;
        }

        public static Bitmap ToBitmap(this RenderTargetBitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
                return new Bitmap(stream);
            }
        }

        public static void SaveImage(this System.Windows.UIElement element, Size size, string fileName)
        {
            SaveImage(element, size, GetPixelFormat(fileName), fileName);
        }

        public static void SaveImage(this System.Windows.UIElement element, Size size, PixelFormat pixelFormat, string fileName)
        {
            using (var stream = File.OpenWrite(fileName))
            {
                element.Measure(size);
                element.Arrange(new System.Windows.Rect(size));
                var renderTargetBitmap = element.ToRenderTargetBitmap(size, pixelFormat);
                var encoder = GetEncoder(fileName);
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                encoder.Save(stream);
            }
        }

        private static void WaitForStartAnimation(UiElement element)
        {
            if (StartAnimation <= TimeSpan.Zero)
            {
                return;
            }

            var window = element.Window;
            _ = User32.GetWindowThreadProcessId(window.NativeWindowHandle, out var id);
            using (var process = Process.GetProcessById((int)id))
            {
                var upTime = DateTime.Now - process.StartTime;
                if (upTime < StartAnimation)
                {
                    Wait.For(StartAnimation - upTime);
                }
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
            switch (bitmap.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    return PixelFormats.Pbgra32;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bitmap), bitmap.PixelFormat, "Unhandled pixel format.");
            }
        }

        private static bool TryGetStream(string fileName, Assembly callingAssembly, out Stream result)
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

                // ReSharper disable once AssignNullToNotNullAttribute
                candidate = Path.Combine(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                    fileName);

                if (File.Exists(candidate))
                {
                    result = File.OpenRead(candidate);
                    return true;
                }

                //// ReSharper disable once AssignNullToNotNullAttribute
                candidate = Path.Combine(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath),
                    fileName);

                if (File.Exists(candidate))
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
                    result = callingAssembly.GetManifestResourceStream(name);
                    return true;
                }
            }

            if (ResourceChache.TryFind(callingAssembly, fileName, out var bitmap))
            {
                result = new MemoryStream();
                bitmap.Save(result, GetImageFormat(fileName));
                return true;
            }

            result = null;
            return false;
        }

        private static class ResourceChache
        {
            private static readonly ConcurrentDictionary<Assembly, List<KeyValuePair<string, Bitmap>>> Cache = new ConcurrentDictionary<Assembly, List<KeyValuePair<string, Bitmap>>>();

            public static bool TryFind(Assembly assembly, string resourceName, out Bitmap result)
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
                        using (var stream = assembly.GetManifestResourceStream(name))
                        {
                            using (var reader = new ResourceReader(stream ?? throw new InvalidOperationException("Did not find a stream.")))
                            {
                                foreach (var item in reader)
                                {
                                    if (item is DictionaryEntry entry &&
                                        entry.Key is string key &&
                                        entry.Value is Bitmap bitmap)
                                    {
                                        map.Add(new KeyValuePair<string, Bitmap>(key, bitmap));
                                    }
                                }
                            }
                        }
                    }
                }

                return map;
            }
        }
    }
}
