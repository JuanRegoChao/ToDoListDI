using System.Collections.ObjectModel;
using ToDoList.MVVM.ViewModels;
using ToDoTask = ToDoList.MVMV.Models.Task;

namespace ToDoList.MVMV.Views;

public partial class CreateTaskView : ContentPage
{
    public CreateTaskView(CreateTaskViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
