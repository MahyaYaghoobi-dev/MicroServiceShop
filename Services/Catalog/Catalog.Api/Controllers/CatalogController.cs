using System.Threading;
using System.Threading.Tasks;
using Catalog.Api.Helpers;
using Catalog.Application.DTOs;
using Catalog.Application.Features.Commands.CreateProduct;
using Catalog.Application.Features.Commands.DeleteProduct;
using Catalog.Application.Features.Commands.UpdateProduct;
using Catalog.Application.Features.Queries.GetAllProductBrands;
using Catalog.Application.Features.Queries.GetAllProducts;
using Catalog.Application.Features.Queries.GetAllProductTypes;
using Catalog.Application.Features.Queries.GetProductById;
using Catalog.Application.Features.Queries.GetProductsByBrandId;
using Catalog.Application.Features.Queries.GetProductsByName;
using Catalog.Application.Features.Queries.GetProductsByTypeId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

public class CatalogController(IMediator mediator) : ApiController
{
    // ============ Products ============

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet("name/{productName}")]
    public async Task<IActionResult> GetProductsByName(string productName, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductsByNameQuery(productName), cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet("brand/{brandId}")]
    public async Task<IActionResult> GetProductsByBrandId(string brandId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductsByBrandIdQuery(brandId), cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet("type/{typeId}")]
    public async Task<IActionResult> GetProductsByTypeId(string typeId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductsByTypeIdQuery(typeId), cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct( [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
         return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return result.ToActionResult();
    }

    // ============ Brands ============

    [HttpGet("brands")]
    public async Task<IActionResult> GetAllBrands(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProductBrandsQuery(), cancellationToken);
        return result.ToActionResult();
    }

    // ============ Types ============

    [HttpGet("types")]
    public async Task<IActionResult> GetAllTypes(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProductTypesQuery(), cancellationToken);
        return result.ToActionResult();
    }
}