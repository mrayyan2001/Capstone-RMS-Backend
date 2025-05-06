using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Item;
using api.Models;

namespace api.Interfaces
{
    public interface IItemService
    {
        public Task<DetailsItemDTO?> GetByIdAsync(int id);
        public Task<List<TopRatedItemDTO>> GetTop10RatedAsync();
        public Task<List<TopRecommendedItemDTO>> GetTop10RecommendedAsync();
        public Task<List<GetItemByCategoryIdDTO>> GetItemByCategoryId(int id);

    }
}