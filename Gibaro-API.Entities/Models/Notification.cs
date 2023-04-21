using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public string? ActionUrl { get; set; }
        public Guid? SenderId { get; set; }
        public DateTime Timestamp { get; init; } = DateTime.Now;
        public bool IsRead { get; set; }
        public User User { get; set; }
        public User? Sender { get; set; }

        // Enum for notification types
        public enum NotificationType
        {
            Messages,
            FriendRequest,
            Mention,
            Favorite,
            Shared,
        }
    }
}
