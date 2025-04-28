using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Client;
using api.Helpers.Validators;
using api.Interfaces;
using api.Models;
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
            
            try
            {
                if (!ClientValidator.IsEnterAllInput(dto))
                {
                    return BadRequest("Email and password are required");
                }

                var client = await _clientService.Login(dto);
                if (client is null)
                    return Unauthorized(new { message = "Incorrect Email or Password" });
                return Ok(new { message = $"Welcome {client.FirstName}", client });
            }
            
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error" );
            }

        }

        [HttpPost("request-otp")]
        public async Task<IActionResult> RequestOtp([FromBody]string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                    return BadRequest("Email is Required");
                if (!await _clientService.CheckExistsClient(email))
                    return Unauthorized("Email not found");
                // TODO - we should sent the otp in the email
                return Ok(new  {message = "OTP sent to your email", OTP = await _clientService.RequestOtp(email)});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpDTO dto)
        {
            try
            {
                if(!ClientValidator.IsEnterAllInput(dto))
                {
                    return BadRequest("OTP is required");
                }
                return Ok(await _clientService.VerifyOtp(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.NewPassword))
                {
                    throw new Exception("insert New Password");
                }
                if (string.IsNullOrEmpty(dto.ConfirmPassword))
                {
                    throw new Exception("Confirm the password");
                }
                if (!dto.NewPassword.Equals(dto.ConfirmPassword))
                {
                    throw new Exception("New password and Confirm password doesn't match");
                }
                
                    await _clientService.ResetPassword(dto);
                    return Ok();
                throw new Exception("Try Again");
            }
      

        catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}