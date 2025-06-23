namespace ProductWithCleanArch.Application.Products.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public ProductDto(string name, int price)
    {
        Name = name;
        Price = price;
    }
}