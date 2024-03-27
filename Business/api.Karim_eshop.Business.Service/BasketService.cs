using api.Karim_eshop.Business.DTOs;
using api.Karim_eshop.Business.Service.Contract;
using api.Karim_eshop.Data.Entity.Model;
using api.Karim_eshop.Data.Repository.Contract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Karim_eshop.Business.Service
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<Basket> RetrieveBasket(string buyerId)
        {
            var basket = await _basketRepository.RetrieveBasket(buyerId);

            return basket;
        }
    }
}
