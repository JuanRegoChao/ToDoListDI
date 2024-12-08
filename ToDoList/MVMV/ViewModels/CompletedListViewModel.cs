using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using PropertyChanged;
using ToDoTask = ToDoList.MVMV.Models.Task;


namespace ToDoList.MVMV.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CompletedListViewModel
    {
        public bool IsRefreshing { get; set; }
        public ObservableCollection<ToDoTask> CompletedTasks { get; set; }
        public ObservableCollection<ToDoTask> Tasks { get; set; }
        private readonly INavigation Navigation;

        #region CONSTRUCTOR
        public CompletedListViewModel(ObservableCollection<ToDoTask> completedTasks, ObservableCollection<ToDoTask> tasks, INavigation navigation)
        {
            CompletedTasks = completedTasks;
            Tasks = tasks;
            Navigation = navigation;
        }
        #endregion

        #region COMMANDS
        public ICommand DeleteTaskCommand => new Command<ToDoTask>((task) =>
        {
            if (task is not null) CompletedTasks.Remove(task);
        });

        public ICommand RestoreTaskCommand => new Command<ToDoTask>(async (task) =>
        {
            if (task is not null)
            {
                Tasks.Add(task);

                CompletedTasks.Remove(task);

                // Creamos una lista ordenada y la sobrescribimos a nuestra antigua lista
                var orderedTasks = Tasks.OrderByDescending(myTask => myTask.UrgencyLevel).ToList();
                Tasks.Clear();
                foreach (var myTask in orderedTasks) Tasks.Add(myTask);

                await Navigation.PopAsync();
            }
        });
        #endregion
    }
}