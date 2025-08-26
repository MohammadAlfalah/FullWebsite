using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repository.IRepository;

namespace Domain.Services.IService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var entities = await _categoryRepository.GetAllAsync();
            return entities.Select(ToDto).ToList();
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _categoryRepository.GetAsync(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<int> CreateAsync(CategoryDTO dto)
        {
            var entity = FromDto(dto);
            await _categoryRepository.AddAsync(entity);
            await _categoryRepository.SaveAsync();
            return entity.CategoryId;
        }

        public async Task EditAsync(CategoryDTO dto)
        {
            var entity = await _categoryRepository.GetAsync(dto.CategoryId);
            if (entity is null) return;

            entity.Name = dto.Name;
            entity.DisplayOrder = dto.DisplayOrder;

            await _categoryRepository.UpdateAsync(entity);
            await _categoryRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _categoryRepository.GetAsync(id);
            if (entity is null) return;

            await _categoryRepository.RemoveAsync(entity);
            await _categoryRepository.SaveAsync();
        }

        
        private static CategoryDTO ToDto(Category c) => new()
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            DisplayOrder = c.DisplayOrder
        };

        private static Category FromDto(CategoryDTO d) => new()
        {
            CategoryId = d.CategoryId,
            Name = d.Name,
            DisplayOrder = d.DisplayOrder
        };
    }
}
