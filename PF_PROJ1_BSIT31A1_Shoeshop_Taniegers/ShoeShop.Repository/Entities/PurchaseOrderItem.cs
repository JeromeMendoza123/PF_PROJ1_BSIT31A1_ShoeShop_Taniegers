using System;

namespace ShoeShop.Repository.Entities
{
    public class PurchaseOrderItem
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ShoeColorVariationId { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal UnitCost { get; set; }

        // Navigation properties
        public PurchaseOrder? PurchaseOrder { get; set; }
        public ShoeColorVariation? ShoeColorVariation { get; set; }
    }
}
