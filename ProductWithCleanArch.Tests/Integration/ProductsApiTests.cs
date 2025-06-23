using System.Net.Http.Json;
using ProductWithCleanArch.API;
using ProductWithCleanArch.Application.Products.DTOs;
using Xunit.Abstractions;


namespace ProductWithCleanArch.Tests.Integration;

public class ProductsApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _client;

    public ProductsApiTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ShouldReturnProducts()
    {
        var response = await _client.GetAsync("/products");
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        Assert.Equal(2, products.Count());
        Assert.Equal("Book", products[0].Name);
        Assert.Equal("Pen", products[1].Name);
    }
    
    [Fact]
    public async Task ShouldAddProduct()
    {
        var response = await _client.PostAsJsonAsync("/products", new CreateProductDto("Book", 100));
        _testOutputHelper.WriteLine(response.ToString());
        response.EnsureSuccessStatusCode();
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();
        Assert.Equal("Book", product.Name);
        Assert.Equal(100, product.Price);
    }
}