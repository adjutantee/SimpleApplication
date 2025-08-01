using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class ReceiptItem
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Receipt))]
        public int ReceiptId { get; set; }
        [ForeignKey(nameof(Resource))]
        public int ResourceId { get; set; }
        [ForeignKey(nameof(Unit))]
        public int UnitId { get; set; }

        public decimal Quantity { get; set; }

        // Навигационные свойства 
        public Receipt Receipt { get; set; }
        public Resource Resource { get; set; }
        public Unit Unit { get; set; }
    }
}
