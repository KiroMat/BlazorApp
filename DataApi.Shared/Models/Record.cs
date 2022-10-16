namespace DataApi.Shared.Models
{
    public class Record
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
