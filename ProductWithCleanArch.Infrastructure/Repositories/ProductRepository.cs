using Microsoft.EntityFrameworkCore;
using ProductWithCleanArch.Application.Products.DTOs;
using ProductWithCleanArch.Domain.Entities;
using ProductWithCleanArch.Domain.Interfaces;
using ProductWithCleanArch.Infrastructure.Data;

namespace ProductWithCleanArch.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;
    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _dbContext.Products.ToListAsync();
    }
    
    public async Task<Product> AddProduct(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }
}