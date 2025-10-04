using ShoeShop.Services.DTOs;
using ShoeShop.Services.Interfaces;

namespace ShoeShop.Services.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        // ✅ Static so it persists across requests
        private static readonly List<PurchaseOrderDto> _orders = new();
        private static int _nextId = 1; // Auto-increment ID

        public IEnumerable<PurchaseOrderDto> GetAllOrders()
        {
            return _orders;
        }

        public PurchaseOrderDto? GetOrderById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public void AddOrder(CreatePurchaseOrderDto dto)
        {
            var order = new PurchaseOrderDto
            {
                Id = _nextId++, // auto-increment
                CustomerName = dto.CustomerName,
                Total = dto.Total,
                Notes = dto.Notes,
                CreatedDate = DateTime.UtcNow
            };
            _orders.Add(order);
        }

        public void UpdateOrder(int id, CreatePurchaseOrderDto dto)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return;

            order.CustomerName = dto.CustomerName;
            order.Total = dto.Total;
            order.Notes = dto.Notes;
        }

        public void DeleteOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
                _orders.Remove(order);
        }
    }
}
