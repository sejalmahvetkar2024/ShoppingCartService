using CartService.Models;
using Microsoft.AspNetCore.Mvc;

//cart service using CartService.Repository;
using Microsoft.AspNetCore.Http;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _cartRepository;

        public CartController(ICart cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // 1️⃣ Get cart items for a specific user
        [HttpGet("{userId:long}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartItems(long userId)
        {
            var cartItems = await _cartRepository.GetCartItemsAsync(userId);
            return Ok(cartItems);
        }

        // 2️⃣ Add a new item to the cart
        [HttpPost]
        public async Task<ActionResult> AddToCart(Cart cartItem)
        {
            await _cartRepository.AddToCartAsync(cartItem);
            return CreatedAtAction(nameof(GetCartItems), new { userId = cartItem.UserId }, cartItem);
        }

        // 3️⃣ Update an item in the cart (Change quantity)
        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(long cartItemId, Cart cartItem)
        {
            if (cartItemId != cartItem.CartId)
            {
                return BadRequest();
            }

            await _cartRepository.UpdateCartItemAsync(cartItem);
            return NoContent();
        }

        // 4️⃣ Remove an item from the cart
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(long cartItemId)
        {
            await _cartRepository.RemoveFromCartAsync(cartItemId);
            return NoContent();
        }

        // 5️⃣ Clear all cart items for a user
        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(long userId)
        {
            await _cartRepository.ClearCartAsync(userId);
            return NoContent();
        }
    }
}