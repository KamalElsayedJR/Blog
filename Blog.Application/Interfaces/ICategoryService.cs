using Blog.Application.DTOs;
using Blog.Application.DTOs.Category;
using Blog.Application.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse> AddCategoryAsync(CategoryDto dto);
        Task<BaseResponse> DeleteCategoryAsync(string CategoryId);
        Task<BaseResponse> EditCategoryAsync(CategoryDto dto, string CategoryId);
    }
}
