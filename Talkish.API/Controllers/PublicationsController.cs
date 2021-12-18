using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talkish.API.DTOs;
using Talkish.API.Responses;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        private readonly IPublicationService _service;
        private readonly IMapper _mapper;

        public PublicationsController(IPublicationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublication([FromBody] AddPublicationDTO PublicationData)
        {
            if (ModelState.IsValid)
            {
                Publication publication = _mapper.Map<Publication>(PublicationData);
                await _service.CreatePublication(publication);

                SuccessResponse response = new()
                {
                    Payload = PublicationData,
                    Status = 201,
                };

                return CreatedAtAction(nameof(GetPublicationById), new { Id = publication.PublicationId }, publication);
            } else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(p => p.ErrorMessage)).ToList();

                ErrorResponse error = new()
                {
                    ErrorMessage = "Invalid Publication Data",
                    Errors = errors,
                    Status = 400,
                };

                return BadRequest(error);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPublications()
        {
            List<Publication> publications = await _service.GetAllPublications();
            List<PublicationDTO> publicationDTOs = _mapper.Map<List<PublicationDTO>>(publications);
            return Ok(publicationDTOs);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetPublicationById([FromRoute] int Id)
        {
            Publication publication = await _service.GetPublicationById(Id);

            if (publication == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Couldn't find publication",
                    Errors = new List<string>(),
                    Status = 404,
                };

                return NotFound(error);
            }

            PublicationDTO publicationDTO = _mapper.Map<PublicationDTO>(publication);
            return Ok(publicationDTO);
        }

        [HttpGet]
        [Route("{Id}/With-Blogs")]
        public async Task<IActionResult> GetPublicationWithBlogsById([FromRoute] int Id)
        {
            Publication publication = await _service.GetPublicationWithBlogsById(Id);

            if (publication == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Couldn't find publication",
                    Errors = new List<string>(),
                    Status = 404,
                };

                return NotFound(error);
            }

            PublicationWithBlogsDTO publicationDTO = _mapper.Map<PublicationWithBlogsDTO>(publication);

            SuccessResponse response = new()
            {
                Payload = publicationDTO,
                Status = 200,
            };
            
            return Ok(response);
        }

        [HttpGet]
        [Route("{Id}/Authors")]
        public async Task<IActionResult> GetPublicationAuthors([FromRoute] int Id)
        {
            List<Author> authors = await _service.GetPublicationAuthors(Id);

            if (authors == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Couldn't find publication",
                    Errors = new List<string>(),
                    Status = 404,
                };

                return NotFound(error);
            }

            List<AuthorDTO> authorDTOs = _mapper.Map<List<AuthorDTO>>(authors);

            SuccessResponse response = new()
            {
                Payload = authorDTOs,
                Status = 200,
            };
            
            return Ok(response);
        }

        [HttpGet]
        [Route("{Id}/Blogs")]
        public async Task<IActionResult> GetPublicationBlogs([FromRoute] int Id)
        {
            List<Blog> blogs = await _service.GetPublicationBlogs(Id);

            if (blogs == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "Couldn't find publication or its blogs",
                    Errors = new List<string>(),
                    Status = 404,
                };

                return NotFound(error);
            }

            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);

            SuccessResponse response = new()
            {
                Payload = blogDTOs,
                Status = 200,
            };
            
            return Ok(response);
        }

        [HttpPatch]
        [Route("{PublicationId}")]
        public async Task<IActionResult> UpdatePublication([FromRoute] int PublicationId, [FromBody] UpdatePublicationDTO PublicationData)
        {
            if (ModelState.IsValid)
            {
                Publication publication = _mapper.Map<Publication>(PublicationData);
                await _service.UpdatePublication(PublicationId, publication);

                SuccessResponse response = new()
                {
                    Payload = PublicationData,
                    Status = 200,
                };

                return Ok(response);
            }

            List<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(p => p.ErrorMessage)).ToList();

            ErrorResponse error = new()
            {
                ErrorMessage = "Invalid Publication Data",
                Errors = errors,
                Status = 400,
            };

            return BadRequest(error);
        }

        [HttpPost]
        [Route("{PublicationId}/Blogs")]
        public async Task<IActionResult> AddBlogToPublication([FromRoute] int PublicationId, [FromBody] int BlogId)
        {
            Publication publication = await _service.AddBlogToPublication(PublicationId, BlogId);
            
            if (publication == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "There was an error adding the blog to the publication",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }

            SuccessResponse response = new()
            {
                Payload = publication,
                Status = 200,
            };
            
            return Ok(response);
        }

        [HttpPost]
        [Route("{PublicationId}/Authors")]
        public async Task<IActionResult> AddAuthorToPublication([FromRoute] int PublicationId, [FromBody] int AuthorId)
        {
            Publication publication = await _service.AddAuthorToPublication(PublicationId, AuthorId);
            
            if (publication == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "There was an error adding the author to the publication",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }

            SuccessResponse response = new()
            {
                Payload = publication,
                Status = 200,
            };
            
            return Ok(response);
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeletePublication([FromRoute] int Id)
        {
            Publication publication = await _service.DeletePublication(Id);

            if (publication == null)
            {
                ErrorResponse error = new()
                {
                    ErrorMessage = "There was an issue trying to delete the publication",
                    Errors = new List<string>(),
                    Status = 400,
                };

                return BadRequest(error);
            }

            PublicationDTO publicationDTO = _mapper.Map<PublicationDTO>(publication);

            SuccessResponse response = new()
            {
                Payload = publicationDTO,
                Status = 200,
            };
            
            return Ok(res);
        }
    }
}
