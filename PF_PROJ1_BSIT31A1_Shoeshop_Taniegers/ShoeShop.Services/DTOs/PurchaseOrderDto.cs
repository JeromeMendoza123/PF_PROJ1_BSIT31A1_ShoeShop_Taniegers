namespace ShoeShop.Services.DTOs
{
    public class PurchaseOrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
