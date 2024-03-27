using api.Karim_eshop.Common.RequestHelpers;
using api.Karim_eshop.Data.Entity.Model;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Karim_eshop.Data.Repository.Contract
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product produit);

        Task<PagedList<Product>> GetAllProductsAsync(ProductParams productParams);

        Task<Product> GetProductByIdAsync(int productId);

        Task<Product> UpdateProductAsync(Product product);

        Task<Product> GetProductByNameAsync(string name);
    }
}
