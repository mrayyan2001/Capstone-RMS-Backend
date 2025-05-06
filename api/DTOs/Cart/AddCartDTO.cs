using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Cart
{
    public class AddCartDTO
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        [Range(0, 99)]
        public int Quantity { get; set; }
    }
}