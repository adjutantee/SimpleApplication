namespace Application.DTO
{
    public class ReceiptFilterDto
    {
        private DateTime? _startDate;
        private DateTime? _endDate;

        public DateTime? StartDate
        {
            get => _startDate;
            set => _startDate = value.HasValue
                ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
                : null;
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set => _endDate = value.HasValue
                ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
                : null;
        }

        public List<string> DocumentNumbers { get; set; }
        public List<int> ResourceIds { get; set; }
        public List<int> UnitIds { get; set; } 
    }
}
