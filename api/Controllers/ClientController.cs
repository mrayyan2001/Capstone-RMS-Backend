using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Client;
using api.Interfaces;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ClientLoginDTO dto)
        {
            var client = await _clientService.Login(dto);
            if (client is null)
                return Unauthorized(new { message = "Incorrect Email or Password" });
            return Ok(new { message = $"Welcome {client.FirstName}", client });
        }

        [HttpPost("request-otp")]
        public async Task<IActionResult> RequestOtp([FromBody] RequestOtpDto dto)
        {
            var otp = await _clientService.RequestOtp(dto);
            if (otp is null)
            {
                return NotFound(new { message = "Email not found" });
            }
            // TODO - we should sent the otp in the email
            return Ok(new { message = "OTP sent to your email", otp });
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}