namespace pet911_backend.Models
{
    public class User
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }
        public DateOnly? Birthdate { get; set; }
        public string? IdRole { get; set; }
        public Role? Role { get; set; }
        public List<Pet>? pets { get; set; }
        public List<Service>? services { get; set; }
    }
}
