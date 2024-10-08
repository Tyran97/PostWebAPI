using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pinpost.data;
using Pinpost.Dtos.Post;
using Pinpost.interfaces;
using Pinpost.models;

namespace Pinpost.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDBContext _context;

        public PostRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Post> CreateAsync(Post postModel)
        {
            await _context.Posts.AddAsync(postModel);
            await _context.SaveChangesAsync();
            return postModel;
        }

        public async Task<Post?> DeleteAsync(int id)
        {
            var postModel = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (postModel == null)
            {
                return null;
            }

            _context.Posts.Remove(postModel);
            await _context.SaveChangesAsync();
            return postModel;
        }

        public async Task<List<Post>> GetAllAsync()
        {
             return await _context.Posts.Include(x => x.Comments).ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> PostExists(int id)
        {
            return await _context.Posts.AnyAsync(x => x.Id == id);
        }

        public async Task<Post?> UpdateAsync(int id, UpdatePostRequestDto postDto)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingPost == null)
            {
                return null;
            }
            existingPost.Theme = postDto.Theme;

            await _context.SaveChangesAsync();
            return existingPost;
        }
    }
}