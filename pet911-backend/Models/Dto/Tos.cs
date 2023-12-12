using System.ComponentModel.DataAnnotations.Schema;

namespace pet911_backend.Models.Dto
{
    [Table("tos")]
    public class Tos
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
