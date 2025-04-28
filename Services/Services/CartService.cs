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
        private readonly ICartRepository _cartRepository;

        public CartService(IMapper mapper, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<CartDto> GetCartAsync(string userId)
        {
            return _mapper.Map<CartDto>(await _cartRepository.GetCartByUserIdAsync(userId));

        }

        public async Task AddItemToCartAsync(string userId, int productId, int quantity)
        {
            await _cartRepository.AddItemAsync(userId, productId, quantity);
        }

        public async Task RemoveItemAsync(int cartItemId)
        {
            await _cartRepository.RemoveItemAsync(cartItemId);
        }
        public async Task ClearCartItemsAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }
        public async Task UpdateCartItemQuantityAsync(int cartItemId, int quantity)
        {
            await _cartRepository.UpdateCartItemQuantityAsync(cartItemId, quantity);
        }
    }
}
