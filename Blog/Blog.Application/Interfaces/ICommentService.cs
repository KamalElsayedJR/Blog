using Blog.Application.DTOs;
using Blog.Application.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces
{
    public interface ICommentService
    {
        Task<BaseResponse> CreateComment(string comment,string userId,string postId);
        Task<BaseResponse> DeleteComment(string commentId,string userId);
        Task<BaseResponse> EditComment(string comment,string commentId,string userId);
    }
}
