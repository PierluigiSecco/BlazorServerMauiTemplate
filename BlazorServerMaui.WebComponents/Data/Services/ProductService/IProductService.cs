using BlazorServerMaui.WebComponents.Data.Entities;

namespace BlazorServerMaui.WebComponents.Data.Services.ProductService;

public interface IProductService
{
    public List<Product> Products { get; set; }
    Task<List<Product>> GetProducts();
    Task<Product> GetProductById(int productId);
    Task<List<Product>> GetProductByCategory(string categoryUrl);
    Task<Product> CreateProduct(Product product);
}