using System;
using System.Collections.Generic;

namespace ShoeShop.Repository.Entities
{
    public class ShoeColorVariation
    {
        public int Id { get; set; }
        public int ShoeId { get; set; }
        public string ColorName { get; set; } = string.Empty;
        public string HexCode { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; } = 5;
        public bool IsActive { get; set; }

        // Navigation property
        public Shoe? Shoe { get; set; }
        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
        public ICollection<StockPullOut> StockPullOuts { get; set; } = new List<StockPullOut>();
    }
}
