using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;

namespace Domain.Services.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(CategoryDTO dto);
        Task EditAsync(CategoryDTO dto);
        Task DeleteAsync(int id);
    }
}
