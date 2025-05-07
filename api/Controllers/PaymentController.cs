using api.DTOs.Cart;
using api.DTOs.Payment;
using api.Interfaces;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _payment;

        public PaymentController(IPayment payment)
        {
            _payment = payment;
        }



        [HttpPost("addPayment")]
        public async Task<IActionResult> AddNewPayment([FromBody] AddNewPaymentDTO dto)
        {
            try
            {
                var Result = await _payment.AddNewPayment(dto);
                if (Result is null)
                {
                    return BadRequest("Wrong Card Information");
                }
                return Ok("card added to payment");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
