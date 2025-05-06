using api.Data.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Implementation
{
    //public class FavRepo : BaseRepo<Bookmark>, IFavRepo
    //{

    //    private readonly FoodtekDbContext _context;

    //    public FavRepo(FoodtekDbContext context):base(context) 
    //    {

    //    }

    //    public async Task<List<Bookmark>> GetFavoriteItemsByUserIdAsync(int userId)
    //    {
    //        var details = await _context.Bookmarks
    //            .Where(x => x.ClientId == userId)
    //            .Include(b => b.Item)
    //            .ToListAsync();

    //        return details;
    //    }



    //}
}
