using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.Responses;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UsersController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Route("{FollowedUserId}/Followers")]
        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] int FollowingUserId, [FromRoute] int FollowedUserId)
        {
            var success = await _service.FollowAsync(FollowingUserId, FollowedUserId);

            if (success == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "There was an issue",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> users = await _service.GetAllUsersAsync();

            SuccessResponse response = new()
            {
                Payload = users,
                Status = 200,
            };

            return Ok(response);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserById([FromRoute] int Id)
        {
            User user = await _service.GetUserByIdAsync(Id);

            if (user == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "User not found",
                    Errors = new List<string>(),
                    Status = 404,
                };

                return NotFound(error);
            }

            SuccessResponse response = new()
            {
                Payload = user,
                Status = 200
            };

            return Ok(response);
        }

        [Route("{Id}/Followers")]
        [HttpGet]
        public async Task<IActionResult> GetUserFollowers([FromRoute] int Id)
        {
            List<User> userFollowers = await _service.GetUserFollowersByUserIdAsync(Id);

            SuccessResponse response = new()
            {
                Payload = userFollowers,
                Status = 200
            };

            return Ok(response);
        }

        [Route("{Id}/Following")]
        [HttpGet]
        public async Task<IActionResult> GetUserFollowing([FromRoute] int Id)
        {
            List<User> userFollowing = await _service.GetUserFollowedUsersByUserIdAsync(Id);

            SuccessResponse response = new()
            {
                Payload = userFollowing,
                Status = 200
            };

            return Ok(response);
        }
    }
}
