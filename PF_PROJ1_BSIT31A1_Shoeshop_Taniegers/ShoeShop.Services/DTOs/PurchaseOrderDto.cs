using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Services.DTOs
{
    public class PurchaseOrderDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
