using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Cart
{
    public class CartItemDTO
    {
        public int ItemId { get; set; }
        public string ItemNameAr { get; set; } = string.Empty;
        public string ItemNameEn { get; set; } = string.Empty;
        public string ItemDescriptionAr { get; set; } = string.Empty;
        public string ItemDescriptionEn { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}