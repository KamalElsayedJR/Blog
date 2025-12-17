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
        Task<SingleResponse<PostDto>> GetPostByIdAsync(string PostId);
        Task<ListResponse<PostDto>> GetAllPostAsync();
        Task<BaseResponse> CreatePostAsync(CreateOrUpdatePostDto dto, string UserId);
        Task<BaseResponse> UpdatePostAsync(CreateOrUpdatePostDto dto, string UserId, string PostId);
        Task<BaseResponse> DeletePostAsync(string Id);
    }
}
