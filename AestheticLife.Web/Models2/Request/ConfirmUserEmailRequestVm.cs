﻿namespace AestheticLife.Web.Models2.Request;

public class ConfirmUserEmailRequestVm
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
}