using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Cart;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _carService;

        public CartController(ICartService carService)
        {
            _carService = carService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddCartDTO dto)
        {
            var addResult = await _carService.AddAsync(dto);
            if (addResult is null)
            {
                return BadRequest("UserId or ItemId is invalid.");
            }
            return Ok("Item added to cart");
        }
    }
}