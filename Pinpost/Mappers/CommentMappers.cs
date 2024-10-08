using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pinpost.Dtos.Comment;
using Pinpost.models;

namespace Pinpost.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment CommentModel)
        {
            return new CommentDto
            {
                Id = CommentModel.Id,
                Content = CommentModel.Content,
                Title = CommentModel.Title
            };
        }

        public static Comment ToCommentFromCreateDTO(this CreateCommentRequestDto commentDto, int postId)
        {
            return new Comment
            {
                Content = commentDto.Content,
                Title = commentDto.Title,
                PostId = postId
            };
        }
    }
}