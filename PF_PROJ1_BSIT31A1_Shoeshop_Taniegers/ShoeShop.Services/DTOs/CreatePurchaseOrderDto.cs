namespace ShoeShop.Services.DTOs
{
    public class CreatePurchaseOrderDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string? Notes { get; set; }
    }
}
