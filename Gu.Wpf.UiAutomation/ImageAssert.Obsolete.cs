namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using Size = System.Windows.Size;

    public static partial class ImageAssert
    {
        [Obsolete("Use Assert with OnFail delegate.")]
        public static OnFail OnFail { get; set; }

        /// <summary>
        /// The time the operating system animates when starting the application.
        /// </summary>
        [Obsolete("Use retry time.")]
        public static TimeSpan StartAnimation { get; set; } = WindowsVersion.IsWindows10() ||
                                                              WindowsVersion.IsWindows8() ||
                                                              WindowsVersion.IsWindows8_1()
            ? TimeSpan.FromMilliseconds(1000)
            : TimeSpan.FromMilliseconds(200);

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The automation element.</param>
        /// <param name="onFail">Useful for saving the actual image on error for example.</param>
        [Obsolete("Use Assert with OnFail delegate.")]
        public static void AreEqual(string fileName, UiElement element, Action<Exception, Bitmap> onFail)
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
                    var start = DateTime.Now;
                    while (!Retry.IsTimeouted(start, RetryTime))
                    {
                        using var actual = Capture.Rectangle(element.Bounds);
                        if (Equal(expected, actual))
                        {
                            return;
                        }
                    }

                    using (var actual = Capture.Rectangle(element.Bounds))
                    {
                        if (Equal(expected, actual))
                        {
                            return;
                        }

                        var e = ImageMatchException(expected, actual, fileName);
                        onFail(e, actual);
                        throw e;
                    }
                }
            }
            else
            {
                using var actual = Capture.Rectangle(element.Bounds);
                var e = MissingResourceException(actual, fileName);
                onFail(e, actual);
                throw e;
            }
        }

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The UIElement.</param>
        [Obsolete("This will be removed.")]
        public static void AreEqual(string fileName, System.Windows.UIElement element)
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
                    using var actual = element.ToBitmap(expected.Size(), expected.PixelFormat());
                    AreEqual(expected, actual, fileName);
                }
            }
            else
            {
                using var actual = element.ToBitmap(element.RenderSize, GetPixelFormat(fileName));
                throw MissingResourceException(actual, fileName);
            }
        }

        /// <summary>
        /// Compare Capture.Rectangle(element.Bounds) to the expected image.
        /// </summary>
        /// <param name="fileName">Can be a full file name relative filename or the name of a resource.</param>
        /// <param name="element">The UIElement.</param>
        /// <param name="onFail">Useful for saving the actual image on error for example.</param>
        [Obsolete("This will be removed.")]
        public static void AreEqual(string fileName, System.Windows.UIElement element, Action<Exception, Bitmap> onFail)
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
                    using var actual = element.ToBitmap(expected.Size(), expected.PixelFormat());
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
            else
            {
                using var actual = element.ToBitmap(element.RenderSize, GetPixelFormat(fileName));
                var e = MissingResourceException(actual, fileName);
                onFail(e, actual);
                throw e;
            }
        }

        [Obsolete("This will be removed.")]
        public static void AreEqual(Bitmap expected, System.Windows.UIElement actual)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }

            if (actual == null)
            {
                throw new ArgumentNullException(nameof(actual));
            }

            using var actualBmp = actual.ToBitmap(expected.Size(), expected.PixelFormat());
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

        [Obsolete("This will be removed.")]
        public static void AreEqual(UiElement expected, UiElement actual)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }

            if (actual == null)
            {
                throw new ArgumentNullException(nameof(actual));
            }

            if (EqualsFast(expected, actual))
            {
                return;
            }

            WaitForStartAnimation(actual);
            using var expectedBmp = Capture.Rectangle(expected.Bounds);
            using var actualBmp = Capture.Rectangle(actual.Bounds);
            AreEqual(expectedBmp, actualBmp);

            static bool EqualsFast(UiElement expected, UiElement actual)
            {
                using var x = Capture.Rectangle(expected.Bounds);
                using var y = Capture.Rectangle(actual.Bounds);
                return EqualFast(x, y);
            }
        }

        [Obsolete("This will be removed.")]
        public static void AreEqual(Bitmap expected, Bitmap actual, string fileName)
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

                if (OnFail == OnFail.SaveImageToTemp)
                {
                    actual.Save(TempFileName(fileName), GetImageFormat(fileName));
                }

                throw ImageMatchException(expected, actual, fileName);
            }
        }

        [Obsolete("This will be removed.")]
        public static void AreEqual(Bitmap expected, Bitmap actual, Action<Bitmap> onFail)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }

            if (actual == null)
            {
                throw new ArgumentNullException(nameof(actual));
            }

            if (onFail == null)
            {
                throw new ArgumentNullException(nameof(onFail));
            }

            if (!Equal(expected, actual))
            {
                if (Debugger.IsAttached)
                {
                    ImageDiffWindow.Show(expected, actual);
                }

                onFail(actual);
                throw ImageMatchException(expected, actual, null);
            }
        }

        [Obsolete("This will be removed.")]
        public static Bitmap ToBitmap(this System.Windows.UIElement element, System.Windows.Size size, PixelFormat pixelFormat)
        {
            return element.ToRenderTargetBitmap(size, pixelFormat)
                          .ToBitmap();
        }

        [Obsolete("This will be removed.")]
        public static Bitmap ToBitmap(this System.Windows.UIElement element, System.Windows.Size size)
        {
            return element.ToRenderTargetBitmap(size, PixelFormats.Pbgra32)
                          .ToBitmap();
        }

        [Obsolete("This will be removed.")]
        public static RenderTargetBitmap ToRenderTargetBitmap(this System.Windows.UIElement element, System.Windows.Size size)
        {
            return element.ToRenderTargetBitmap(size, PixelFormats.Pbgra32);
        }

        [Obsolete("This will be removed.")]
        public static RenderTargetBitmap ToRenderTargetBitmap(this System.Windows.UIElement element, System.Windows.Size size, PixelFormat pixelFormat)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, pixelFormat);
            element.Measure(size);
            element.Arrange(new System.Windows.Rect(size));
            result.Render(element);
            return result;
        }

        [Obsolete("This will be removed.")]
        public static Bitmap ToBitmap(this RenderTargetBitmap bitmap)
        {
            using var stream = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(stream);
            return new Bitmap(stream);
        }

        [Obsolete("This will be removed.")]
        public static void SaveImage(this System.Windows.UIElement element, System.Windows.Size size, string fileName)
        {
            SaveImage(element, size, GetPixelFormat(fileName), fileName);
        }

        [Obsolete("This will be removed.")]
        public static void SaveImage(this System.Windows.UIElement element, Size size, PixelFormat pixelFormat, string fileName)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            using var stream = File.OpenWrite(fileName);
            element.Measure(size);
            element.Arrange(new System.Windows.Rect(size));
            var renderTargetBitmap = element.ToRenderTargetBitmap(size, pixelFormat);
            var encoder = GetEncoder(fileName);
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            encoder.Save(stream);
        }

        [Obsolete("This will be removed.")]
        private static void WaitForStartAnimation(UiElement element)
        {
            if (StartAnimation <= TimeSpan.Zero)
            {
                return;
            }

            var window = element.Window;
            _ = User32.GetWindowThreadProcessId(window.NativeWindowHandle, out var id);
            using var process = Process.GetProcessById((int)id);
            var upTime = DateTime.Now - process.StartTime;
            if (upTime < StartAnimation)
            {
                Wait.For(StartAnimation - upTime);
            }
        }
    }
}
