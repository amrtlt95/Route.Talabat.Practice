﻿using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetWithSpecAsync(BaseSpecifications<T> specifications);
        Task<IEnumerable<T>> GetAllWithSpecAsync(BaseSpecifications<T> specifications);
    }
}
