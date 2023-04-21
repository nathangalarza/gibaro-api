using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.User
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public DateTime Birthdate { get; init; }
        public string? Phone { get; init; }
        public bool IsFullyRegistered { get; set; }


    }
}
