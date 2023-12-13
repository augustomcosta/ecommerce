﻿using Microsoft.AspNetCore.Identity;

namespace ecommerce_api.Domain.Models.Identity;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
    public Address Address { get; set; }
}