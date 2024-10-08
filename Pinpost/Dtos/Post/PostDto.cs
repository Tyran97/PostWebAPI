using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pinpost.Dtos.Comment;

namespace Pinpost.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Theme { get; set; } = string.Empty;
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
