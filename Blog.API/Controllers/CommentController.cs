using Blog.Application.DTOs;
using Blog.Application.DTOs.Comment;
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
    //[Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IUnitOfWork _uow;

        public CommentController(ICommentService commentService, IUnitOfWork uow)
        {
            _commentService = commentService;
            _uow = uow;
        }

        [HttpPost("AddComment")]
        public async Task<ActionResult<BaseResponse>> AddCommentToPost(CommentDto dto, string PostId)
        {
            //var userName = User.Claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userid is null /*|| userName is null*/)
            {
                return Unauthorized();
            }
            return await _commentService.CreateComment(dto.Text, userid, PostId);
        }
        [HttpPut("{CommentId}")]
        public async Task<ActionResult<BaseResponse>> EditComment([FromRoute] string CommentId, CommentDto dto)
        {
            var comment = await _uow.GenericRepository<Comment>().GetByIdAsync(CommentId);
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userid is null || comment.UserId != userid)
            {
                return Unauthorized();
            }
            return await _commentService.EditComment(dto.Text, CommentId, userid);
        }
        [HttpDelete("{CommentId}")]
        public async Task<ActionResult<BaseResponse>> DeleteComment([FromRoute] string CommentId)
        {
            var comment = await _uow.GenericRepository<Comment>().GetByIdAsync(CommentId);
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userid is null || comment.UserId != userid)
            {
                return Unauthorized();
            }
            return await _commentService.DeleteComment(CommentId, userid);
        }
    }
}
