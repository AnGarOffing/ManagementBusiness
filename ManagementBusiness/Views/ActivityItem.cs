namespace ManagementBusiness.Views
{
    public class ActivityItem
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;

        // Constructor sin parámetros requerido por XAML
        public ActivityItem()
        {
        }

        public ActivityItem(string title, string description, string time)
        {
            Title = title;
            Description = description;
            Time = time;
        }
    }
}
