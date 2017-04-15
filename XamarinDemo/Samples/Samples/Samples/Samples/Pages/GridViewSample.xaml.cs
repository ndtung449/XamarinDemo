namespace Samples.Pages
{
    using Samples.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using XamarinDemo.Controls;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridViewSample : ContentPage
    {
        private GridViewSampleViewModel _viewmodel;

        public GridViewSample()
        {
            InitializeComponent();
            _viewmodel = (GridViewSampleViewModel)BindingContext;
            _viewmodel.OpenTask += OnOpenButtonClicked;
        }

        private void OnOpenButtonClicked(TaskViewViewModel task)
        {
            DisplayAlert("Alert", $"Open [{task.Text}] Task", "OK");
        }
    }
}
