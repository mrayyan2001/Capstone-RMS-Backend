using api.Interfaces;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {

        private readonly IOfferServce _service;

        public OfferController(IOfferServce service)
        {
            _service = service;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllActiveOffer()
        {
            try
            {
                var result=await _service.GetActiveOffers();
                if (result == null)
                {
                    return NoContent();
                }
                
                return Ok(new
                {
                    status = "success",
                    data = result
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, "An Error Occurs: " + e.Message);
            }
        }

    }
}
