using Catalog.Application.DTOs;
using Catalog.Application.Shared.Results;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository,IBrandRepository brandRepository, ITypeRepository typeRepository, IMapper mapper):IRequestHandler<CreateProductCommand,Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var brand=await brandRepository.GetProductBrandByIdAsync(request.BrandId,cancellationToken);
        var type=await typeRepository.GetProductTypeByIdAsync(request.TypeId,cancellationToken);

        if (brand is null)
            return Result<ProductDto>.Failure("Brand not found", ResultType.NotFound);

        if (type is null)
            return Result<ProductDto>.Failure("Type not found", ResultType.NotFound);
        
        var product = mapper.Map<Product>(request);
        var result = await productRepository.CreateProductAsync(product, cancellationToken);

        if (result is null)
            return Result<ProductDto>.Failure("Failed to create product.", ResultType.BadRequest);
        product.Brand = brand;
        product.Type = type;    
        var productDto=mapper.Map<ProductDto>(result);
        return Result<ProductDto>.Success(productDto);
        
        
    }
}