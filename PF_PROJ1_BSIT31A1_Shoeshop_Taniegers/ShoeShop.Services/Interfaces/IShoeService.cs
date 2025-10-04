using ShoeShop.Services.DTOs;

namespace ShoeShop.Services.Interfaces
{
    public interface IShoeService
    {
        Task<IEnumerable<ShoeDto>> GetAllShoesAsync();
        Task<ShoeDto?> GetShoeByIdAsync(int id);
        Task AddShoeAsync(CreateShoeDto dto);
        Task UpdateShoeAsync(int id, CreateShoeDto dto);
        Task DeleteShoeAsync(int id);
    }
}
