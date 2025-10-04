namespace ShoeShop.Services.DTOs
{
    public class ShoeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Brand { get; set; } = "";
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public string? ImageUrl { get; set; }
    }
}
