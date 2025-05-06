using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.DTOs.Cart;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cart-items")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private int UserId
        {
            get => Convert.ToInt32(User.FindFirst("uid")?.Value ?? throw new UnauthorizedAccessException());
        }

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddCartDTO dto)
        {
            try
            {
                var addResult = await _cartService.AddAsync(UserId, dto);
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

        [HttpGet]
        public async Task<IActionResult> GetAllByUserId()
        {
            try
            {
                return Ok(await _cartService.GetAllByUserIdAsync(UserId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            try
            {
                await _cartService.DeleteAsync(UserId, itemId);
                return Ok("Item removed from cart");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{itemId}")]
        public async Task<IActionResult> UpdateQuantity(int itemId, [FromBody] int newQuantity)
        {
            try
            {
                await _cartService.UpdateAsync(UserId, itemId, newQuantity);
                return Ok("Item quantity updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}