using AutoMapper;
using ProductWithCleanArch.Application.Products.DTOs;
using ProductWithCleanArch.Application.Products.Queries;
using MediatR;
using ProductWithCleanArch.Domain.Interfaces;

namespace ProductWithCleanArch.Application.Products.Handlers;

public class GetProductsHandler:IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProducts();
        return _mapper.Map<List<ProductDto>>(products);
    }
}