﻿using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Specifications;
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
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext DbContext)
        {
            _dbContext = DbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Product))
            //{
            //    return (IEnumerable<T>)await _dbContext.Products.Include(p => p.Brand).Include(p => p.Category).AsNoTracking().ToListAsync();

            //}
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(BaseSpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            //if (typeof(T) == typeof(Product))
            //{
            //    return await _dbContext.Products.Where(p=> p.Id == id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;

            //}
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetWithSpecAsync(BaseSpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).FirstOrDefaultAsync();
        }
        #region Helper methods
        private IQueryable<T> ApplySpecifications(BaseSpecifications<T> specifications)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), specifications);
        }
        #endregion
    }
}
