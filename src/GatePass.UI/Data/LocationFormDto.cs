using System.ComponentModel.DataAnnotations;

namespace GatePass.UI.Data;

public class LocationFormDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
