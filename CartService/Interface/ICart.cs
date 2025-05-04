using CartService.Models;

public interface ICart
{
    Task<IEnumerable<Cart>> GetCartItemsAsync(long userId);
    Task<Cart?> GetCartItemByIdAsync(long cartItemId);
    Task AddToCartAsync(Cart cartItem);
    Task UpdateCartItemAsync(Cart cartItem);
    Task RemoveFromCartAsync(long cartItemId);
    Task ClearCartAsync(long userId);
}