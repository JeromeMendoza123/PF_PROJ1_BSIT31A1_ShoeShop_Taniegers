using System;

namespace ShoeShop.Repository.Entities
{
    public class StockPullOut
    {
        public int Id { get; set; }
        public int ShoeColorVariationId { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? ReasonDetails { get; set; }
        public string RequestedBy { get; set; } = string.Empty;
        public string? ApprovedBy { get; set; }
        public DateTime PullOutDate { get; set; }
        public StockPullOutStatus Status { get; set; }

        // Navigation property
        public ShoeColorVariation? ShoeColorVariation { get; set; }
    }

    public enum StockPullOutStatus
    {
        Pending,
        Approved,
        Completed,
        Rejected
    }
}

// Stock Pull out set