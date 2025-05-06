using api.DTOs.Category;

namespace api.Interfaces
{
    public interface ICategory
    {
        Task<List<GetCategoryDTO>> GetAllActiveCategory();
    }
}
