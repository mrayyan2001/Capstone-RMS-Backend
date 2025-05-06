namespace api.DTOs.Bookmark
{
    public class FavItemDTOs
    {
        public int ItemId { get; set; }
        public string ItemNameEn { get; set; } = string.Empty;
        public string ItemDescriptionEn { get; set; } = string.Empty;
        public float Price { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }
}
