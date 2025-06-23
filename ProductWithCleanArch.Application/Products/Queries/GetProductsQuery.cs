using MediatR;
using ProductWithCleanArch.Application.Products.DTOs;

namespace ProductWithCleanArch.Application.Products.Queries;

public class GetProductsQuery : IRequest<List<ProductDto>>
{
}