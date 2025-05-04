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
            try
            {
                var addResult = await _carService.AddAsync(dto);
                if (addResult is null)
                {
                    return BadRequest("UserId or ItemId is invalid.");
                }
                return Ok("Item added to cart");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("get-all/{userId}")]
        public async Task<IActionResult> GetAllByUserId(int userId)
        {
            try
            {
                return Ok(await _carService.GetAllByUserIdAsync(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{userId}/{itemId}")]
        public async Task<IActionResult> RemoveItem(int userId, int itemId)
        {
            try
            {
                await _carService.DeleteAsync(userId, itemId);
                return Ok("Item removed from cart");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("update-quantity")]
        public async Task<IActionResult> UpdateQuantity(UpdateCartQuantityDTO dto)
        {
            try
            {
                await _carService.UpdateAsync(dto);
                return Ok("Item updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}