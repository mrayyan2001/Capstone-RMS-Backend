using api.DTOs.Bookmark;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens.Configuration;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteItemsController : ControllerBase
    {

        private readonly IfavServices _favService;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public FavoriteItemsController(IfavServices favService, IUserService userService, IAuthService authService)
        {
            _favService = favService;
            _userService = userService;
            _authService = authService;
        }


        [HttpGet("[action]")]
        //[Authorize]
        public async Task<IActionResult> GetFavoriteItems(int userId)
        {
            //after add the token the user id must not pass in Query and the id get from String TOkens when sent the request 
            try
            {
                if (userId <= 0)
                  return BadRequest("Invalid user");
                
                if(!await _userService.IsExistsUser(userId))
                    return NotFound("User Not Found");
                // TODO Check item if exists 

                //TODO: Use JWT rather than this
                if(!await _authService.IsLogin(userId))
                    return Unauthorized();

                List<FavItemDTOs>? favoriteItem=await _favService.GetFavItem(userId);
                if (favoriteItem == null)
                {
                    return NoContent();
                }
                
                return Ok(new
                {
                    status = "success",
                    data = favoriteItem
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, "An Error Occurs: " + e.Message);
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddItemInFav(AddItemToFavDTO input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (!await _userService.IsExistsUser(input.ClientId))
                    return NotFound("User Doesn't Exists");

                //TODO: when create the item services check if the item Exists

                //if (!await _.IsExistsUser(input.ClientId))
                //    return NotFound("User Doesn't Exists");


                int? newFavID = await _favService.AddItemToFav(input);

                return Ok(new
                {
                    status = "success",
                    item = newFavID
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, "An Error Occurs " + e.Message);
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveItemFromFav(int id)
        {
            try
            {
                var newFavID = await _favService.RemoveItemFromFav(id);

                return Ok(new
                {
                    status = "success",
                    item = newFavID
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, "An Error Occurs " + e.Message);
            }
        }

    }
}
