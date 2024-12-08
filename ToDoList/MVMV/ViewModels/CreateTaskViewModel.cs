using System.Windows.Input;
using PropertyChanged;
using System.Collections.ObjectModel;
using ToDoTask = ToDoList.MVMV.Models.Task;
using System.Diagnostics;

namespace ToDoList.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CreateTaskViewModel
    {
        private readonly ObservableCollection<ToDoTask> Tasks;
        private readonly INavigation Navigation;
        private double urgencyLevel;
        private readonly ICommand GetUrgencyLevelCommand;
        public string Name { get; set; }
        public bool HasError { get; set; }
        public Color ThumbColor { get; set; }
        public double UrgencyLevel
        {
            get => urgencyLevel;
            set
            {
                urgencyLevel = value;
                ThumbColor = GetColorFromSliderValue(value);
            }
        }

        public CreateTaskViewModel(ObservableCollection<ToDoTask> tasks, INavigation navigation, ICommand getUrgencyLevelCommand)
        {
            Tasks = tasks;
            Navigation = navigation;
            UrgencyLevel = 0;
            HasError = false;
            ThumbColor = Color.FromRgb(0, 255, 0);
            GetUrgencyLevelCommand = getUrgencyLevelCommand;
        }

        public ICommand AddTaskCommand => new Command(async() =>
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                ToDoTask taskToAdd = new ToDoTask(Name, (int)UrgencyLevel);

                Tasks.Add(taskToAdd);
                GetUrgencyLevelCommand.Execute(taskToAdd);

                // Creamos una lista ordenada y la sobrescribimos a nuestra antigua lista
                var orderedTasks = Tasks.OrderByDescending(task => task.UrgencyLevel).ToList();
                Tasks.Clear();
                foreach (var task in orderedTasks) Tasks.Add(task);


                await Navigation.PopAsync();
            }
            else
            {
                HasError = true;
            }
        });

        private Color GetColorFromSliderValue(double value)
        {
            byte r = (byte)((value / 5.0) * 255);
            byte g = (byte)((1 - (value / 5.0)) * 255);
            byte b = 0;
            return Color.FromRgb(r, g, b);
        }
    }
}
