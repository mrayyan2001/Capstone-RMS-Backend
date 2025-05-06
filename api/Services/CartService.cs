using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Cart;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class CartService : ICartService
    {
        private readonly FoodtekDbContext _context;

        public CartService(FoodtekDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem?> AddAsync(int userId, AddCartDTO dto)
        {
            var user = await _context.Users.Include(u => u.CartItems).FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                throw new ArgumentException("UserId is invalid.");

            if (!_context.Items.Any(i => i.Id == dto.ItemId))
                throw new ArgumentException("ItemId is invalid.");


            // TODO - check if item is already in the cart if it is then add the quantity to this item
            var cartItem = user.CartItems.FirstOrDefault(ci => ci.ItemId == dto.ItemId);
            if (cartItem is not null)
            {
                cartItem.Quantity += dto.Quantity;
                await _context.SaveChangesAsync();
                return cartItem;
            }

            cartItem = new CartItem()
            {
                UserId = userId,
                ItemId = dto.ItemId,
                Quantity = dto.Quantity
            };
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
        public async Task<List<CartItemDTO>> GetAllByUserIdAsync(int userId)
        {
            if (!_context.Users.Any(u => u.Id == userId))
            {
                throw new Exception("UserId is invalid");
            }

            return await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Select(ci => new CartItemDTO()
                {
                    ItemId = ci.ItemId,
                    ItemNameAr = ci.Item.ItemNameAr,
                    ItemNameEn = ci.Item.ItemNameEn,
                    ItemDescriptionAr = ci.Item.ItemDescriptionAr,
                    ItemDescriptionEn = ci.Item.ItemDescriptionEn,
                    Quantity = ci.Quantity
                }).ToListAsync();
        }
        public async Task DeleteAsync(int userId, int itemId)
        {
            var user = await _context.Users.Include(u => u.CartItems).FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                throw new ArgumentException("UserId is invalid");

            var item = user.CartItems.FirstOrDefault(ci => ci.ItemId == itemId);
            if (item is null)
                throw new ArgumentException("ItemId is invalid");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int userId, int itemId, int newQuantity)
        {
            var user = await _context.Users.Include(u => u.CartItems).FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                throw new ArgumentException("UserId is invalid.");

            if (!_context.Items.Any(i => i.Id == itemId))
                throw new ArgumentException("ItemId is invalid.");

            var cartItem = user.CartItems.FirstOrDefault(ci => ci.ItemId == itemId);
            if (cartItem is null)
            {
                return;
            }
            cartItem.Quantity = newQuantity;
            await _context.SaveChangesAsync();
        }
    }
}