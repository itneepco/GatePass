using System.ComponentModel.DataAnnotations;

namespace GatePass.UI.Data;

public class LoginFormDto
{
    [Required] public string UserName { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = false;
}
