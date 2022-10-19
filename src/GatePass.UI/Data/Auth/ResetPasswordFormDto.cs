namespace GatePass.UI.Data;

public class ResetPasswordFormDto
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? DisplayName { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}
