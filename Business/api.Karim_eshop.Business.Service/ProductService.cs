using api.Karim_eshop.Business.DTOs;
using api.Karim_eshop.Business.Service.Contract;
using api.Karim_eshop.Common.Extensions;
using api.Karim_eshop.Common.RequestHelpers;
using api.Karim_eshop.Data.Entity.Model;
using api.Karim_eshop.Data.Repository.Contract;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Karim_eshop.Business.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateProductDto> CreateProductDTOAsync(Product product)
        {
            _logger.LogInformation("Creating Product : {@Product}", product);

            var productAdded = await _productRepository.CreateProductAsync(product).ConfigureAwait(false);

            _logger.LogInformation("Creating Product Successfull : {@ProductAdded}", productAdded);

            return _mapper.Map<CreateProductDto>(productAdded);
        }

        public async Task<PagedList<Product>> GetProductsAsync(ProductParams productParams)
        {
            var products = await _productRepository.GetAllProductsAsync(productParams);

            return products;
            //var products = await _produitRepository.GetAllProductsAsync().ConfigureAwait(false);

            //List<ProduitDto> produitListDto = new(products.Count);

            //foreach (var product in products)
            //{
            //    produitListDto.Add(_mapper.Map<ProduitDto>(product));
            //}

            //return produitListDto;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId).ConfigureAwait(false);

            return product;
        }

        public async Task<UpdateProductDto> UpdateProductAsync(Product product)
        {
            var produitUpdated = await _productRepository.UpdateProductAsync(product).ConfigureAwait(false);

            return _mapper.Map<UpdateProductDto>(produitUpdated);
        }

        public async Task<bool> CheckProductNameExisteAsync(string productName)
        {
            var getProduct = await _productRepository.GetProductByNameAsync(productName).ConfigureAwait(false);

            return getProduct != null;
        }

        public async Task<bool> CheckProductIdExisteAsync(int id)
        {
            var getProduct = await _productRepository.GetProductByIdAsync(id).ConfigureAwait(false);

            return getProduct != null;
        }
    }
}
