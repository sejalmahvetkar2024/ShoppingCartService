using Microsoft.EntityFrameworkCore;
using CartService.Models;
using System;
namespace CartService.Repository
{
    public class CartRepo : ICart
    {
        private readonly ShoppingCartContext _context;
        public CartRepo(ShoppingCartContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cart>> GetCartItemsAsync(long userId)
        {
            return await _context.Carts
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        // 2️⃣ Get a single cart item by ID
        public async Task<Cart?> GetCartItemByIdAsync(long cartItemId)
        {
            return await _context.Carts.FindAsync(cartItemId);
        }

        // 3️⃣ Add an item to the cart
        public async Task AddToCartAsync(Cart cartItem)
        {
            await _context.Carts.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        // 4️⃣ Update cart item quantity
        public async Task UpdateCartItemAsync(Cart cartItem)
        {
            //_context.Entry(cartItem).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            var updatecart = await _context.Carts.FindAsync(cartItem.CartId);

            if (updatecart != null)
            {
                updatecart.TotalPrice = cartItem.TotalPrice; // Update quantity
                await _context.SaveChangesAsync(); // Save changes to the database
            }
        }

        // 5️⃣ Remove a specific item from the cart
        public async Task RemoveFromCartAsync(long cartItemId)
        {
            var cartItem = await _context.Carts.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        // 6️⃣ Clear all cart items for a user
        public async Task ClearCartAsync(long userId)
        {
            var cartItems = _context.Carts.Where(c => c.UserId == userId);
            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }



    }
}
