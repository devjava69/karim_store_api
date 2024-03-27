using api.Karim_eshop.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Karim_eshop.Data.Repository.Contract
{
    public interface IBasketRepository
    {
        Task<Basket> RetrieveBasket(string buyerId);
    }
}
