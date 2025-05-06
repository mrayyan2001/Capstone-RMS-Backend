using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class SendEmailDto()
    {
        [Required, EmailAddress]
        public string To { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
    }
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(SendEmailDto dto)
        {
            try
            {
                await _emailService.SendEmailAsync(dto.To, dto.Subject, dto.Body);
                return Ok($"Email is sent to {dto.To}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}