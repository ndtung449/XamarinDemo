namespace XamarinDemo.Controls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class TaskViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NotifyClickCommand { protected set; get; }

        public Action<TaskViewViewModel> ButtonClicked;

        public TaskViewViewModel()
        {
            NotifyClickCommand = new Command(() =>
            {
                ButtonClicked?.Invoke(this);
            });
        }

        private ImageSource _source = "icon";

        public ImageSource Source
        {
            get
            {
                return _source;
            }

            set
            {
                if (_source != value)
                {
                    _source = value;
                    OnPropertyChanged(nameof(Source));
                }
            }
        }

        private string _text = "Sample Task";

        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
