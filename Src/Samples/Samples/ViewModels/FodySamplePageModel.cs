namespace Samples.ViewModels
{
    using PropertyChanged;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    [ImplementPropertyChanged]
    public class FodySamplePageModel
    {
        public Action<string> Submited;

        public string UserName { get; set; }

        public ICommand SubmitCommand
        {
            get
            {
                return new Command(() => Submited?.Invoke(UserName));
            }
        }
    }
}
