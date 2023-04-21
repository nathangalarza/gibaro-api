using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsFullyRegistered { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? AvatarUrl { get; set; }
        public string? HeaderUrl { get; set; }
        public bool BirthDateLocked { get; set; }
        public string? CountryCode { get; set; }
        public string? Bio { get; set; }
        public string? LeavingAppReason { get; set; }
        public string? CustomerId { get; set; }
        public List<Notification>? Notifications { get; set; }
        public List<UserDevice>? Devices { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsProffesional { get; set; }
        public bool Notification { get; set; }

    }
}
