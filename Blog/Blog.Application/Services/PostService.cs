using AutoMapper;
using Blog.Application.DTOs;
using Blog.Application.DTOs.Post;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public class PostService : IPostServices
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public PostService(IUnitOfWork uof,IMapper mapper) 
        { 
            _uof = uof;
            _mapper = mapper;
        }
        //Create Post
        public async Task<BaseResponse> CreatePostAsync(CreateOrUpdatePostDto dto,string UserId,string categoryName)
        {
            var MappedPost = _mapper.Map<CreateOrUpdatePostDto, Post>(dto);
            var category = await _uof.CategoryRepository.GetCategoryByName(categoryName);
            if(category is null) return new BaseResponse(false,"Category Not Found");
            MappedPost.CategoryId = category.Id;
            MappedPost.UserId = UserId;
            await _uof.GenericRepository<Post>().AddAsync(MappedPost);
            if (await _uof.SaveChangesAsync() <= 0) return new BaseResponse(false,"Couldnt Create Post");
            return new BaseResponse(true,"Post Created Successfuly");
        }
        //Update Post
        public async Task<BaseResponse> UpdatePostAsync(CreateOrUpdatePostDto dto, string UserId, string PostId)
        {
            var Post = await _uof.GenericRepository<Post>().GetByIdAsync(PostId);
            if(Post is null) return new BaseResponse(false,"Post Not Found");
            if(Post.UserId != UserId) return new BaseResponse(false,"You are not Authorized to Update this Post");
            _mapper.Map(dto, Post);
            _uof.GenericRepository<Post>().Update(Post);
            if(await _uof.SaveChangesAsync() <=0 ) return new BaseResponse(false,"Couldnt Update Post");
            return new BaseResponse(true,"Post Updated Successfuly");
        }
        //Get Post By Id
        public async Task<SingleResponse<PostDto>> GetPostByIdAsync(string PostId)
        {
            var rPost = await _uof.GenericRepository<Post>().GetByIdAsync(PostId);
            if (rPost is null) return new SingleResponse<PostDto>(false,"Post Not Found");
            var MappedPost = _mapper.Map<Post, PostDto>(rPost);
            return new SingleResponse<PostDto>(true,"Post Retrieved Successfully",MappedPost);
        }
        //Get All Posts
        public async Task<ListResponse<PostDto>> GetAllPost()
        {
            var rPosts =await _uof.GenericRepository<Post>().GetAllAsync();
            if (!rPosts.Any()) return new ListResponse<PostDto>(false, "No Posts Found",new List<PostDto>());
            var MappedPosts = _mapper.Map<IEnumerable<Post>, IReadOnlyList<PostDto>>(rPosts);
            return new ListResponse<PostDto>(true,"Posts Retrieved Successfully" ,MappedPosts.ToList());
        }
        //Delete Post
        public async Task<BaseResponse> DeletePost(string Id)
        {
            var rPost = await _uof.GenericRepository<Post>().GetByIdAsync(Id);
            if (rPost is null) return new BaseResponse(false, "Post Not Found");
            _uof.GenericRepository<Post>().Delete(rPost);
            if (await _uof.SaveChangesAsync() <= 0) return new BaseResponse(false,"Error During Delete Post");
            return new BaseResponse(true,"Post Deleted Successfully");
        }

    }
}
