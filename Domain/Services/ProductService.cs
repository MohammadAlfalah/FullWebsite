using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repository.IRepository;
using Domain.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var entities = await _productRepository.GetAllAsync();
            return entities.Select(ToDto).ToList();
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var entity = await _productRepository.GetAsync(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<int> CreateProductAsync(ProductDTO dto)
        {
            var entity = FromDto(dto);
            await _productRepository.AddAsync(entity);
            await _productRepository.SaveAsync();
            return entity.Id;
        }

        public async Task EditProductAsync(ProductDTO dto)
        {
            var entity = await _productRepository.GetAsync(dto.Id);
            if (entity is null) return;

            entity.Name = dto.Name;
            entity.OldPrice = dto.OldPrice;
            entity.NewPrice = dto.NewPrice;
            entity.CategoryId = dto.CategoryId;

            await _productRepository.UpdateAsync(entity);
            await _productRepository.SaveAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var entity = await _productRepository.GetAsync(id);
            if (entity is null) return;

            await _productRepository.RemoveAsync(entity);
            await _productRepository.SaveAsync();
        }

        private static ProductDTO ToDto(Product p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            OldPrice = p.OldPrice,
            NewPrice = p.NewPrice,
            CategoryId = p.CategoryId,
            CategoryName = p.Category?.Name    
        };

        private static Product FromDto(ProductDTO d) => new()
        {
            Id = d.Id,
            Name = d.Name,
            OldPrice = d.OldPrice,
            NewPrice = d.NewPrice,
            CategoryId = d.CategoryId
        };
    }

}
