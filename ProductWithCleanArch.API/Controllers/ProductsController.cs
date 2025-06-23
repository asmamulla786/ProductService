using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductWithCleanArch.Application.Products.Commands;
using ProductWithCleanArch.Application.Products.DTOs;
using ProductWithCleanArch.Application.Products.Queries;

namespace ProductWithCleanArch.API.Controllers;

[ApiController]
[Route("products")]
public class ProductsController:ControllerBase
{
    private readonly IMediator _mediator;
    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<OkObjectResult> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }

    [HttpPost]
    public async Task<OkObjectResult> AddProduct([FromBody] CreateProductDto productDto)
    {
        var product = await _mediator.Send(new CreateProductCommand(productDto));
        return Ok(product);
    }
}