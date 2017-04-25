using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletServices.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<int> AddAsync(T entity);

        Task DeleteAsync(int id);

        Task EditAsync(T entity);
    }
}
