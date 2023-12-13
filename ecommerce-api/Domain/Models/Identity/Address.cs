﻿using System.ComponentModel.DataAnnotations;

namespace ecommerce_api.Domain.Models.Identity;

public class Address
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    [Required]
    public string AppUserId { get; set; }
    public AppUser App { get; set; }
}