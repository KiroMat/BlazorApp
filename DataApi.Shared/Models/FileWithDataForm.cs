using Microsoft.AspNetCore.Http;

namespace DataApi.Shared.Models
{
    public class FileWithDataForm
    {
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
}
