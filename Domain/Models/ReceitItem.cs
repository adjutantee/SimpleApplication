namespace Domain.Models
{
    public class ReceitItem
    {
        public int Id { get; set; }

        public int ReceitId { get; set; }
        public int ResourceId { get; set; }
        public int UnitId { get; set; }

        public decimal Quantity { get; set; }
    }
}
