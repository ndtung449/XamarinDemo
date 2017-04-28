namespace Samples.Pages
{
    using Samples.ViewModels;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FodySamplePage : ContentPage
    {
        public FodySamplePage()
        {
            InitializeComponent();

            var viewmodel = (FodySamplePageModel)BindingContext;

            viewmodel.Submitted = (username) => DisplayAlert("Sample", $"Welcome, {username}!!!", "OK");
        }
    }
}
