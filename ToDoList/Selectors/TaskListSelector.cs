
using Task = ToDoList.MVMV.Models.Task;

namespace ToDoList.Selectors
{
    public class TaskListSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Task? task = item as Task;
            if (task != null && task.UrgencyLevel > 2)
            {
                Application.Current!.Resources.TryGetValue("UrgencyTaskTemplate", out var urgencyTaskStyle);
                return urgencyTaskStyle as DataTemplate ?? new DataTemplate();
            }

            Application.Current!.Resources.TryGetValue("TaskTemplate", out var taskStyle);
            return taskStyle as DataTemplate ?? new DataTemplate();
        }
    }
}
