using System;
using Microsoft.AspNetCore.Identity;

namespace ShoeShop.Repository.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Basic profile
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DisplayName { get; set; }

        // Derived helper (not mapped)
        public string FullName => string.IsNullOrWhiteSpace(DisplayName)
            ? $"{FirstName} {LastName}".Trim()
            : DisplayName;

        // Optional profile & contact
        public string? ProfilePictureUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        // App-specific flags / auditing
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}