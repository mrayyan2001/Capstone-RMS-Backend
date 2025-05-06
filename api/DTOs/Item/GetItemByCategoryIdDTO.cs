
namespace api.DTOs.Item
{
    public class GetItemByCategoryIdDTO
    {
        //Id , Name , Description , Price , Image
        
        public int ItemId { get; set; }
        public string NameAr { get; set; }  

        public string NameEn { get; set; }  
        public string DescriptionAr { get; set; }

        public string DescriptionEn { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        internal List<GetItemByCategoryIdDTO> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
