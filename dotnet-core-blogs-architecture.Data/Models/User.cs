﻿using dotnet_core_blogs_architecture.Data.Models;
using dotnet_core_blogs_architecture.infrastructure.Models;

namespace dotnet_core_blogs_architecture.Data.Models
{
    public class User : EntityBaseWithTypedId<long>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public bool IsActive { get; set; }

        public string PasswordHash { get; set; }

        public string GetFullName()
        {
            if (!string.IsNullOrWhiteSpace(MiddleName))
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }

            return $"{FirstName} {LastName}";
        }
    }
}
