using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.API.DTOs;
using Talkish.API.Responses;
using Talkish.Domain.Models;
using Talkish.Services;
using Talkish.Services.DTOs;

namespace Talkish.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        private readonly IMapper _mapper;

        public AuthController(AuthService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO RegistrationData)
        {
            if (ModelState.IsValid)
            {
                User registeredUser = await _service.Register(RegistrationData);

                if (registeredUser == null)
                {
                    ErrorResponse error = new()
                    {
                        ErrorMessage = "Invalid User Registration Data",
                        Errors = new List<string>(),
                        Status = 400,
                    };

                    return BadRequest(error);
                }

                RegisteredUserDTO registeredUserDTO = _mapper.Map<RegisteredUserDTO>(registeredUser);

                SuccessResponse response = new()
                {
                    Payload = registeredUserDTO,
                    Status = 201,
                };

                // Will have to manually add the "/users/{id}" controller action later on as a CreatedAtAction param
                // TEMPORARY FIX!
                return Created("", response);
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
                IdentityUser loggedUser = await _service.Login(LoginData);

                if (loggedUser is null)
                {
                    ErrorResponse error = new()
                    {
                        ErrorMessage = "Invalid Auth Credentials",
                        Errors = new List<string>(),
                        Status = 400,
                    };

                    return BadRequest(error);
                }

                return NoContent();
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
