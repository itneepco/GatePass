using System.ComponentModel.DataAnnotations;

namespace GatePass.UI.Data;

public class ResetPasswordFormDto
{
    public string? Id { get; set; }
    
    [Required] 
    public string? UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
    public string? ConfirmPassword { get; set; }
}
