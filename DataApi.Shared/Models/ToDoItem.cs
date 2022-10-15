namespace DataApi.Shared.Models
{
    public class ToDoItem : Record
    {
        public string Description { get; set; }

        public bool IsDone { get; set; }
        public DateTime? EstimatedDate { get; set; }
        public DateTime? AchievedDate { get; set; }

        public virtual Plan Plan { get; set; }

        public string PlanId { get; set; }
    }
}
