using MediatR;
using ProductWithCleanArch.Application.Products.DTOs;

namespace ProductWithCleanArch.Application.Products.Commands;

public class CreateProductCommand : IRequest<ProductDto>
{
    public CreateProductDto Product { get; set; }
    public CreateProductCommand(CreateProductDto product)
    {
        Product = product;
    }
}