using ToDoList.MVMV.ViewModels;

namespace ToDoList.MVMV.Views;

public partial class ListView : ContentPage
{
	public ListView()
	{
		InitializeComponent();
        BindingContext = new ListViewModel(Navigation);
    }
}