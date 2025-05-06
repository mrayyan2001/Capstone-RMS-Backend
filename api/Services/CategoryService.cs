using api.DTOs.Category;
using api.DTOs.Notification;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class CategoryService : ICategory
    {
        private readonly FoodtekDbContext _context;

        public CategoryService(FoodtekDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetCategoryDTO>> GetAllActiveCategory()
        {
            if (_context.Categories.Any(x => x.IsActive == false))
            {
                throw new Exception("Category is invalid");
            }

            return await _context.Categories.Where(x => x.IsActive == true)
                .Select(x => new GetCategoryDTO()
                {
                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                   ImageURL = x.ImageUrl,
                   
                }).ToListAsync();

        }
    }
}
