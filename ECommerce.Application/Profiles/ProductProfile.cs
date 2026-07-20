using AutoMapper;
using ECommerce.Application.DTOs.Products;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductsBrand,BrandDto>();
            CreateMap<ProductsType,TypeDto>();

            CreateMap<Product,ProductDto>()
                .ForMember(dist => dist.BrandName,opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dist => dist.TypeName,opt => opt.MapFrom(src => src.Type.Name));

        }
    }
}
