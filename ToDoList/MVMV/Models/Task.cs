
using PropertyChanged;


namespace ToDoList.MVMV.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Task
    {
        public string Name { get; set; } = string.Empty;
        public string CompletedImage { get; set; } = "chek_button_up.png";
        public string ShortDescription { get; set; } = string.Empty;
        public int UrgencyLevel { get; set; } = 0;
        public string UrgencyFontAttributes { get; set; } = "None";
        public string UrgencyTextColor { get; set; } = "Black";
        public Task? SelectedTask { get; set; }

        
        public Task(String name, int urgencyLevel)
        {
            Name = name;
            UrgencyLevel = urgencyLevel;
        }
    }
}
