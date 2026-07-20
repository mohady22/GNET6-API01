using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repo = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;

            if (_Repo.TryGetValue(typeName, out object oldRepos))
            {
                return (IGenericRepository<TEntity, TKey>) oldRepos;
                               
            }
            var newRepo = new GenericRepository<TEntity, TKey>(dbContext);

            _Repo[typeName] = newRepo;
            return newRepo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await dbContext.SaveChangesAsync(ct);
        }
    }
}
