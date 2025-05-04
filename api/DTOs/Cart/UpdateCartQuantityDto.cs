using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;

namespace api.DTOs.Cart
{
    public class UpdateCartQuantityDTO
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        [Range(1, 99)]
        public int NewQuantity { get; set; }
    }
}