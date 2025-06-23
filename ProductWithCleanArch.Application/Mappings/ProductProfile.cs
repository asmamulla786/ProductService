using AutoMapper;
using ProductWithCleanArch.Application.Products.DTOs;
using ProductWithCleanArch.Domain.Entities;

namespace ProductWithCleanArch.Application.Mappings;

public class ProductProfile:Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
    }
}