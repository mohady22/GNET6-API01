using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Common
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(IReadOnlyList<TEntity> data, int pageIndex, int pageSize, int count)
        {
            this.data = data;
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.count = count;
        }

        public IReadOnlyList<TEntity> data { get; set; } = [];

        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int count { get; set; }
    }
}
