using ShoeShop.Services.Models;

namespace ShoeShop.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ShoeId { get; set; }
        public int Quantity { get; set; }

        public Shoe Shoe { get; set; } = new Shoe();

        public string ShoeName => Shoe?.Name ?? string.Empty;
        public string Brand => Shoe?.Brand ?? string.Empty;
        public decimal Price => Shoe?.Price ?? 0m;
    }
}
