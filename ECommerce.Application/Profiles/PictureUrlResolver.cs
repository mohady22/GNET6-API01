using AutoMapper;
using AutoMapper.Configuration.Annotations;
using AutoMapper.Execution;
using ECommerce.Application.DTOs.Products;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Profiles
{
    public class PictureUrlResolver(IOptions<UrlSettings> options) : IValueResolver<Product, ProductDto, string?>
    {
        private readonly UrlSettings urlSettings= options.Value;
        public string? Resolve(Product source, ProductDto destination, string? destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl)) return null;
            var BaseUrl = urlSettings.BaseUrl.TrimEnd('/');
            var path = source.PictureUrl.TrimEnd('/');

            return $"*{BaseUrl}/Files/{path}";
            
        }
    }
    public class UrlSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
    }
}
