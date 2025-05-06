using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Bookmark
{
    public class AddItemToFavDTO
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ItemId { get; set; }
    }
}
