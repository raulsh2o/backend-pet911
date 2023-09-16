namespace pet911_backend.Models
{
    public class Role
    {
        public string? Id { get; set; }
        public string RoleType { get; set; }
        public string Description { get; set; }
        public List<User>? users { get; set; }
    }
}
