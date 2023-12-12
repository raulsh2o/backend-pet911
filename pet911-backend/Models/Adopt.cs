using System.ComponentModel.DataAnnotations.Schema;

namespace pet911_backend.Models
{
    [Table("adopt")]
    public class Adopt
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string? Race { get; set; }
        public string? Allergies { get; set; }
        public string? Notes { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
    }
}
