using Catalog.Application.Shared.Results;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository productRepository,IBrandRepository brandRepository,ITypeRepository typeRepository,IMapper mapper):IRequestHandler<UpdateProductCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var brand=await brandRepository.GetProductBrandByIdAsync(request.BrandId,cancellationToken);
        var type=await typeRepository.GetProductTypeByIdAsync(request.TypeId,cancellationToken);

        if (brand is null)
            return Result<bool>.Failure("Brand not found", ResultType.NotFound);

        if (type is null)
            return Result<bool>.Failure("Type not found", ResultType.NotFound);
        
        var product = mapper.Map<Product>(request);
        var result = await productRepository.UpdateProductAsync(product, cancellationToken);

        if (result is false)
            return Result<bool>.Failure("Failed to update product.", ResultType.BadRequest);
        
        return Result<bool>.Success(true);
    }
}