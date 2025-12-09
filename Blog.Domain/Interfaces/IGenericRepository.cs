using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> AddAsync(T item);
        Task<T> GetByIdAsync(string id);
        Task<T> GetAllAsync();
        void Update(T item);
        void Delete(T item);

    }
}
