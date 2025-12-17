using Blog.Application.DTOs;
using Blog.Application.DTOs.Category;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<BaseResponse> AddCategoryAsync(CategoryDto dto)
        {
            var CatEntity = new Category()
            {
                Name = dto.Name,
                Description = dto.Description
            };
            await _uow.GenericRepository<Category>().AddAsync(CatEntity);
            if (await _uow.SaveChangesAsync() <= 0)
                return new BaseResponse(false, "Error During Save Category");
            return new BaseResponse(true, "Category Created Successfully");
        }

        public async Task<BaseResponse> DeleteCategoryAsync(string CategoryId)
        {
            var cat = await _uow.GenericRepository<Category>().GetByIdAsync(CategoryId);
            if (cat is null)
                return new BaseResponse(false, "Category not found");
            _uow.GenericRepository<Category>().Delete(cat);
            if (await _uow.SaveChangesAsync() <= 0)
                return new BaseResponse(false, "Error During Delete Category");
            return new BaseResponse(true, "Category Deleted Successfully");
        }

        public async Task<BaseResponse> EditCategoryAsync(CategoryDto dto,string CategoryId)
        {
            var cat = await _uow.GenericRepository<Category>().GetByIdAsync(CategoryId);
            if (cat is null)
                return new BaseResponse(false, "Category not found");
            cat.Name = dto.Name;
            cat.Description = dto.Description;
            _uow.GenericRepository<Category>().Update(cat);
            if (await _uow.SaveChangesAsync() <= 0)
                return new BaseResponse(false, "Error During Edit Category");
            return new BaseResponse(true, "Category Edited Successfully");
        }
    }
}
