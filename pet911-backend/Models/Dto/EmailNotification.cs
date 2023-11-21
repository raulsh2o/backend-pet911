using System.ComponentModel.DataAnnotations;

namespace pet911_backend.Models.Dto
{
    public class EmailNotification
    {
        [Required]
        public string Email { get; set; }
    }
}