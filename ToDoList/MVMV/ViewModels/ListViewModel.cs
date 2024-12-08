using System.Windows.Input;
using PropertyChanged;
using System.Collections.ObjectModel;
using ToDoList.MVMV.Views;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using ToDoTask = ToDoList.MVMV.Models.Task;
using System.Diagnostics;
using ToDoList.MVVM.ViewModels;

namespace ToDoList.MVMV.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ListViewModel
    {
        private readonly INavigation Navigation;

        #region TASKS LIST
        private List<ToDoTask> _tasks = new List<ToDoTask>
        {
            new ToDoTask("Tarea de pruebas 1", 3),
            new ToDoTask("Tarea de pruebas 2", 1),
            new ToDoTask("Tarea de pruebas 3", 5),
            new ToDoTask("Tarea de pruebas 4", 2),
            new ToDoTask("Tarea de pruebas 5", 4)
        };
        #endregion

        #region CONSTRUCTOR
        public ListViewModel(INavigation navigation)
        {
            Navigation = navigation;

            RefreshTasks();
        }
        #endregion

        #region PROPERTIES
        public ObservableCollection<ToDoTask> Tasks { get; set; } = new ObservableCollection<ToDoTask>();
        public ObservableCollection<ToDoTask> CompletedTasks { get; set; } = new ObservableCollection<ToDoTask>();
        public bool IsRefreshing { get; set; }
        #endregion

        #region COMMANDS
        public ICommand RefreshCommand => new Command(() =>
        {
            IsRefreshing = true;
            RefreshTasks();
            IsRefreshing = false;
        });
        public ICommand CompleteTaskCommand => new Command<ToDoTask>(async (task) =>
        {
            if (task is not null)
            {
                task.CompletedImage = "chek_button_down";
                await Task.Delay(150);
                Tasks.Remove(task);
                _tasks.Remove(task);
                CompletedTasks.Add(task);
                task.CompletedImage = "chek_button_up";
            }
        });
        public ICommand GetUrgencyLevelCommand => new Command<ToDoTask>((task) =>
        {
            switch (task.UrgencyLevel)
            {
                case 0: task.ShortDescription = "No corre prisa"; task.UrgencyTextColor = "Black"; break;
                case 1: task.ShortDescription = "Poco urgente"; task.UrgencyTextColor = "Black"; break;
                case 2: task.ShortDescription = "Moderadamente urgente"; task.UrgencyTextColor = "Black"; break;
                case 3: task.ShortDescription = "URGENTE"; task.UrgencyTextColor = "DarkOrange"; break;
                case 4: task.ShortDescription = "MUY URGENTE"; task.UrgencyTextColor = "DarkRed"; break;
                case 5: task.ShortDescription = "EXTREMADAMENTE\nURGENTE"; task.UrgencyTextColor = "Crimson"; break;
                default: task.ShortDescription = "Urgencia sin determinar"; task.UrgencyTextColor = "Black"; break;
            }
        });
        public ICommand PushToCreateTaskView => new Command(async () =>
        {
            var createTaskViewModel = new CreateTaskViewModel(Tasks, Navigation, GetUrgencyLevelCommand);
            await Navigation.PushAsync(new CreateTaskView(createTaskViewModel));
        });
        public ICommand PushToCompletedListView => new Command(async () =>
        {
            await Navigation.PushAsync(new CompletedListView(CompletedTasks, Tasks));
        });
        #endregion

        #region PRIVATE METHODS
        private void RefreshTasks(int skip = 0, int take = 10)
        {
            var tempTasks = _tasks.OrderByDescending(task => task.UrgencyLevel).Skip(skip).Take(take).ToList();

            foreach (var task in tempTasks)
            {
                if (!Tasks.Contains(task)) Tasks.Add(task); // Si la tarea no esta en la lista la añadimos
                GetUrgencyLevelCommand.Execute(task); // Actualizamos las urgencias
            }
        }
        #endregion

    }
}