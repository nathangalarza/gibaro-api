using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.User
{
    public record UserCreationDto : UserManipulationDto
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(18, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 10)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).*$", ErrorMessage = "Password must have 1+ uppercase, 1+ lowercase and 1+ number")]
        [DataType(DataType.Password)]
        public string? Password { get; init; }
    }
}
