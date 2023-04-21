using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserDevice
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string DeviceId { get; set; }

        public string Model { get; set; }

        public string RefreshToken { get; set; }

        public string? UserAgent { get; set; }

        public Platform? Platform { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUsedAt { get; set; }

        public bool Revoked { get; set; } = false;

        public User User { get; set; }
    }
}


public enum Platform
{
    Android,
    IOS,
}