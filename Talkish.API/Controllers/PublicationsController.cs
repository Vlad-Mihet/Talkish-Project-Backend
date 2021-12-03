﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.API.DTOs;
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
            Publication publication = _mapper.Map<Publication>(PublicationData);
            await _service.CreatePublication(publication);
            return Ok(publication);
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
            PublicationDTO publicationDTO = _mapper.Map<PublicationDTO>(publication);
            return Ok(publicationDTO);
        }

        [HttpGet]
        [Route("{Id}/With-Blogs")]
        public async Task<IActionResult> GetPublicationWithBlogsById([FromRoute] int Id)
        {
            Publication publication = await _service.GetPublicationWithBlogsById(Id);
            PublicationWithBlogsDTO publicationDTO = _mapper.Map<PublicationWithBlogsDTO>(publication);
            return Ok(publicationDTO);
        }

        [HttpGet]
        [Route("{Id}/Authors")]
        public async Task<IActionResult> GetPublicationAuthors([FromRoute] int Id)
        {
            List<Author> authors = await _service.GetPublicationAuthors(Id);
            List<AuthorDTO> authorDTOs = _mapper.Map<List<AuthorDTO>>(authors);
            return Ok(authorDTOs);
        }

        [HttpGet]
        [Route("{Id}/Blogs")]
        public async Task<IActionResult> GetPublicationBlogs([FromRoute] int Id)
        {
            List<Blog> blogs = await _service.GetPublicationBlogs(Id);
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);
            return Ok(blogDTOs);
        }

        [HttpPatch]
        [Route("{PublicationId}")]
        public async Task<IActionResult> UpdatePublication([FromRoute] int PublicationId, [FromBody] UpdatePublicationDTO PublicationData)
        {
            Publication publication = _mapper.Map<Publication>(PublicationData);
            await _service.UpdatePublication(PublicationId, publication);
            return Ok(publication);
        }

        [HttpPost]
        [Route("{PublicationId}/Blogs")]
        public async Task<IActionResult> AddBlogToPublication([FromRoute] int PublicationId, [FromBody] int BlogId)
        {
            Publication publication = await _service.AddBlogToPublication(PublicationId, BlogId);
            return Ok(publication);
        }

        [HttpPost]
        [Route("{PublicationId}/Authors")]
        public async Task<IActionResult> AddAuthorToPublication([FromRoute] int PublicationId, [FromBody] int AuthorId)
        {
            Publication publication = await _service.AddAuthorToPublication(PublicationId, AuthorId);
            return Ok(publication);
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeletePublication([FromRoute] int Id)
        {
            Publication publication = await _service.DeletePublication(Id);
            PublicationDTO publicationDTO = _mapper.Map<PublicationDTO>(publication);
            return Ok(publicationDTO);
        }
    }
}
