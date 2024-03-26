using System.ComponentModel.DataAnnotations;
using Yomikaze.Domain.Models;

public abstract class AuthenticationModel
{
    [Required]
    [Length(3, 32, ErrorMessage = "UserName must be between 3 and 32 characters")]
    public string Username { get; set; } = default!;

    [Required] public string Password { get; set; } = default!;
}

public class SignInModel : AuthenticationModel
{
}

public class SignUpModel : AuthenticationModel
{
    [Required]
    [Length(3, 40, ErrorMessage = "FullName must be between 3 and 40 characters")]
    public string Fullname { get; set; } = default!;

    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = default!;

    [Required] [EmailAddress] public string Email { get; set; } = default!;

    [Required]
    [DataType(DataType.Date)]
    // min date is 1900-01-01
    [CustomDateRange(ErrorMessage = "Birthday cannot be greater than current date.")]
    public DateTime Birthday { get; set; } = default!;
}