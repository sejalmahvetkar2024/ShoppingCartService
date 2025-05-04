using ProductServices.Models;

namespace ProductServices.Interface
{
    public interface IProduct
    {
        Task<bool> AddProduct(Product product);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProductsByName(string ProductName);

        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);

        Task<IEnumerable<Product>> GetProductByCategory(string Category);
        Task<IEnumerable<Product>> GetProductByType(string type);
    }
}