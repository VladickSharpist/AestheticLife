﻿namespace AestheticLife.Web.Models2.Request;

public class RegistrationRequestVm
{
    public string UserName { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string SecondName { get; set; }

    public string DateOfBirth { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
}