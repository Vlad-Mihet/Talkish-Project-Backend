﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.API.Responses;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IMapper _mapper;

        public AuthController(IAuthService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register([FromBody] dynamic RegistrationData)
        {
            if (ModelState.IsValid)
            {
                User createdUser = await _service.Register(RegistrationData);

                SuccessResponse response = new()
                {
                    Payload = createdUser,
                    Status = 201,
                };

                // Will have to manually add the "/users/{id}" controller action later on as a CreatedAtAction param
                return Ok(response);
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
        public async Task<IActionResult> Login([FromBody] dynamic LoginData)
        {
            if (ModelState.IsValid)
            {
                User loggedUser = await _service.Login(LoginData);

                if (loggedUser == null)
                {
                    ErrorResponse error = new()
                    {
                        ErrorMessage = "Invalid Auth Credentials",
                        Errors = new List<string>(),
                        Status = 400,
                    };

                    return BadRequest(error);
                }

                SuccessResponse response = new()
                {
                    Payload = loggedUser,
                    Status = 200,
                };

                return Ok(response);
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
