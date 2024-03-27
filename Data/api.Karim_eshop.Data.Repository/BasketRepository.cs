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
    public class BasketRepository : IBasketRepository
    {
        private readonly KarimeshopDbContext _context;

        public BasketRepository(KarimeshopDbContext context)
        {
            _context = context;
        }

        public async Task<Basket> RetrieveBasket(string buyerId)
        {
            return await _context.Baskets
                            .Include(i => i.Items)
                            .ThenInclude(p => p.Product)
                            .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
        }

        public async Task<Basket> CreateBasket(Basket basket)
        {
            var elementAdded = await _context.Baskets.AddAsync(basket);
            _context.SaveChangesAsync();

            return elementAdded.Entity;
        }
    }
}
