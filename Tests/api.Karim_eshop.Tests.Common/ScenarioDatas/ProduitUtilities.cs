using api.Karim_eshop.Data.Context.Contract;
using api.Karim_eshop.Data.Entity;
using api.Karim_eshop.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Karim_eshop.Tests.Common.ScenarioDatas
{
    public static class ProduitUtilities
    {
        public static void CreateProduit(this KarimeshopDbContext Karim_eshopDBContex)
        {
            var unite = new Product {
                Id = 1,
                Name = "telephone à cadran",
                Brand = "philips",
                Description = "telephone à cadran argenté",
                PictureUrl = "telephone.png",
                QuantityInStock = 50,
                Price = 158900,
                Type = "telephone"
            };
            Karim_eshopDBContex.Products.Add(unite);
            Karim_eshopDBContex.SaveChanges();
        }

        public static void CreateProduits(this KarimeshopDbContext Karim_eshopDBContex)
        {
            var unite1 = new Product {
                Id = 2,
                Name = "pipe en bois",
                Brand = "stanwell",
                Description = "pipe en bois",
                PictureUrl = "pipe.png",
                QuantityInStock = 50,
                Price = 158900,
                Type = "divers"
            };
            var unite2 = new Product {
                Id = 3,
                Name = "armoire louis XIV",
                Brand = "armoirelux",
                Description = "armoire ancien epoque lui XIV",
                PictureUrl = "armoire.png",
                QuantityInStock = 50,
                Price = 158900,
                Type = "armoire"
            };
            Karim_eshopDBContex.Products.AddRange(unite1, unite2);
            Karim_eshopDBContex.SaveChanges();
        }
    }
}
