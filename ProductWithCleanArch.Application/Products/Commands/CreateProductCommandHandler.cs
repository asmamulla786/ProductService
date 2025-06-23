using AutoMapper;
using MediatR;
using ProductWithCleanArch.Application.Products.DTOs;
using ProductWithCleanArch.Domain.Entities;
using ProductWithCleanArch.Domain.Interfaces;

namespace ProductWithCleanArch.Application.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.Product);

        product = await _productRepository.AddProduct(product);
        return _mapper.Map<ProductDto>(product);
    }
}