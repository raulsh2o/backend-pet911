namespace pet911_backend.Models.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public DateOnly? Birthdate { get; set; }
        public string? IdRole { get; set; }
    }
}
