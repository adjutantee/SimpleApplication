namespace Application.DTO
{
    public class ReceiptItemDto
    {
        public int Id { get; set; }

        public int ReceiptId { get; set; }
        public int ResourceId { get; set; }
        public int UnitId { get; set; }

        public decimal Quantity { get; set; }
    }
}
