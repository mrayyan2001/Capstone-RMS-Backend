using api.Interfaces;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotification _notificationService;

        public NotificationController(INotification notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet("get-all/{userId}")]
        public async Task<IActionResult> GetAllByUserId(int userId)
        {
            try
            {
                return Ok(await _notificationService.GetAllNotification(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
