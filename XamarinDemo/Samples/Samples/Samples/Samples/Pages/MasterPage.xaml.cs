namespace Samples.Pages
{
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
            }
        };

        public MasterPage()
        {
            InitializeComponent();

            listView.ItemsSource = _pageItems;
        }
    }
}
