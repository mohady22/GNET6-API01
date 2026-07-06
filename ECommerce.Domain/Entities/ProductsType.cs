using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class ProductsType:BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
