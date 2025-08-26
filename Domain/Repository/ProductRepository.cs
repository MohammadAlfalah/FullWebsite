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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task AddAsync(Product product)
        {
            await _db.Products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Products.Update(product);

        }
        public async Task RemoveAsync(Product product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetAsync(int id)
        {
            return await _db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
