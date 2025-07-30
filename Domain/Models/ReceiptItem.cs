namespace Domain.Models
{
    public class ReceiptItem
    {
        public int Id { get; set; }

        public int ReceiptId { get; set; }
        public int ResourceId { get; set; }
        public int UnitId { get; set; }

        public decimal Quantity { get; set; }
    }
}
