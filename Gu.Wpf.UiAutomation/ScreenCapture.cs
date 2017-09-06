﻿namespace Gu.Wpf.UiAutomation
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using Gu.Wpf.UiAutomation.Logging;

    /// <summary>
    /// Provides methods to capture screenshots or partially screenshots
    /// </summary>
    public static class ScreenCapture
    {
        /// <summary>
        /// Captures the whole screen (all monitors)
        /// </summary>
        public static Bitmap CaptureScreen()
        {
            return CaptureArea(
                new Rect(
                    x: SystemParameters.VirtualScreenLeft,
                    y: SystemParameters.VirtualScreenTop,
                    width: SystemParameters.VirtualScreenWidth,
                    height: SystemParameters.VirtualScreenHeight));
        }

        public static BitmapImage CaptureScreenWpf()
        {
            return CaptureScreen().ToWpf();
        }

        public static BitmapImage CaptureAreaWpf(Rect rectangle)
        {
            return CaptureArea(rectangle).ToWpf();
        }

        /// <summary>
        /// Captures the screen and saves it to a file
        /// </summary>
        public static void CaptureScreenToFile(string filePath)
        {
            using (var bmp = CaptureScreen())
            {
                Logger.Default.Info($"CaptureScreenToFile: {filePath}");
                bmp.Save(filePath, ImageFormat.Png);
            }
        }

        /// <summary>
        /// Captures an element and saves it to a file
        /// </summary>
        public static void CaptureToFile(AutomationElement element, string filePath)
        {
            using (var bmp = CaptureArea(element.Properties.BoundingRectangle.Value))
            {
                Logger.Default.Info($"CaptureToFile: {element} {filePath}");
                bmp.Save(filePath, ImageFormat.Png);
            }
        }

        /// <summary>
        /// Captures a specific area and saves it to a file
        /// </summary>
        public static void CaptureAreaToFile(Rect rectangle, string filePath)
        {
            using (var bmp = CaptureArea(rectangle))
            {
                Logger.Default.Info($"CaptureAreaToFile: {rectangle} {filePath}");
                bmp.Save(filePath, ImageFormat.Png);
            }
        }

        /// <summary>
        /// Captures a specific area from the screen
        /// </summary>
        public static Bitmap CaptureArea(Rect rectangle)
        {
            var width = rectangle.Width.ToInt();
            var height = rectangle.Height.ToInt();
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.CopyFromScreen(
                    sourceX: rectangle.Left.ToInt(),
                    sourceY: rectangle.Top.ToInt(),
                    destinationX: 0,
                    destinationY: 0,
                    blockRegionSize: new System.Drawing.Size(width, height),
                    copyPixelOperation: CopyPixelOperation.SourceCopy);
                return bmp;
            }
        }

        /// <summary>
        /// Converts a WinForms bitmap to a WPF bitmap
        /// </summary>
        public static BitmapImage ToWpf(this Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                memory.Seek(0, SeekOrigin.Begin);
                bitmapImage.StreamSource = memory;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }
    }
}
