namespace ProductWithCleanArch.Application.Products.DTOs;

public class CreateProductDto
{
    public string Name { get; set; }
    public int Price { get; set; }
    public CreateProductDto(string name, int price) 
    {
        Name = name;
        Price = price;
    }
}