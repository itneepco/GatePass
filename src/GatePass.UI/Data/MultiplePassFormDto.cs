namespace GatePass.UI.Data
{
    public class MultiplePassFormDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public Guid VisitorId { get; set; }
    }
}
