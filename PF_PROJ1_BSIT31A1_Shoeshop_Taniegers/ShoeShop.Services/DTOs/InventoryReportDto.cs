using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Services.DTOs
{
    public class InventoryReportDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Brand { get; set; } = string.Empty; 

        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
