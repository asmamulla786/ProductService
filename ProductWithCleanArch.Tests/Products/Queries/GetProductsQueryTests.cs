using AutoMapper;
using Moq;
using ProductWithCleanArch.Application.Products.DTOs;
using ProductWithCleanArch.Application.Products.Handlers;
using ProductWithCleanArch.Application.Products.Queries;
using ProductWithCleanArch.Domain.Entities;
using ProductWithCleanArch.Domain.Interfaces;

namespace ProductWithCleanArch.Tests.Products.Queries;

public class GetProductsQueryTests
{
    [Fact]
    public async Task ShouldReturnProducts()
    {
        var mockRepo = new Moq.Mock<IProductRepository>();
        var mockMapper = new Moq.Mock<IMapper>();

        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Book", Price = 100 },
            new Product { Id = Guid.NewGuid(), Name = "Pen", Price = 10 }
        };

        var resultedProducts = new List<ProductDto>
        {
            new ProductDto("Book", 100),
            new ProductDto("Pen", 10)
        };

        mockRepo
            .Setup(r => r.GetProducts())
            .ReturnsAsync(products);
        mockMapper
            .Setup(m => m.Map<List<ProductDto>>(It.IsAny<List<Product>>()))
            .Returns((List<Product> src) => resultedProducts);
        
        var handler = new GetProductsHandler(mockRepo.Object, mockMapper.Object);
        var query = new GetProductsQuery();

        var allProducts = await handler.Handle(query, CancellationToken.None);
        
        Assert.Equal(2,allProducts.Count);
        Assert.Equal("Book", allProducts[0].Name);
        Assert.Equal("Pen", allProducts[1].Name);
        Assert.Equal(100, allProducts[0].Price);
        Assert.Equal(10, allProducts[1].Price);
    }
}