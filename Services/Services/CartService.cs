using AutoMapper;
using DataAccess.Repository.IRepository;
using Models;
using Services.Contracts;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    internal class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly IUnitofwork _unitofwork;

        public CartService(IMapper mapper, IUnitofwork unitofwork)
        {
            _mapper = mapper;
            _unitofwork = unitofwork;
        }

        public async Task<CartDto> GetCartAsync(string userId)
        {
            return _mapper.Map<CartDto>(await _unitofwork.Carts.GetCartByUserIdAsync(userId));

        }

        public async Task AddItemToCartAsync(string userId, int productId, int quantity)
        {
            await _unitofwork.Carts.AddItemAsync(userId, productId, quantity);
        }

        public async Task RemoveItemAsync(int cartItemId)
        {
            await _unitofwork.Carts.RemoveItemAsync(cartItemId);
        }
        public async Task ClearCartItemsAsync(string userId)
        {
            await _unitofwork.Carts.ClearCartAsync(userId);
        }
        public async Task UpdateCartItemQuantityAsync(int cartItemId, int quantity)
        {
            await _unitofwork.Carts.UpdateCartItemQuantityAsync(cartItemId, quantity);
        }
    }
}
