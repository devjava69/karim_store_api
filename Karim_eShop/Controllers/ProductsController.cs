using api.Karim_eshop.Business.DTOs;
using api.Karim_eshop.Business.Service;
using api.Karim_eshop.Business.Service.Contract;
using api.Karim_eshop.Common.Extensions;
using api.Karim_eshop.Common.RequestHelpers;
using api.Karim_eshop.Data.Entity;
using api.Karim_eshop.Data.Entity.Model;
using api.Karim_eshop.Data.Repository.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Karim_eShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly KarimeshopDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ProductsController> _logger;
        private readonly ImageService _imageService;
        private readonly IProductService _productService;

        public ProductsController(KarimeshopDbContext context, IMapper mapper,
            UserManager<User> userManager, ILogger<ProductsController> logger, ImageService imageService,
            IProductService productService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(ProductsController));
            _imageService = imageService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery] ProductParams productParams)
        {
            //var query = _context.Products
            //    .Sort(productParams.OrderBy)
            //    .Search(productParams.SearchTerm)
            //    .Filter(productParams.Brands, productParams.Types)
            //    .AsQueryable();

            //var products =
            //    await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

            var products = await _productService.GetProductsAsync(productParams);

            Response.AddPaginationHeader(products.MetaData);

            return products;
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            //var product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var brands = await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
            var types = await _context.Products.Select(p => p.Type).Distinct().ToListAsync();

            return Ok(new { brands, types });
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CreateProductDto>> CreateProduct([FromForm] CreateProductDto productDto)
        {
            if(productDto is null)
            {
                _logger.LogError("Creating Product failed", productDto);
            }

            var product = _mapper.Map<Product>(productDto);

            if(productDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(productDto.File);

                if(imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                product.PictureUrl = imageResult.SecureUri.ToString();
                product.PublicId = imageResult.PublicId;
            }

            await _productService.CreateProductDTOAsync(product);

            var isResult = await _productService.CheckProductNameExisteAsync(productDto.Name);

            if (isResult) return CreatedAtRoute("GetProduct", new { Id = product.Id }, product);

            return BadRequest(new ProblemDetails { Title = "Problem creating new product" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct([FromForm] UpdateProductDto productDto)
        {
            var product = await _productService.GetProductByIdAsync(productDto.Id);

            if (product == null) return NotFound();

            _mapper.Map(productDto, product);

            if (productDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(productDto.File);

                if(imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title= imageResult.Error.Message });

                if(!string.IsNullOrEmpty(product.PublicId))
                    await _imageService.DeleteImageAsync(product.PublicId);

                product.PictureUrl = imageResult.SecureUri.ToString();
                product.PublicId = imageResult.PublicId;
            }

            await _productService.UpdateProductAsync(product);

            var isResult = await _productService.CheckProductNameExisteAsync(productDto.Name);

            if (isResult) return Ok(product);

            return BadRequest(new ProblemDetails { Title = "Problem updating product" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            if (!string.IsNullOrEmpty(product.PublicId))
                await _imageService.DeleteImageAsync(product.PublicId);

            _context.Products.Remove(product);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting product" });
        }
    }
}
