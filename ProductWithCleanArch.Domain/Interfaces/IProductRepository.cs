using ProductWithCleanArch.Domain.Entities;

namespace ProductWithCleanArch.Domain.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProducts();
    Task<Product> AddProduct(Product product);
}