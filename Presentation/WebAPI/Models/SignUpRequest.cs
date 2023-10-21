using System.ComponentModel.DataAnnotations;

namespace Yomikaze.WebAPI.Models
{
    public class SignUpRequest : UsernamePasswordModel
    {

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = default!;

        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public DateTime Birthday { get; set; } = default!;



    }
}
