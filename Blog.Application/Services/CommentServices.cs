using Blog.Application.DTOs;
using Blog.Application.DTOs.Comment;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public class CommentServices : ICommentService
    {
        private readonly IUnitOfWork _uof;

        public CommentServices(IUnitOfWork uof)
        {
            _uof = uof;
        }
        //create comment
        public async Task<BaseResponse> CreateComment(string comment, string userId, string postId, string UserName)
        {
            Comment commentEtntiy = new Comment()
            {
                Text = comment,
                UserId = userId,
                PostId = postId,
                UserName = UserName
            };
            await _uof.GenericRepository<Comment>().AddAsync(commentEtntiy);
            if (await _uof.SaveChangesAsync() <= 0) return new BaseResponse(false, "Error During Save Comment");
            return new BaseResponse(true, "Comment Created Successfully");
        }
        //delete comment
        public async Task<BaseResponse> DeleteComment(string commentId, string userid)
        {
            var comment = await _uof.GenericRepository<Comment>().GetByIdAsync(commentId);
            if (comment is null || comment.UserId == userid) return new BaseResponse(false,"comment not found");
            _uof.GenericRepository<Comment>().Delete(comment);
            if (await _uof.SaveChangesAsync() <= 0) return new BaseResponse(false, "Error During Delete Comment");
            return new BaseResponse(true, "Comment Deleted Successfully");
        }
        //edit comment
        public async Task<BaseResponse> EditComment(string comment, string commentId, string userId)
        {
            var commentEntity = await _uof.GenericRepository<Comment>().GetByIdAsync(commentId);
            if (commentEntity is null || commentEntity.UserId != userId) return new BaseResponse(false, "Comment not found");
            commentEntity.Text = comment;
            _uof.GenericRepository<Comment>().Update(commentEntity);
            if (await _uof.SaveChangesAsync() <= 0) return new BaseResponse(false, "Error During Edit Comment");
            return new BaseResponse(true, "Comment Edited Successfully");
        }

    }
}
