using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface IProductServices
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<BrandDto>>> GetAllProductBrandsAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllProductTypesAsync(CancellationToken ct = default);
        Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default);
    }
}
