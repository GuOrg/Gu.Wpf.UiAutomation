namespace Gu.Wpf.UiAutomation.Overlay
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Gu.Wpf.UiAutomation.Shapes;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class WinFormsOverlayManager : IOverlayManager
    {
        public int Size { get; set; }
        public int Margin { get; set; }

        public WinFormsOverlayManager()
        {
            this.Size = 3;
            this.Margin = 0;
        }

        public void Show(Rectangle rectangle, System.Windows.Media.Color color, int durationInMs)
        {
            if (rectangle.IsValid)
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

        public void ShowBlocking(Rectangle rectangle, System.Windows.Media.Color color, int durationInMs)
        {
            this.CreateAndShowForms(rectangle, color, durationInMs);
        }

        private void CreateAndShowForms(Rectangle rectangle, System.Windows.Media.Color color, int durationInMs)
        {
            var leftBorder = new Rectangle(rectangle.X - this.Margin, rectangle.Y - this.Margin, this.Size, rectangle.Height + 2 * this.Margin);
            var topBorder = new Rectangle(rectangle.X - this.Margin, rectangle.Y - this.Margin, rectangle.Width + 2 * this.Margin, this.Size);
            var rightBorder = new Rectangle(rectangle.X + rectangle.Width - this.Size + this.Margin, rectangle.Y - this.Margin, this.Size, rectangle.Height + 2 * this.Margin);
            var bottomBorder = new Rectangle(rectangle.X - this.Margin, rectangle.Y + rectangle.Height - this.Size + this.Margin, rectangle.Width + 2 * this.Margin, this.Size);
            var allBorders = new[] { leftBorder, topBorder, rightBorder, bottomBorder };

            var gdiColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            var forms = new List<OverlayRectangleForm>();
            foreach (var border in allBorders)
            {
                var form = new OverlayRectangleForm { BackColor = gdiColor };
                forms.Add(form);
                // Position the window
                User32.SetWindowPos(form.Handle, new IntPtr(-1), border.X.ToInt(), border.Y.ToInt(),
                    border.Width.ToInt(), border.Height.ToInt(), SetWindowPosFlags.SWP_NOACTIVATE);
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

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}
