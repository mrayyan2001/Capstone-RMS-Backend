using api.DTOs.Address;
using api.DTOs.Payment;
using api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _address;

        public AddressController(IAddress address)
        {
            _address = address;
        }

        [HttpPost("addAddress")]
        public async Task<IActionResult> AddNewAddress([FromBody] AddNewAddressDTO dto)
        {
            try
            {
                var Result = await _address.AddNewAddress(dto);
                if (Result is null)
                {
                    return BadRequest("Wrong Address Information");
                }
                return Ok("Address added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
