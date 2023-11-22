using System.ComponentModel.DataAnnotations;

namespace pet911_backend.Models.Dto
{
    public class EmailNotification
    {
        [Required]
        public string Email_rx { get; set; }
        public string Email_tx { get; set; }

    }
}