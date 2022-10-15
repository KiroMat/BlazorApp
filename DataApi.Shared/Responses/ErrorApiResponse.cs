namespace DataApi.Shared.Responses
{
    public class ErrorApiResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
