namespace Samples.ViewModels.FreshMvvm
{
    using global::FreshMvvm;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class TasksPageModel : FreshBasePageModel
    {
        public ICommand PushQuestionPageCommand => new Command(async (taskId) =>
        {
            await CoreMethods.PushPageModel<QuestionPageModel>(Convert.ToInt32(taskId));
        });
    }
}
