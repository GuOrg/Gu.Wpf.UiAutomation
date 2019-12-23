namespace WpfApplication
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DataGridItemViewModel : INotifyPropertyChanged
    {
        private int intValue;
        private string? stringValue;
        private bool boolValue;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int IntValue
        {
            get => this.intValue;

            set
            {
                if (value == this.intValue)
                {
                    return;
                }

                this.intValue = value;
                this.OnPropertyChanged();
            }
        }

        public string? StringValue
        {
            get => this.stringValue;

            set
            {
                if (value == this.stringValue)
                {
                    return;
                }

                this.stringValue = value;
                this.OnPropertyChanged();
            }
        }

        public bool BoolValue
        {
            get => this.boolValue;

            set
            {
                if (value == this.boolValue)
                {
                    return;
                }

                this.boolValue = value;
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
