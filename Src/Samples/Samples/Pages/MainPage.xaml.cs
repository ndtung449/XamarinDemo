namespace Samples.Pages
{
    using global::FreshMvvm;
    using System;
    using Xamarin.Forms;

    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            masterPage.ListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is MasterPageItem item)
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);

                if (item.PageModelType != null)
                {
                    page.BindingContext = (FreshBasePageModel)Activator.CreateInstance(item.PageModelType);
                }

                Detail = new NavigationPage(page);

                masterPage.ListView.SelectedItem = null;

                IsPresented = false;
            }
        }
    }
}
