using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> InputQuery,
            ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery;

            if (specifications.IncludeExpressions.Count > 0)
            {
                query = specifications.IncludeExpressions.Aggregate(query, (current, experssion) => current.Include(experssion
                    ));
            }

            if (specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }
            if (specifications.OrderBy is not null)
            {
                query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDesc is not null)
            {
                query.OrderByDescending(specifications.OrderByDesc);
            }

            return query;
        }
    }
}
