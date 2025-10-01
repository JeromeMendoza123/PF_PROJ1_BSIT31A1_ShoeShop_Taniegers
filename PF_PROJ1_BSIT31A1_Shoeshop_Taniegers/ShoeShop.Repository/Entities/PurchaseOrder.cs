using System;
using System.Collections.Generic;

namespace ShoeShop.Repository.Entities
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDate { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation properties
        public Supplier? Supplier { get; set; }
        public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
    }

    public enum PurchaseOrderStatus
    {
        Pending,
        Confirmed,
        Shipped,
        Received,
        Cancelled
    }
}
