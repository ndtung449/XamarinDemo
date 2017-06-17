namespace Samples.ViewModels.FreshMvvm
{
    using global::FreshMvvm;
    using Samples.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class QuestionPageModel : FreshBasePageModel
    {
        private readonly IQuestionService _questionService;

        public QuestionPageModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public IList<string> Questions { get; set; }

        public ICommand SubmitCommand => new Command(async () => await PushCompletedPageAsync());

        public async override void Init(object initData)
        {
            var taskId = (int)initData;

            Questions = await _questionService.GetByTaskId(taskId);
        }

        private async Task PushCompletedPageAsync()
        {
            await CoreMethods.PushPageModel<CompletedPageModel>();
        }
    }
}
