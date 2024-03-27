using api.Karim_eshop.Data.Context.Contract;
using Microsoft.Extensions.DependencyInjection;
using api.Karim_eshop.IoC.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Karim_eshop.Data.Entity;

namespace api.Karim_eshop.Tests.Common
{
    public class TestBase
    {
        protected ServiceProvider? _serviceProvider;

        protected KarimeshopDbContext? _context;

        private void InitTestDatabase()
        {
            _context = _serviceProvider!.GetService<KarimeshopDbContext>();
            _context?.Database.EnsureDeleted();
            _context?.Database.EnsureCreated();
        }

        public void SetupTest()
        {
            _serviceProvider = new ServiceCollection()
                .ConfigureDBContextTest()
                .ConfigureInjectionDependencyRepositoryTest()
                .BuildServiceProvider();

            InitTestDatabase();
        }

        public void CleanTest()
        {
            _context?.Database.EnsureCreated();
            _serviceProvider!.Dispose();
            _context?.Dispose();
        }
    }
}
