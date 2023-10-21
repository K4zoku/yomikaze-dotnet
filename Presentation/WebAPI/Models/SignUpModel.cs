namespace Yomikaze.WebAPI.Models
{
    public class SignUpModel : UsernamePasswordModel
    {
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }



    }
}
