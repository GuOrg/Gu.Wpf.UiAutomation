namespace Gu.Wpf.UiAutomation.Overlay
{
    using System.Windows.Forms;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class OverlayRectangleForm : Form
    {
        public OverlayRectangleForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Left = 0;
            this.Top = 0;
            this.Width = 1;
            this.Height = 1;
            this.Visible = false;
        }

        /// <inheritdoc/>
        protected override bool ShowWithoutActivation => true;

        /// <inheritdoc/>
        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ExStyle |= (int)WindowStyles.WS_EX_TOPMOST;
                return createParams;
            }
        }
    }
}
