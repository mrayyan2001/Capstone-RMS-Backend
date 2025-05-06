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
    [Route("api/cart-items")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddCartDTO dto)
        {
            try
            {
                var addResult = await _cartService.AddAsync(dto);
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllByUserId(int userId)
        {
            try
            {
                return Ok(await _cartService.GetAllByUserIdAsync(userId));
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
                await _cartService.DeleteAsync(userId, itemId);
                return Ok("Item removed from cart");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{userId}/{itemId}")]
        public async Task<IActionResult> UpdateQuantity(int userId, int itemId, [FromBody] int newQuantity)
        {
            try
            {
                await _cartService.UpdateAsync(userId, itemId, newQuantity);
                return Ok("Item quantity updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}