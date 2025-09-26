using System;
using System.Collections.Generic;

namespace ShoeShop.Repository.Entities
{
    public class Shoe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation property
        public ICollection<ShoeColorVariation> ColorVariations { get; set; } = new List<ShoeColorVariation>();
    }
}
