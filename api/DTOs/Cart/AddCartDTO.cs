using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Cart
{
    public class AddCartDTO
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}