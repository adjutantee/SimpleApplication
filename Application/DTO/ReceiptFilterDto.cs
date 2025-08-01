namespace Application.DTO
{
    public class ReceiptFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> DocumentNumbers { get; set; }
        public List<int> ResourceIds { get; set; }
        public List<int> UnitIds { get; set; } 
    }
}
