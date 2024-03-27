using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using api.Karim_eshop.Data.Entity.Model;
using api.Karim_eshop.Data.Context.Contract;
using System.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using api.Karim_eshop.Data.Entity.Model.OrderAggregate;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace api.Karim_eshop.Data.Entity
{
    public class KarimeshopDbContext : IdentityDbContext<User, Role, int>
    {
        public KarimeshopDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(a => a.Address)
                .WithOne()
                .HasForeignKey<UserAddress>(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Role>()
                .HasData(
                    new Role { Id = 1, Name = "Member", NormalizedName = "MEMBER" },
                    new Role { Id = 2, Name = "Admin", NormalizedName = "ADMIN" }
                );
        }























        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<IdentityRole>()
        //        .HasData(
        //            new IdentityRole
        //            {
        //                Name = "Member",
        //                NormalizedName = "MEMBER",
        //            }
        //        );

        //    const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
        //    // any guid, but nothing is against to use the same one
        //    const string ROLE_ID = ADMIN_ID;
        //    builder.Entity<IdentityRole>().HasData(new IdentityRole
        //    {
        //        Id = ROLE_ID,
        //        Name = "admin",
        //        NormalizedName = "admin"
        //    });

        //    var hasher = new PasswordHasher<User>();
        //    builder.Entity<User>().HasData(new User
        //    {
        //        Id = ADMIN_ID,
        //        UserName = "admin",
        //        NormalizedUserName = "ADMIN",
        //        Email = "some-admin-email@nonce.fake",
        //        NormalizedEmail = "some-admin-email@nonce.fake",
        //        EmailConfirmed = true,
        //        PasswordHash = hasher.HashPassword(null, "SOME_ADMIN_PLAIN_PASSWORD"),
        //        SecurityStamp = string.Empty,
        //        Photo = "admin.png"
        //    });

        //    builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        //    {
        //        RoleId = ROLE_ID,
        //        UserId = ADMIN_ID
        //    });

        //    builder.Entity<Product>()
        //        .HasData(
        //            new Product
        //            {
        //                Id = 1,
        //                Name = "Table basse en Or",
        //                Description =
        //                "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
        //                Price = 20000,
        //                PictureUrl = "/images/products/table_basse_or.png",
        //                Brand = "Fluknumbluk",
        //                Type = "table basse",
        //                QuantityInStock = 100
        //            },
        //            new Product
        //            {
        //                Id = 2,
        //                Name = "Lustre en diamant",
        //                Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
        //                Price = 15000,
        //                PictureUrl = "/images/products/lustre_diamant.png",
        //                Brand = "lustrulux",
        //                Type = "Lustres",
        //                QuantityInStock = 100
        //            },
        //            new Product
        //            {
        //                Id = 3,
        //                Name = "Peignoir de douche en peau d'ours",
        //                Description =
        //                    "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
        //                Price = 18000,
        //                PictureUrl = "/images/products/peignoir_peau_ours.png",
        //                Brand = "Louis Vuitton",
        //                Type = "Vetement mixte",
        //                QuantityInStock = 100
        //            },
        //            new Product
        //            {
        //                Id = 4,
        //                Name = "robe de mariée satin bordé en or et diamant",
        //                Description =
        //                    "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
        //                Price = 30000,
        //                PictureUrl = "/images/products/robe_marie_or_diamant.png",
        //                Brand = "MariLux",
        //                Type = "Vetement femme",
        //                QuantityInStock = 100
        //            },
        //            new Product
        //            {
        //                Id = 5,
        //                Name = "Pendule en or",
        //                Description =
        //                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
        //                Price = 25000,
        //                PictureUrl = "/images/products/pendule_en_or.png",
        //                Brand = "Tissot",
        //                Type = "Horloge",
        //                QuantityInStock = 100
        //            },
        //            new Product
        //            {
        //                Id = 6,
        //                Name = "Chaussure peau crocodile",
        //                Description =
        //                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
        //                Price = 12000,
        //                PictureUrl = "/images/products/chaussure_peau_crocodile.png",
        //                Brand = "Lacoste",
        //                Type = "Chaussure homme",
        //                QuantityInStock = 100
        //            },
        //            new Product
        //            {
        //                Id = 7,
        //                Name = "Sac en peau de gazelle",
        //                Description =
        //                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
        //                Price = 1000,
        //                PictureUrl = "/images/products/sac_peau_gazelle.png",
        //                Brand = "Louis Vuitton",
        //                Type = "Maroquinerie mixte",
        //                QuantityInStock = 100
        //            },
        //            new Product
        //            {
        //                Id = 8,
        //                Name = "Armoire en bois noble",
        //                Description =
        //                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
        //                Price = 8000,
        //                PictureUrl = "/images/products/armoire_noble.png",
        //                Brand = "Noblerama",
        //                Type = "Mobilier chambre à coucher",
        //                QuantityInStock = 100
        //            },
        //            new Product
        //            {
        //                Id = 9,
        //                Name = "Cendrier en or",
        //                Description =
        //                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
        //                Price = 1500,
        //                PictureUrl = "/images/products/cendrier_en_or.png",
        //                Brand = "Cendrilux",
        //                Type = "Divers",
        //                QuantityInStock = 100
        //            },
        //            new Product
        //            {
        //                Id = 10,
        //                Name = "Pipe en bois noble",
        //                Description =
        //                    "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
        //                Price = 1800,
        //                PictureUrl = "/images/products/pipe_en_bois_noble.png",
        //                Brand = "Stanwell",
        //                Type = "Divers",
        //                QuantityInStock = 100
        //            }
        //       );
        //}

    }
}
