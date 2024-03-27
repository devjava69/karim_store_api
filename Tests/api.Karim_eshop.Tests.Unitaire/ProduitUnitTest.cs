using api.Karim_eshop.Common.RequestHelpers;
using api.Karim_eshop.Data.Entity.Model;
using api.Karim_eshop.Data.Repository;
using api.Karim_eshop.Data.Repository.Contract;
using api.Karim_eshop.Tests.Common;
using api.Karim_eshop.Tests.Common.ScenarioDatas;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Karim_eshop.Tests.Unitaire
{
    [TestFixture]
    public class ProduitUnitTest : TestBase
    {
        private IProductRepository _productRepository;
        [SetUp]
        public void Setup()
        {
            SetupTest();

            IProductRepository? productRepository = _serviceProvider?.GetService<IProductRepository>();
            _productRepository = productRepository!;

            _context!.CreateProduit();
        }

        [TearDown]
        public void Teardown()
        {
            CleanTest();
        }

        [Test]
        public async Task CreateProduit()
        {
            // Arrange
            var produit = new Product()
            {
                Id = 2,
                Name = "Mixeur",
                Description = "Mixeur pour bébé",
                Price = 49,
                PictureUrl = "mixeur.jpeg",
                Brand = "electrolux",
                Type = "mixeur",
            };

            // Act
            var productToAdd = await _productRepository.CreateProductAsync(produit).ConfigureAwait(false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(productToAdd, Is.Not.Null);
                Assert.That(produit.Name, Is.EqualTo(productToAdd.Name));
            });
        }

        //[Test]
        //public async Task GetProducts(ProductParams productParams)
        //{
        //    var produits = await _productRepository.GetAllProductsAsync(productParams).ConfigureAwait(false);

        //    Assert.That(produits, Is.Not.Null);
        //}

        [Test]
        public async Task GetProductById()
        {
            int id = 1;
            var produit = await _productRepository.GetProductByIdAsync(id).ConfigureAwait(false);

            Assert.That(id, Is.EqualTo(produit.Id));
        }
    }
}
