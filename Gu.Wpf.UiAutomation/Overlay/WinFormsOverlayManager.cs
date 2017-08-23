namespace Gu.Wpf.UiAutomation.Overlay
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class WinFormsOverlayManager : IOverlayManager
    {
        /// <inheritdoc/>
        public int Size { get; set; }

        /// <inheritdoc/>
        public int Margin { get; set; }

        public WinFormsOverlayManager()
        {
            this.Size = 3;
            this.Margin = 0;
        }

        /// <inheritdoc/>
        public void Show(Rect rectangle, System.Windows.Media.Color color, int durationInMs)
        {
            if (rectangle.IsValid())
            {
#if NET35
                new Thread(() =>
                {
                    CreateAndShowForms(rectangle, color, durationInMs);
                }).Start();
#elif NET40
                System.Threading.Tasks.Task.Factory.StartNew(() => {
                    CreateAndShowForms(rectangle, color, durationInMs);
                });
#else
                System.Threading.Tasks.Task.Run(() => this.CreateAndShowForms(rectangle, color, durationInMs));
#endif
            }
        }

        /// <inheritdoc/>
        public void ShowBlocking(Rect rectangle, System.Windows.Media.Color color, int durationInMs)
        {
            this.CreateAndShowForms(rectangle, color, durationInMs);
        }

        private void CreateAndShowForms(Rect rectangle, System.Windows.Media.Color color, int durationInMs)
        {
            var leftBorder = new Rect(
                x: rectangle.X - this.Margin,
                y: rectangle.Y - this.Margin,
                width: this.Size,
                height: rectangle.Height + (2 * this.Margin));

            var topBorder = new Rect(
                x: rectangle.X - this.Margin,
                y: rectangle.Y - this.Margin,
                width: rectangle.Width + (2 * this.Margin),
                height: this.Size);

            var rightBorder = new Rect(
                x: rectangle.X + rectangle.Width - this.Size + this.Margin,
                y: rectangle.Y - this.Margin,
                width: this.Size,
                height: rectangle.Height + (2 * this.Margin));
            var bottomBorder = new Rect(
                x: rectangle.X - this.Margin,
                y: rectangle.Y + rectangle.Height - this.Size + this.Margin,
                width: rectangle.Width + (2 * this.Margin),
                height: this.Size);

            var allBorders = new[] { leftBorder, topBorder, rightBorder, bottomBorder };

            var gdiColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            var forms = new List<OverlayRectangleForm>();
            foreach (var border in allBorders)
            {
                var form = new OverlayRectangleForm { BackColor = gdiColor };
                forms.Add(form);

                // Position the window
                User32.SetWindowPos(
                    form.Handle,
                    new IntPtr(-1),
                    border.X.ToInt(),
                    border.Y.ToInt(),
                    border.Width.ToInt(),
                    border.Height.ToInt(),
                    SetWindowPosFlags.SWP_NOACTIVATE);

                // Show the window
                User32.ShowWindow(form.Handle, ShowWindowTypes.SW_SHOWNA);
            }

            Thread.Sleep(durationInMs);
            foreach (var form in forms)
            {
                // Cleanup
                form.Hide();
                form.Close();
                form.Dispose();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}
