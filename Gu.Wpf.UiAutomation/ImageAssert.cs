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
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Image = System.Drawing.Image;
    using Size = System.Windows.Size;

    public static class ImageAssert
    {
        public static OnFail OnFail { get; set; }

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The UIElement.</param>
        public static void AreEqual(string fileName, UIElement element)
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
                                    AreEqual(expected, actual, (bitmap) => bitmap.Save(TempFileName(fileName), System.Drawing.Imaging.ImageFormat.Png));
                                }

                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
            else
            {
                using (var actual = element.ToBitmap(element.RenderSize, GetPixelFormat(fileName)))
                {
                    actual.Save(TempFileName(fileName), System.Drawing.Imaging.ImageFormat.Png);
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
        public static void AreEqual(string fileName, UIElement element, Action<Exception, Bitmap> onFail)
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

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The automation element.</param>
        public static void AreEqual(string fileName, AutomationElement element)
        {
            if (TryGetStream(fileName, Assembly.GetCallingAssembly(), out var stream))
            {
                using (stream)
                {
                    using (var expected = (Bitmap)Image.FromStream(stream))
                    {
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
                                        (bitmap) => bitmap.Save(TempFileName(fileName), System.Drawing.Imaging.ImageFormat.Png));
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
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
        public static void AreEqual(string fileName, AutomationElement element, Action<Exception, Bitmap> onFail)
        {
            if (TryGetStream(fileName, Assembly.GetCallingAssembly(), out var stream))
            {
                using (stream)
                {
                    using (var expected = (Bitmap)Image.FromStream(stream))
                    {
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

        public static void AreEqual(Bitmap expected, AutomationElement actual)
        {
            using (var actualBmp = Capture.Rectangle(actual.Bounds))
            {
                AreEqual(expected, actualBmp);
            }
        }

        public static void AreEqual(Bitmap expected, UIElement actual)
        {
            using (var actualBmp = actual.ToBitmap(expected.Size(), expected.PixelFormat()))
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

            if (expected.PixelFormat != actual.PixelFormat)
            {
                throw AssertException.Create(
                    "PixelFormats did not match\r\n" +
                    $"Expected: {expected.PixelFormat}\r\n" +
                    $"Actual:   {actual.PixelFormat}");
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

            if (expected.PixelFormat != actual.PixelFormat)
            {
                throw AssertException.Create(
                    "PixelFormats did not match\r\n" +
                    $"Expected: {expected.PixelFormat}\r\n" +
                    $"Actual:   {actual.PixelFormat}");
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

        public static Bitmap ToBitmap(this UIElement element, Size size, PixelFormat pixelFormat)
        {
            return element.ToRenderTargetBitmap(size, pixelFormat)
                          .ToBitmap();
        }

        public static Bitmap ToBitmap(this UIElement element, Size size)
        {
            return element.ToRenderTargetBitmap(size, PixelFormats.Pbgra32)
                          .ToBitmap();
        }

        public static RenderTargetBitmap ToRenderTargetBitmap(this UIElement element, Size size)
        {
            return element.ToRenderTargetBitmap(size, PixelFormats.Pbgra32);
        }

        public static RenderTargetBitmap ToRenderTargetBitmap(this UIElement element, Size size, PixelFormat pixelFormat)
        {
            var result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, pixelFormat);
            element.Measure(size);
            element.Arrange(new Rect(size));
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

        public static void SaveImage(this UIElement element, Size size, string fileName)
        {
            SaveImage(element, size, GetPixelFormat(fileName), fileName);
        }

        public static void SaveImage(this UIElement element, Size size, PixelFormat pixelFormat, string fileName)
        {
            using (var stream = File.OpenWrite(fileName))
            {
                element.Measure(size);
                element.Arrange(new Rect(size));
                var renderTargetBitmap = element.ToRenderTargetBitmap(size, pixelFormat);
                var encoder = GetEncoder(fileName);
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                encoder.Save(stream);
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
                    throw new ArgumentOutOfRangeException();
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

                candidate = Path.Combine(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                    fileName);

                if (File.Exists(candidate))
                {
                    result = File.OpenRead(candidate);
                    return true;
                }

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
                    result = callingAssembly.GetManifestResourceStream(name);
                    return true;
                }
            }

            if (ResourceChache.TryFind(callingAssembly, fileName, out var bitmap))
            {
                result = new MemoryStream();
                bitmap.Save(result, System.Drawing.Imaging.ImageFormat.Png);
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
                result = map.FirstOrDefault(x => x.Key.EndsWith(resourceName)).Value;
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
                            using (var reader = new ResourceReader(stream))
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