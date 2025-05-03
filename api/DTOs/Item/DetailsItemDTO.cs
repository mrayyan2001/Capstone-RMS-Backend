using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Item
{
    public class DetailsItemDTO
    {
        public int Id { get; set; }
        public string ItemNameAr { get; set; } = null!;
        public string ItemNameEn { get; set; } = null!;
        public string ItemDescriptionAr { get; set; } = null!;
        public string ItemDescriptionEn { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal? Rate { get; set; }
        public int NumberOfReviews { get; set; }
    }
}