using Blog.Application.DTOs;
using Blog.Application.DTOs.Post;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userid is null || userole.ToLower() != "admin" || userole.ToLower() != "editor")
            {
                return Unauthorized();
            }
            return await _postService.CreatePostAsync(dto, userid);
        }

        [HttpPut("{PostId}")]
        public async Task<ActionResult<BaseResponse>> UpdatePost(CreateOrUpdatePostDto dto, [FromRoute] string PostId)
        {
            var Post = await _Uow.GenericRepository<Post>().GetByIdAsync(PostId);
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userid is null || userole.ToLower() != "admin" || Post.Id != userid)
            {
                return Unauthorized();
            }
            return await _postService.UpdatePostAsync(dto, userid, PostId);
        }
        [HttpDelete("{PostId}")]
        public async Task<ActionResult<BaseResponse>> DeletePost([FromRoute] string PostId)
        {
            var Post = await _Uow.GenericRepository<Post>().GetByIdAsync(PostId);
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userid is null || userole.ToLower() != "admin" || Post.Id != userid)
            {
                return Unauthorized();
            }
            return await _postService.DeletePostAsync(PostId);
        }
    }
}
