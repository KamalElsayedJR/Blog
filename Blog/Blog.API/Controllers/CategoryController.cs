using AutoMapper;
using Blog.Application.DTOs;
using Blog.Application.DTOs.Category;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryServices;
        private readonly IUnitOfWork _uow;
        private readonly IMapper mapper;
    
        public CategoryController(ICategoryService categoryServices, IUnitOfWork uow, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _uow = uow;
            this.mapper = mapper;
        }
        [HttpGet("{CategotyId}")]
        public async Task<ActionResult<SingleResponse<Category>>> GetCategory([FromRoute] string CategotyId)
        {
            var cat = await _uow.GenericRepository<Category>().GetByIdAsync(CategotyId);
            return new SingleResponse<Category>(true,"",cat);
        }
        [HttpPut("{CategoryId}")]
        public async Task<ActionResult<BaseResponse>> EditCategory(CategoryDto dto,[FromRoute] string CategoryId)
        {
            return await _categoryServices.EditCategoryAsync(dto, CategoryId);
        }
        [HttpGet]
        public async Task<ActionResult<ListResponse<CategoryDto>>> GetCategories()
        {
            var cats = (await _uow.GenericRepository<Category>().GetAllAsync()).ToList();
            var mCats = mapper.Map<List<Category>, List<CategoryDto>>(cats);
            return Ok(new ListResponse<CategoryDto>(isSuccess: true, message: "Category Retrived Successfully", mCats));
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> AddCategory(CategoryDto dto)
        {
            return await _categoryServices.AddCategoryAsync(dto);
        }
        [HttpDelete("{CategoryId}")]
        public async Task<ActionResult<BaseResponse>> DeleteCategory([FromRoute] string CategoryId)
        {
            return await _categoryServices.DeleteCategoryAsync(CategoryId);
        }
    }
}
