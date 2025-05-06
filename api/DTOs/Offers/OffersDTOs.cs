namespace api.DTOs.Offers
{
    public class OffersDTOs
    {
        public int Id { get; set; }
        public string TitleEn { get; set; } = null!;

        public string TitleAr { get; set; } = null!;
        public string DescriptionEn { get; set; } = null!;

        public string DescriptionAr { get; set; } = null!;
        public string? ImageUrl { get; set; }

    }
}
