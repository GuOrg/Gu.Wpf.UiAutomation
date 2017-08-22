namespace WpfApplication
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DataGridItem : INotifyPropertyChanged
    {
        private int id;
        private string name;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get => this.id;

            set
            {
                if (value == this.id)
                {
                    return;
                }

                this.id = value;
                this.OnPropertyChanged();
            }
        }

        public string Name
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
