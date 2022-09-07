using GatePass.Core.VisitorAggregate;
using System.ComponentModel.DataAnnotations;

namespace GatePass.UI.Data
{
    public class VisitorFormDto
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(10), Phone]
        public string Phone { get; set; } = string.Empty;
        
        public string? PhotoName { get; set; }

        [Required]
        public IdentificationType IdentificationType { get; set; } = IdentificationType.Others;

        [Required, MaxLength(50)]
        public string IdentificationNo { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Address { get; set; } = string.Empty;
    }
}
