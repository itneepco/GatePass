using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GatePass.UI.Data;

public class SinglePassFormDto
{
    [Required]
    public DateTime? VisitDate { get; set; } = DateTime.Today;
    
    [Required]
    public TimeSpan? InTime { get; set; } = DateTime.Now.TimeOfDay;
    public TimeSpan? OutTime { get; set; }
    
    [Required]
    public string Purpose { get; set; } = string.Empty;
    
    [Required, DisplayName("Officer to visit")]
    public string OfficerToVisit { get; set; } = string.Empty;
    
    [Required]
    public string Department { get; set; } = string.Empty;
    public int NoOfCompanions { get; set; }
    
    public Guid VisitorId { get; set; }
}
