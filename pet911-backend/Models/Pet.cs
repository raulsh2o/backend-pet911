namespace pet911_backend.Models
{
    public class Pet
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string? Race { get; set; }
        public string? Allergies { get; set; }
        public string? IdUser { get; set; }
        public User? user { get; set; }
    }
}
