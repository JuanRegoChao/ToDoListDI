using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoList.MVMV.ViewModels;
using ToDoTask = ToDoList.MVMV.Models.Task;

namespace ToDoList.MVMV.Views
{
    public partial class CompletedListView : ContentPage
    {
        public CompletedListView(ObservableCollection<ToDoTask> completedTasks, ObservableCollection<ToDoTask> tasks)
        {
            InitializeComponent();
            BindingContext = new CompletedListViewModel(completedTasks, tasks, Navigation);
        }
    }
}