using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Params
{
    public class ProductQueryParams
    {
        public int? brandId {  get; set; }
        public int? typeId { get; set; }
        public string? searchValue { get; set; }
        public ProductSortingOptions sort {  get; set; }

        public int PageIndex { get; set; } = 1;

        private const int defaultPageSize = 5;
        private const int maxPageSize = 10;
        private int pageSize = defaultPageSize;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > maxPageSize ? maxPageSize : (value < 1 ? defaultPageSize : value);
        }
    }
}
