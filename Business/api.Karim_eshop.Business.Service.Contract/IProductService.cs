using api.Karim_eshop.Business.DTOs;
using api.Karim_eshop.Common.RequestHelpers;
using api.Karim_eshop.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Karim_eshop.Business.Service.Contract
{
    public interface IProductService
    {
        Task<CreateProductDto> CreateProductDTOAsync(Product productDto);

        Task<PagedList<Product>> GetProductsAsync(ProductParams productParams);

        Task<Product> GetProductByIdAsync(int productId);

        Task<bool> CheckProductNameExisteAsync(string productName);

        Task<bool> CheckProductIdExisteAsync(int id);

        Task<UpdateProductDto> UpdateProductAsync(Product product);
    }
}
