using api.DTOs.Bookmark;

namespace api.Interfaces
{
    public interface IfavServices
    {
        public Task<List<FavItemDTOs>?> GetFavItem(int UserId);
        public Task<int> AddItemToFav(AddItemToFavDTO input);
        public Task<int> RemoveItemFromFav(int id);
    }
}
