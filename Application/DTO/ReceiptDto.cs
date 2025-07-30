namespace Application.DTO
{
    public class ReceiptDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = String.Empty;
        public DateTime Date { get; set; }
    }
}
