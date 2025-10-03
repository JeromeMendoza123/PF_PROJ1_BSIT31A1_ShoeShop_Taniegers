using ShoeShop.Repository.Entities;

namespace ShoeShop.Services
{
    public class InMemoryShoeService
    {
        private readonly List<Shoe> _shoes = new List<Shoe>
        {
            new Shoe { Id = 1, Name = "Air Max 90", Brand = "Nike", Price = 120m, ImageUrl = "/images/nike_airmax90.jpg" },
            new Shoe { Id = 2, Name = "UltraBoost", Brand = "Adidas", Price = 140m, ImageUrl = "/images/adidas_ultraboost.jpg" },
            new Shoe { Id = 3, Name = "Classic Leather", Brand = "Reebok", Price = 100m, ImageUrl = "/images/reebok_classic.jpg" }
        };

        public IEnumerable<Shoe> GetAllShoes() => _shoes;

        public Shoe? GetShoeById(int id) => _shoes.FirstOrDefault(s => s.Id == id);

        public void AddShoe(Shoe shoe)
        {
            shoe.Id = _shoes.Count + 1;
            _shoes.Add(shoe);
        }
    }
}
