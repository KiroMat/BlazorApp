using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace DataApi.Shared.Models
{
    [SwaggerTag("Lolitka")]
    public class User
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        
        [Required]
        
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
