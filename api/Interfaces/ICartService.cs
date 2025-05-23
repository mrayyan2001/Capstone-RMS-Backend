using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Cart;
using api.Models;

namespace api.Interfaces
{
    public interface ICartService
    {
        public Task<CartItem?> AddAsync(int userId, AddCartDTO dto);
        public Task<List<CartItemDTO>> GetAllByUserIdAsync(int userId);
        public Task DeleteAsync(int userId, int itemId);
        public Task UpdateAsync(int userId, int itemId, int newQuantity);
    }
}