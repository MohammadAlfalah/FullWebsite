using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;
using Domain.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Categories
                            .OrderBy(c => c.DisplayOrder)
                            .ToListAsync();
        }

        public async Task<Category?> GetAsync(int id)
        {
            return await _db.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task AddAsync(Category entity)
        {
            await _db.Categories.AddAsync(entity);
        }

        public Task UpdateAsync(Category entity)
        {
            _db.Categories.Update(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Category entity)
        {
            _db.Categories.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
