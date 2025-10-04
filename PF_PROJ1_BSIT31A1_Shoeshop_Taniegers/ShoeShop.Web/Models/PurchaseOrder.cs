namespace ShoeShop.Web.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public int ShoeId { get; set; }
        public string ShoeName { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
