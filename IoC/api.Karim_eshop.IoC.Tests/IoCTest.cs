using api.Karim_eshop.Data.Context.Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
//using api.Karim_eshop.IoC.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Karim_eshop.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using api.Karim_eshop.Data.Repository.Contract;
using api.Karim_eshop.Data.Repository;
using api.Karim_eshop.Business.Service.Contract;
using api.Karim_eshop.Business.Service;
using api.Karim_eshop.Data.Entity.Model;
//using api.Karim_eshop.Data.Repository.Contract;
//using api.Karim_eshop.Data.Repository;
//using api.Karim_eshop.Business.Service.Contract;
//using api.Karim_eshop.Business.Service;

namespace api.Karim_eshop.IoC.Tests
{
    public static class IoCTest
    {
        /// <summary>
        /// Configuration de l'injection des repositories du Web API RestFul
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureInjectionDependencyRepositoryTest(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;

        }

        /// <summary>
        /// Configure l'injection des services
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureInjectionDependencyServiceTest(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();

            return services;
        }

        public static IServiceCollection ConfigureDBContextTest(this IServiceCollection services)
        {
            services.AddDbContext<KarimeshopDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "TestApplication")
            );

            return services;
        }

        public static void ConfigureIdentityTest(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<KarimeshopDbContext>();
        }
    }
}
