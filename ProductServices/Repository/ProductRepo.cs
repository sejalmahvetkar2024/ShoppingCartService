using Microsoft.EntityFrameworkCore;

using ProductServices.Interface;

using ProductServices.Models;

namespace ProductServices.Repository

{

    public class ProductRepo : IProduct

    {

        private readonly ShoppingCartContext _context;

        public ProductRepo(ShoppingCartContext context)

        {

            _context = context;

        }


        public async Task<bool> AddProduct(Product product)

        {

            if (product.MerchantId <= 0)

            {

                return false; // Ensure that MerchantId is valid

            }

            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync(); // Save the changes to the database

            return true;

        }


        public async Task<IEnumerable<Product>> GetAllProducts()

        {

            return await _context.Products.ToListAsync(); // Fetches all products from the Products table

        }

        public async Task<Product> GetProductById(int id)

        {

            var existingProfile = await _context.Products.FirstOrDefaultAsync(user => user.ProductId == id);

            if (existingProfile != null)

            {

                return existingProfile;

            }

            return null;

        }

        public async Task<IEnumerable<Product>> GetProductsByName(string ProductName)

        {

            return await _context.Products

                .Where(p => p.ProductName.Contains(ProductName)) // Filters products where name contains the given string

                .ToListAsync(); // Fetches matching products asynchronously

        }

        public async Task<bool> UpdateProduct(Product product)

        {

            var existingProduct = await _context.Products.FindAsync(product.ProductId);

            if (existingProduct == null)

            {

                return false;

            }

            // Update product fields

            existingProduct.ProductName = product.ProductName;

            existingProduct.Category = product.Category;

            existingProduct.Price = product.Price;

            existingProduct.Rating = product.Rating;

            existingProduct.Image = product.Image;

            existingProduct.Description = product.Description;

            existingProduct.Producttype = product.Producttype;

            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> DeleteProduct(int id)

        {

            var res = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (res == null)

            {

                return false;

            }

            _context.Products.Remove(res);

            await _context.SaveChangesAsync(); // Save the changes to the database

            return true;

        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string Category)

        {

            return await _context.Products

               .Where(p => p.Category.Contains(Category))

               .ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductByType(string type)

        {

            return await _context.Products

                .Where(p => p.Producttype.Contains(type))

                .ToListAsync();

        }

    }

}

