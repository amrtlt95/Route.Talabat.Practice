using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext applicationDbContext;

        public GenericRepository(ApplicationDbContext ApplicationDbContext) => applicationDbContext = ApplicationDbContext;

        public async Task<T?> GetAsync(int id) => await applicationDbContext.Set<T>().FindAsync(id) ;


        public async Task<IEnumerable<T>> GetAllAsync() => await applicationDbContext.Set<T>().AsNoTracking().ToListAsync();
    }
}
