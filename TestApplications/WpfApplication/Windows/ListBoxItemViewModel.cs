namespace WpfApplication.Windows
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ListBoxItemViewModel : INotifyPropertyChanged
    {
        private string? name;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? Name
        {
            get => this.name;

            set
            {
                if (value == this.name)
                {
                    return;
                }

                this.name = value;
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
