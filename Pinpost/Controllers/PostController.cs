using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pinpost.data;
using Pinpost.Dtos.Post;
using Pinpost.interfaces;
using Pinpost.Mappers;

namespace Pinpost.Controllers
{

    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepo;
        public PostController(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var posts = await _postRepo.GetAllAsync();
            var postDto = posts.Select(x => x.ToPostDto());

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var posts = await _postRepo.GetByIdAsync(id);
            
            if(posts == null)
            {
                return NotFound();
            }

            return Ok(posts.ToPostDto());

        }

        

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequestDto postDto)
        {

            var postModel = postDto.ToPostFromCreateDTO();
            await _postRepo.CreateAsync(postModel);
            return CreatedAtAction(nameof(GetById), new { id = postModel.Id }, postModel.ToPostDto());

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostRequestDto updateDto)
        {
            var postModel = await _postRepo.UpdateAsync(id , updateDto);
            if(postModel == null)
            {
                return NotFound();
            }

            return Ok(postModel.ToPostDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var postModel = await _postRepo.DeleteAsync(id);
            if (postModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}