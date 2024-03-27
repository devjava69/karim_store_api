using api.Karim_eshop.Business.DTOs;
using api.Karim_eshop.Common.Extensions;
using api.Karim_eshop.Common.RequestHelpers;
using api.Karim_eshop.Data.Context.Contract;
using api.Karim_eshop.Data.Entity;
using api.Karim_eshop.Data.Entity.Model;
using api.Karim_eshop.Data.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Karim_eshop.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly KarimeshopDbContext _context;

        public ProductRepository(KarimeshopDbContext karim_EshopDBContext)
        {
            _context = karim_EshopDBContext;
        }

        public async Task<Product> CreateProductAsync(Product produit)
        {
            var elementAdded = await _context.Products.AddAsync(produit).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return elementAdded.Entity;
        }

        public async Task<PagedList<Product>> GetAllProductsAsync(ProductParams productParams)
        {
            var query = _context.Products
                .Sort(productParams.OrderBy)
                .Search(productParams.SearchTerm)
                .Filter(productParams.Brands, productParams.Types)
                .AsQueryable();

            return await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(product => product.Name == name)
                .ConfigureAwait(false);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var elementUpdated = _context.Products.Update(product);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return elementUpdated.Entity;
        }
    }
}
