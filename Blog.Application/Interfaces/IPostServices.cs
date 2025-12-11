using Blog.Application.DTOs;
using Blog.Application.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces
{
    public interface IPostServices
    {
        Task<BaseResponse> CreatePostAsync(CreateOrUpdatePostDto dto, string UserId,string CategoryName);
        Task<BaseResponse> UpdatePostAsync(CreateOrUpdatePostDto dto, string UserId, string PostId);
        Task<SingleResponse<PostDto>> GetPostByIdAsync(string PostId);
        Task<ListResponse<PostDto>> GetAllPost();
        Task<BaseResponse> DeletePost(string Id);
    }
}
