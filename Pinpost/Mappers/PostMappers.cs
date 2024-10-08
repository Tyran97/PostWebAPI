using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pinpost.Dtos.Post;
using Pinpost.models;

namespace Pinpost.Mappers
{
    public static class PostMappers
    {
        public static PostDto ToPostDto(this Post PostModel)
        {
            return new PostDto
            {
                Id = PostModel.Id,
                Theme = PostModel.Theme,
                Comments = PostModel.Comments.Select(x => x.ToCommentDto()).ToList()
                
            };
        }

        public static Post ToPostFromCreateDTO(this CreatePostRequestDto postDto)
        {
            return new Post
            {
                Theme = postDto.Theme
            };
        }
    }
}