using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.API.Responses;
using Talkish.Services;
using Talkish.Services.DTOs;

namespace Talkish.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO RegistrationData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string jwtToken = await _service.Register(RegistrationData);

                    SuccessResponse response = new()
                    {
                        Payload = jwtToken,
                        Status = 201,
                    };

                    // Will have to manually add the "/users/{id}" controller action later on as a CreatedAtAction param
                    // TEMPORARY FIX!
                    return Created("", response);
                } catch (Exception err)
                {
                    ErrorResponse error = new()
                    {
                        ErrorMessage = "There was an issue creating the user",
                        Errors = new List<string>(err.Message.First()),
                        Status = 400,
                    };

                    return BadRequest(error);
                }
            } else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList();

                ErrorResponse error = new()
                {
                    ErrorMessage = "Invalid User Registration Data",
                    Errors = new List<string>(errors),
                    Status = 400,
                };

                return BadRequest(error);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO LoginData)
        {
            if (ModelState.IsValid)
            {
                try {
                    string jwtToken = await _service.Login(LoginData);

                    SuccessResponse response = new()
                    {
                        Payload = jwtToken,
                        Status = 200,
                    };

                    return Ok(response);
                } catch (Exception err) {
                    ErrorResponse error = new()
                    {
                        ErrorMessage = "Invalid Auth Credentials",
                        Errors = new List<string>(err.Message.First()),
                        Status = 400,

                    };

                    return BadRequest(error);
                }
            } else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList();

                ErrorResponse error = new()
                {
                    ErrorMessage = "Invalid Login Data",
                    Errors = new List<string>(errors),
                    Status = 400,
                };

                return BadRequest(error);
            }
        }
    }
}
