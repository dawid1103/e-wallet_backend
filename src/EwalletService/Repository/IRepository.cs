﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwalletService.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<int> CreateAsync(T entity);

        Task DeleteAsync(int id);

        Task EditAsync(T entity);
    }
}
