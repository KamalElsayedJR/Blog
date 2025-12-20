using Blog.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {



        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec);
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
    }
} 
