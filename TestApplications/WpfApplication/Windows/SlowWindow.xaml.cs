namespace WpfApplication.Windows
{
    using System.Threading;
    using System.Windows;

    public partial class SlowWindow : Window
    {
        public SlowWindow()
        {
            Thread.Sleep(100);
            this.InitializeComponent();
        }
    }
}
