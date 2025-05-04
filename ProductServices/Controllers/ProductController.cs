    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Interface;
using ProductServices.Models;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _proRepository;

        // Constructor injection of UserRepository
        public ProductController(IProduct proRepository)
        {
            _proRepository = proRepository;
        }
        [HttpPost("AddProducts")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required.");
            }

            // Ensure MerchantId is provided (you can also derive this from the logged-in merchant's details if needed)
            if (product.MerchantId <= 0)
            {
                return BadRequest("MerchantId is required.");
            }

            // Call the repository to add the product
            bool isAdded = await _proRepository.AddProduct(product);

            if (isAdded)
            {
                return CreatedAtAction(nameof(AddProduct), new { id = product.ProductId }, product);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the product.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _proRepository.GetAllProducts();
            return Ok(products); // Return products as a response
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _proRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound(new { Message = "Product not found." }); // Return 404 if product is not found
            }

            return Ok(product); // Return the product if found
        }
        [HttpGet("GetProductBYName")]
        public async Task<IActionResult> GetProductsByName([FromQuery] string productName)
        {
            var products = await _proRepository.GetProductsByName(productName);

            if (products == null || !products.Any())
            {
                return NotFound(new { Message = "No products found with the given name." });
            }

            return Ok(products);
        }
        [HttpPut("UpdateProduct")]
        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await _proRepository.UpdateProduct(product);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _proRepository.DeleteProduct(id);
            if (product)
            {
                return Ok("Product Deleted");
            }
            return NotFound(new { Message = "No products found with the given Id." });
        }

        [HttpGet("GetProductBycategory")]
        public async Task<IActionResult> GetProductByCategory([FromQuery] string category)
        {
            var products = await _proRepository.GetProductByCategory(category);
            if (products == null || !products.Any())
            {
                return NotFound($"No products found for category {category}.");
            }

            return Ok(products);
        }

        [HttpGet("type")]
        public async Task<IActionResult> GetProductByType([FromQuery] string type)
        {
            var products = await _proRepository.GetProductByType(type);
            if (products == null)
            {
                return NotFound(new { Message = "No products found" });
            }
            return Ok(products);
        }
    }
}