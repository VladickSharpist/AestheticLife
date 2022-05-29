﻿using Microsoft.AspNetCore.Identity;

namespace AestheticLife.DataAccess.Domain.Models;

public class UserRole : IdentityUserRole<long>
{
    public User User { get; set; }

    public Role Role { get; set; }
}