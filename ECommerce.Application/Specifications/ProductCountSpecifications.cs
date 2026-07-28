using ECommerce.Application.Params;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Specifications
{
    public class ProductCountSpecifications:BaseSpecifications<Product,int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams)
            : base(p => (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId)
            && (!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId)
            && (string.IsNullOrEmpty(queryParams.searchValue) || p.Name.ToLower().Contains(queryParams.searchValue.ToLower())))
        {
            
        }
    }
}
