﻿namespace Samples.Pages
{
    using Samples.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using XamarinDemo.Controls;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridViewSample : ContentPage
    {
        public static string[] Images = new string[] { "icon", "icon", "icon", "icon", "icon", "icon" };

        public GridViewSample()
        {
            InitializeComponent();
        }
    }
}
