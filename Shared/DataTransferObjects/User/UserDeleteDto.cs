using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.User
{
    public record UserDeleteDto 
    {
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
    }
}
