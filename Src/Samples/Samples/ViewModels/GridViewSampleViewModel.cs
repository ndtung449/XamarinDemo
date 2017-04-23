namespace Samples.ViewModels
{
    using System;
    using XamarinDemo.Controls;

    public class GridViewSampleViewModel
    {
        public Action<TaskViewViewModel> OpenTask;

        private TaskViewViewModel[] _tasks = new TaskViewViewModel[]
        {
            new TaskViewViewModel{Source = "icon", Text = "The 1st Task"},
            new TaskViewViewModel{Source = "icon", Text = "The 2nd Task"},
            new TaskViewViewModel{Source = "icon", Text = "The 3rd Task"},
            new TaskViewViewModel{Source = "icon", Text = "The 4th Task"},
            new TaskViewViewModel{Source = "icon", Text = "The 5th Task"},
            new TaskViewViewModel{Source = "icon", Text = "The 6th Task"},
        };

        public TaskViewViewModel[] Tasks => _tasks;

        public GridViewSampleViewModel()
        {
            foreach (var task in Tasks)
            {
                task.ButtonClicked += OnButtonClicked;
            }
        }

        private void OnButtonClicked(TaskViewViewModel task)
        {
            task.IsEnabled = false;
            OpenTask?.Invoke(task);
        }
    }
}
