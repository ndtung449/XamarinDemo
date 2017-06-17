namespace Samples
{
    using FreshMvvm;
    using Samples.Pages;
    using Samples.Services;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            RegisterIoC();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void RegisterIoC()
        {
            CleanupIoc();

            FreshIOC.Container.Register<IQuestionService, QuestionService>();
        }

        private void CleanupIoc()
        {
            FreshIOC.Container.Unregister<IQuestionService>();
        }
    }
}
