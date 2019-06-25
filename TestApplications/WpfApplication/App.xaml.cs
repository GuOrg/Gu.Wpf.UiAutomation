namespace WpfApplication
{
    using System;
    using System.Windows;

    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e != null &&
                e.Args.Length == 1)
            {
                var window = e.Args[0];
                this.StartupUri = new Uri($"Windows/{window}.xaml", UriKind.Relative);
            }

            base.OnStartup(e);
        }
    }
}
