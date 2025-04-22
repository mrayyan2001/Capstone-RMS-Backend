using System;
using System.Collections.Generic;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnboardingController : ControllerBase
    {
        private readonly IOnboardingService _onboardingService;

        public OnboardingController(IOnboardingService onboardingService)
        {
            _onboardingService = onboardingService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_onboardingService.GetAllPages());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }

        }

        [HttpGet("{pageNumber}")]
        public IActionResult GetPageByNumber([FromRoute] int pageNumber)
        {
            try
            {
                var existingPage = _onboardingService.GetPageByNumber(pageNumber);
                if (existingPage is null)
                {
                    return NotFound();
                }

                return Ok(existingPage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

    }
}