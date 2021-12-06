using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class UserRegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordCheck { get; set; }

    }
}
