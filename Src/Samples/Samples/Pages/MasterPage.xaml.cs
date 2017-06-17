namespace Samples.Pages
{
    using Samples.Pages.FreshMvvm;
    using Samples.ViewModels.FreshMvvm;
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        private List<MasterPageItem> _pageItems = new List<MasterPageItem>
        {
            new MasterPageItem
            {
                Title = "GridView Sample",
                TargetType = typeof(GridViewSample)
            },
            new MasterPageItem
            {
                Title = "Bordered Button Sample",
                TargetType = typeof(BorderedButtonSample)
            },
            new MasterPageItem
            {
                Title = "Using PropertyChange.Fody Sample",
                TargetType = typeof(FodySamplePage)
            },
            new MasterPageItem
            {
                Title = "FreshMvvm Sample",
                TargetType = typeof(StartingPage),
                PageModelType = typeof(StartingPageModel)
            }
        };

        public MasterPage()
        {
            InitializeComponent();

            listView.ItemsSource = _pageItems;
        }
    }
}
