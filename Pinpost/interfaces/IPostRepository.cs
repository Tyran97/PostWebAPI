using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pinpost.Dtos.Post;
using Pinpost.models;

namespace Pinpost.interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post postModel);
        Task<Post?> UpdateAsync(int id , UpdatePostRequestDto postDto);
        Task<Post?> DeleteAsync(int id);
        Task<bool> PostExists(int id);
    }
}