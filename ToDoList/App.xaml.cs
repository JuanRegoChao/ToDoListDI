
using ListView = ToDoList.MVMV.Views.ListView;

namespace ToDoList
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ListView());
        }
    }
}
