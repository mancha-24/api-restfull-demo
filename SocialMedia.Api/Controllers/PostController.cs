using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMediaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(ILogger<PostController> logger, 
                              IPostRepository postRepository,
                              IMapper mapper)
        {
            _logger = logger;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();

            var postDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            return Ok(postDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);

            var postDto = _mapper.Map<PostDto>(post);
     
            return Ok(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var post = _mapper.Map<Post>(postDto);
           
            await _postRepository.InsertPost(post);

            return Ok(post);
        }

    }
}