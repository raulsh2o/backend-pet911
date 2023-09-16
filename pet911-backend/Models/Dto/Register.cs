using System.ComponentModel.DataAnnotations;

namespace pet911_backend.Models.Dto
{
    public class Register
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Birthdate { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
