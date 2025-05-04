using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Cart;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class CartService : ICartService
    {
        private readonly FoodtekDbContext _context;

        public CartService(FoodtekDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem?> AddAsync(AddCartDTO dto)
        {
            if (!_context.Users.Any(u => u.Id == dto.UserId) || !_context.Items.Any(i => i.Id == dto.ItemId) || dto.Quantity <= 0)
                return null;


            var cartItem = new CartItem()
            {
                UserId = dto.UserId,
                ItemId = dto.ItemId,
                Quantity = dto.Quantity
            };
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
    }
}