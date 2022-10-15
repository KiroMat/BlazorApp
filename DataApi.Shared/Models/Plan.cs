namespace DataApi.Shared.Models
{
    public class Plan : Record
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverPath { get; set; }
        public virtual ICollection<ToDoItem> ToDoItems { get; set; }
    }
}
