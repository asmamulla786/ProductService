using AutoMapper;
using Moq;
using ProductWithCleanArch.Application.Products.Commands;
using ProductWithCleanArch.Application.Products.DTOs;
using ProductWithCleanArch.Domain.Entities;
using ProductWithCleanArch.Domain.Interfaces;

namespace ProductWithCleanArch.Tests.Products.Commands;

public class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task ShouldAddProductAndReturnIt()
    {
        var mockRepo = new Mock<IProductRepository>();
        var mockMapper = new Mock<IMapper>();
        
        var productDto = new CreateProductDto("Book", 100);
        var productEntity = new Product{Name = "Book", Price = 100};
        var addedProduct = new Product{Id = Guid.NewGuid(), Name = "Book", Price = 100};
        var returnedProduct = new ProductDto( "Book",  100);
        
        mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(productEntity);
        mockRepo.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync(addedProduct);
        mockMapper.Setup(m => m.Map<ProductDto>(addedProduct)).Returns(()=>returnedProduct);
        
        var handler = new CreateProductCommandHandler(mockRepo.Object, mockMapper.Object);
        var command = new CreateProductCommand(productDto);
        
        var result =await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal("Book", result.Name);
        Assert.Equal(100, result.Price);
    }
    
    // [Fact]
    // public async Task ShouldReturnNullIfProductCreationFails()
    // {
    //     var mockRepo = new Mock<IProductRepository>();
    //     var mockMapper = new Mock<IMapper>();
    //
    //     var productDto = new CreateProductDto("Pen", 10);
    //     var productEntity = new Product { Name = "Pen", Price = 10 };
    //
    //     mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(productEntity);
    //     mockRepo.Setup(r => r.AddProduct(It.IsAny<Product>())).ReturnsAsync((Product?)null);
    //
    //     var handler = new CreateProductCommandHandler(mockRepo.Object, mockMapper.Object);
    //     var command = new CreateProductCommand(productDto);
    //
    //     var result = await handler.Handle(command, CancellationToken.None);
    //
    //     Assert.Null(result);
    // }

}