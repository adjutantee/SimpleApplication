namespace Application.DTO
{
    public class ReceiptWithItemsDto
    {
        public ReceiptDto Receipt { get; set; }
        public List<ReceiptItemDto> Items { get; set; } = new List<ReceiptItemDto>();
    }
}
