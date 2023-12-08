namespace pet911_backend.Models
{
    public class Adopt
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string? Race { get; set; }
        public string? Allergies { get; set; }
        public string? Notes { get; set; }
    }
}
