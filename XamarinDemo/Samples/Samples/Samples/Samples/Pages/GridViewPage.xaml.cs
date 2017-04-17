namespace Samples.Pages
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridViewPage : ContentPage
    {
        public static string[] Images = new string[] { "icon", "icon", "icon", "icon", "icon", "icon" };

        public GridViewPage()
        {
            InitializeComponent();
        }
    }
}
