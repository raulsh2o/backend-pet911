using System.ComponentModel.DataAnnotations.Schema;

namespace pet911_backend.Models.Dto
{
    [Table("notification")]
    public class Notification
    {
        public string? Id { get; set; }
        public string Email_tx { get; set; }
        public string Email_rx { get; set; }
        public string Message { get; set; }

    }
}
