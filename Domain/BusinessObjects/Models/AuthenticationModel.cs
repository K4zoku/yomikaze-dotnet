﻿using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Models;

public abstract class AuthenticationModel
{
    [Required] public string Username { get; set; } = default!;

    [Required] public string Password { get; set; } = default!;
}

public class SignInModel : AuthenticationModel
{
}

public class SignUpModel : AuthenticationModel
{
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = default!;

    [Required] [EmailAddress] public string Email { get; set; } = default!;

    [Required] [DataType(DataType.Date)] public DateTime Birthday { get; set; } = default!;
}