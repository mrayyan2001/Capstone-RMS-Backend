﻿using api.Interfaces;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class OnboardingControllers : ControllerBase
    {
        private readonly IOnboardingService _service;
        public OnboardingControllers(IOnboardingService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAllPages());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }

        }

        [HttpGet("{pageNumber}")]
        public async Task<IActionResult> GetPageByNumber([FromRoute] int pageNumber)
        {
            try
            {
                var response = _service.GetPageByNumber(pageNumber);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
