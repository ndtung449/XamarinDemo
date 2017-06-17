namespace Samples.ViewModels.FreshMvvm
{
    using global::FreshMvvm;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class CompletedPageModel : FreshBasePageModel
    {
        public ICommand BackToTasksPageCommand => new Command(async () => await CoreMethods.PopToRoot(true));
    }
}
