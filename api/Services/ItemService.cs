using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Item;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ItemService : IItemService
    {
        private readonly FoodtekDbContext _context;

        public ItemService(FoodtekDbContext context)
        {
            _context = context;
        }

        public async Task<DetailsItemDTO?> GetByIdAsync(int id)
        {
            var item = await _context
                .Items
                .AsNoTracking()
                .Include(i => i.OrderItems)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (item is null)
                return null;
            return new DetailsItemDTO()
            {
                Id = item.Id,
                ItemNameAr = item.ItemNameAr,
                ItemNameEn = item.ItemNameEn,
                ItemDescriptionAr = item.ItemDescriptionAr,
                ItemDescriptionEn = item.ItemDescriptionEn,
                Price = item.Price,
                Rate = item.OrderItems.Average(oi => oi.Rate),
                NumberOfReviews = item.OrderItems.Count(oi => oi.Review is not null),
            };
        }
        public async Task<List<TopRatedItemDTO>> GetTop10RatedAsync()
        {
            return await _context.Items
                .AsNoTracking()
                .Include(i => i.OrderItems)
                .Where(i => i.OrderItems.Any())
                .Select(i => new TopRatedItemDTO()
                {
                    Id = i.Id,
                    ItemNameAr = i.ItemNameAr,
                    ItemNameEn = i.ItemNameEn,
                    ItemDescriptionAr = i.ItemDescriptionAr,
                    ItemDescriptionEn = i.ItemDescriptionEn,
                    Price = i.Price,
                    ImageUrl = i.ImageUrl,
                    Rate = i.OrderItems.Average(oi => oi.Rate),
                })
                .OrderByDescending(i => i.Rate)
                .Take(10)
                .ToListAsync();
        }
        public async Task<List<TopRecommendedItemDTO>> GetTop10RecommendedAsync()
        {
            return await _context.Items
                .AsNoTracking()
                .Include(i => i.OrderItems)
                .OrderByDescending(i => i.OrderItems.Count())
                .Take(10)
                .Select(i => new TopRecommendedItemDTO()
                {
                    Id = i.Id,
                    ItemNameAr = i.ItemNameAr,
                    ItemNameEn = i.ItemNameEn,
                    ItemDescriptionAr = i.ItemDescriptionAr,
                    ItemDescriptionEn = i.ItemDescriptionEn,
                    Price = i.Price,
                    ImageUrl = i.ImageUrl,
                })
                .ToListAsync();
        }
    }
}