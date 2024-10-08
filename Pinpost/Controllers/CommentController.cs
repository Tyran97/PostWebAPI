using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pinpost.data;
using Pinpost.Dtos.Comment;
using Pinpost.interfaces;
using Pinpost.Mappers;

namespace Pinpost.Controllers
{

    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
       private readonly ICommentRepository _commentRepo;
       private readonly IPostRepository _postRepo;

       public CommentController(ICommentRepository commentRepo, IPostRepository postRepo)
       {
        _commentRepo = commentRepo;
        _postRepo = postRepo;
       }

       [HttpGet]
       public async Task<IActionResult>GetAll()
       {
        var comments = await _commentRepo.GetAllAsync();
        var commentDto = comments.Select(x => x.ToCommentDto()); // CommentDto olarak cevap almak istersem,alt satır için
        return Ok(comments);
       }
// buraya bir daha bak
       [HttpGet]
       [Route("{id}")]
       public async Task<IActionResult>GetByID([FromRoute]int id)
       {
        var comment = await _commentRepo.GetByIdAsync(id);
        if(comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
       }

        [HttpPost]
        [Route("{postId}")]
        public async Task<IActionResult>Create([FromRoute] int postId , CreateCommentRequestDto commentDto)
        {
            if(!await _postRepo.PostExists(postId))
            {
                return BadRequest("Post does not exists!");
            }
            var commentModel = commentDto.ToCommentFromCreateDTO(postId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetByID), new { id = commentModel.Id }, commentModel.ToCommentDto());

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult>Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentRequestDto )
        {
            var postModel = await _commentRepo.UpdateAsync(id, commentRequestDto);
            if(postModel == null)
            {
                return NotFound();
            }
            /*if(!await _postRepo.PostExists(id))
            {
                return BadRequest("Post does not exists!");
            }*/
            return Ok(postModel.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult>Delete([FromRoute] int id)
        {
            var commentModel = await _commentRepo.DeleteAsync(id);
            if(commentModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
