namespace WpfApplication.Windows
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class TextBoxWindowViewModel : INotifyPropertyChanged
    {
        private string text = "abc";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get => this.text;

            set
            {
                if (value == this.text)
                {
                    return;
                }

                this.text = value;
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
