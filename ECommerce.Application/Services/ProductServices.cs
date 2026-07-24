using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Params;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductServices(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllProductBrandsAsync(CancellationToken ct = default)
        {
            var brands = await unitOfWork.GetRepository<ProductsBrand,int>().GetAllAsync(ct);
            var mappedBrands = mapper.Map<IReadOnlyList<ProductsBrand>,IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(mappedBrands);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default)
        {
            var spec = new ProductSpecifications(queryParams);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecificationAsync(spec,ct);
            var mappedProducts = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            return Result<IReadOnlyList<ProductDto>>.Ok(mappedProducts);
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllProductTypesAsync(CancellationToken ct = default)
        {
            var types = await unitOfWork.GetRepository<ProductsType, int>().GetAllAsync(ct);
            var mappedTypes = mapper.Map<IReadOnlyList<ProductsType>, IReadOnlyList<TypeDto>>(types);
            return Result<IReadOnlyList<TypeDto>>.Ok(mappedTypes);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new ProductSpecifications(id);
            var products = await unitOfWork.GetRepository<Product,int>().GetByIdWithSpecificationsAsync(spec,ct);

            if (products == null)
                return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound", $"Product With Id: {id} is not found."));

            var mappedProduct = mapper.Map<Product, ProductDto>(products);
            return Result<ProductDto>.Ok(mappedProduct);
        }
    }
}
