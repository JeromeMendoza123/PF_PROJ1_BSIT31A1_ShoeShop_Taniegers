using ShoeShop.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace ShoeShop.Services
{
    public class CartService
    {
        private readonly List<CartItem> _cartItems = new();

        public IEnumerable<CartItem> GetCart() => _cartItems;

        public void AddToCart(ShoeDto shoe, int quantity)
        {
            var existing = _cartItems.FirstOrDefault(c => c.Shoe.Id == shoe.Id);
            if (existing != null) existing.Quantity += quantity;
            else _cartItems.Add(new CartItem { Shoe = shoe, Quantity = quantity });
        }

        public void RemoveFromCart(int shoeId)
        {
            var item = _cartItems.FirstOrDefault(c => c.Shoe.Id == shoeId);
            if (item != null) _cartItems.Remove(item);
        }

        public void ClearCart() => _cartItems.Clear();
    }

    public class CartItem
    {
        public ShoeDto Shoe { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
