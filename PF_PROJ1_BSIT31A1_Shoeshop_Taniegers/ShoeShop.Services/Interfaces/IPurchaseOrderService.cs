using ShoeShop.Services.DTOs;

namespace ShoeShop.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        IEnumerable<PurchaseOrderDto> GetAllOrders();
        PurchaseOrderDto? GetOrderById(int id);
        void AddOrder(CreatePurchaseOrderDto dto);
        void UpdateOrder(int id, CreatePurchaseOrderDto dto);
        void DeleteOrder(int id);
    }
}
