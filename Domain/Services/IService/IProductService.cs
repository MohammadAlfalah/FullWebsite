using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Services.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<int> CreateProductAsync(ProductDTO dto);
        Task EditProductAsync(ProductDTO dto);
        Task DeleteProductAsync(int id);
    }
}
