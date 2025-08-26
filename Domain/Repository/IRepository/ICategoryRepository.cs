using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetAsync(int id);
        Task AddAsync(Category entity);
        Task UpdateAsync(Category entity);   
        Task RemoveAsync(Category entity);
        Task SaveAsync();
    }
}
