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
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        if (result is null)
            return NotFound($"Product with id '{id}' not found.");

        return Ok(result);
    }

    [HttpGet("product/{productName}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByName(string productName, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductsByNameQuery(productName), cancellationToken);
        return Ok(result);
    }

    [HttpGet("brand/{brandId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByBrandId(string brandId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductsByBrandIdQuery(brandId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("type/{typeId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByTypeId(string typeId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductsByTypeIdQuery(typeId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (result is null)
            return BadRequest("Failed to create product.");

        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Product ID mismatch.");

        var result = await mediator.Send(command, cancellationToken);
        if (!result)
            return BadRequest("Failed to update product.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteProductCommand(id), cancellationToken);
        if (!result)
            return NotFound($"Product with id '{id}' not found.");

        return NoContent();
    }

    // ============ Brands ============

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProductBrandsQuery(), cancellationToken);
        return Ok(result);
    }

    // ============ Types ============

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProductTypesQuery(), cancellationToken);
        return Ok(result);
    }
}