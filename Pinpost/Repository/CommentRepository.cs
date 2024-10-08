using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Pinpost.data;
using Pinpost.Dtos.Comment;
using Pinpost.interfaces;
using Pinpost.models;

namespace Pinpost.Repository
{
    public class CommentRepository : ICommentRepository
    {

        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public  async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if(commentModel == null)
            {
                return null;
            }

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);

            /*var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if(commentModel == null)
            {
                return null;
            }
            return commentModel;*/
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if(existingComment == null)
            {
                return null;
            }
            existingComment.Content = commentDto.Content;
            existingComment.Title = commentDto.Title;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}