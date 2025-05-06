namespace api.DTOs.Category
{
    public class GetCategoryDTO
    {
        //Id and Name and Image / Logo URL
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public string ImageURL { get; set; }
    }
}
