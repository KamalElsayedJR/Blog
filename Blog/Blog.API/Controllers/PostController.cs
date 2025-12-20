using Blog.Application.DTOs;
using Blog.Application.DTOs.Post;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostServices _postService;

        public IUnitOfWork _Uow { get; }

        public PostController(IPostServices postService, IUnitOfWork uow)
        {
            _postService = postService;
            _Uow = uow;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleResponse<PostDto>>> GetPost([FromRoute] string Id)
        {
            return await _postService.GetPostByIdAsync(Id);
        }
        [HttpGet]
        public async Task<ActionResult<ListResponse<PostDto>>> GetAllPosts()
        {
            return await _postService.GetAllPostAsync();
        }
        [Authorize]
        [HttpPost("AddPost")]
        public async Task<ActionResult<BaseResponse>> AddPost(CreateOrUpdatePostDto dto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (userId is null || !userRoles.Contains("admin") || !userRoles.Contains("editor")) return Unauthorized(new BaseResponse(false, "Unauthorized To Post"));
            return await _postService.CreatePostAsync(dto, userId);
        }
        [Authorize]
        [HttpPut("{PostId}")]
        public async Task<ActionResult<BaseResponse>> UpdatePost(CreateOrUpdatePostDto dto, [FromRoute] string PostId)
        {
            var Post = await _Uow.GenericRepository<Post>().GetByIdAsync(PostId);
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (userid is null || !userRoles.Contains("admin") || Post.UserId != userid) return Unauthorized(new BaseResponse(false, "Unauthorized edit This Post"));
            return await _postService.UpdatePostAsync(dto, PostId);
        }
        [Authorize]
        [HttpDelete("{PostId}")]
        public async Task<ActionResult<BaseResponse>> DeletePost([FromRoute] string PostId)
        {
            var Post = await _Uow.GenericRepository<Post>().GetByIdAsync(PostId);
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (userid is null || !userRoles.Contains("admin") || Post.UserId != userid)
            {
                return Unauthorized(new BaseResponse(false, "Unauthorized To Delete This Post"));
            }
            return await _postService.DeletePostAsync(PostId);
        }
    }
}
