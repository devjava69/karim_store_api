using api.Karim_eshop.Business.DTOs;
using api.Karim_eshop.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Karim_eshop.Business.Service.Contract
{
    public interface IBasketService
    {
        Task<Basket> RetrieveBasket(string buyerId);
    }
}
