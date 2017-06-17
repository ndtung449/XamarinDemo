namespace Samples.ViewModels.FreshMvvm
{
    using global::FreshMvvm;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class StartingPageModel : FreshBasePageModel
    {
        public ICommand StartCommand => new Command(() => DisplayTasksPage());

        private void DisplayTasksPage()
        {
            var tasksPage = FreshPageModelResolver.ResolvePageModel<TasksPageModel>();

            var tasksPageContainer = new FreshNavigationContainer(tasksPage, "TasksPageContainer");

            Application.Current.MainPage = tasksPageContainer;
        }
    }
}
