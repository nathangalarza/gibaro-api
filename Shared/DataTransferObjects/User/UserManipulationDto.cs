using System.ComponentModel.DataAnnotations;
//using Shared.Validations;

namespace Shared.DataTransferObjects.User;

public record UserManipulationDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Name must be between 2 - 25 characters")]
    public string? Name { get; init; }
    
    [Required(ErrorMessage = "Username is required")]
    [StringLength(25, MinimumLength = 4, ErrorMessage = "Username must be between 4 - 25 characters")]
    [RegularExpression(@"^[A-Za-z0-9_.-]*$", ErrorMessage = "Username must only contain letters, numbers, dashes, underscores or periods")]
    public string? UserName { get; init; }
    
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; init; }
    
    [Required(ErrorMessage = "Birthdate is required")]
    //[BirthdateValidator]
    public string? Birthdate { get; init; }
    
    public string? Phone { get; init; }
}