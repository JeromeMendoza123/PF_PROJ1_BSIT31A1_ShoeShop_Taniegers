namespace ShoeShop.Services.DTOs
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public ShoeDto Shoe { get; set; } = new ShoeDto();
        public int Quantity { get; set; }

        public decimal Total => (Shoe?.Price ?? 0m) * Quantity;
    }
}
