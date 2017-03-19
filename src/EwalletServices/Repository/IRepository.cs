using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletServices.Repository
{
    interface IRepository<T>
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> ListAsync();

        Task<int> AddAsync(T entity);

        Task DeleteAsync(int id);

        Task EditAsync(T entity);
    }
}
