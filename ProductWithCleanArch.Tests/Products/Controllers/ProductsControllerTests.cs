using MediatR;
using Moq;
using ProductWithCleanArch.API.Controllers;
using ProductWithCleanArch.Application.Products.DTOs;
using ProductWithCleanArch.Application.Products.Queries;

namespace ProductWithCleanArch.Tests.Products.Controllers;

public class ProductsControllerTests
{
    [Fact]
    public async Task ShouldReturnProductsWithOk()
    {
        var mockMediator = new Moq.Mock<IMediator>();

        var expectedProducts = new List<ProductDto>
        {
            new ProductDto("Book", 100),
            new ProductDto("Pen", 10)
        };
        
        mockMediator
            .Setup(m => m.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProducts);
        
        var controller = new ProductsController(mockMediator.Object);
        var result = await controller.GetProducts();
        
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(expectedProducts, result.Value);
    }
}