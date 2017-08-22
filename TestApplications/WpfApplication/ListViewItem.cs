namespace WpfApplication
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ListViewItem : INotifyPropertyChanged
    {
        private string key;
        private string value;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Key
        {
            get => this.key;

            set
            {
                if (value == this.key)
                {
                    return;
                }

                this.key = value;
                this.OnPropertyChanged();
            }
        }

        public string Value
        {
            get => this.value;

            set
            {
                if (value == this.value)
                {
                    return;
                }

                this.value = value;
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
