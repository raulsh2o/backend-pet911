using System.ComponentModel.DataAnnotations;

namespace pet911_backend.Models.Dto
{
    public class ResetPassword
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
