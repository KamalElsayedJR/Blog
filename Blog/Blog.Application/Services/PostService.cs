using AutoMapper;
using Blog.Application.DTOs;
using Blog.Application.DTOs.Post;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Domain.Specifications;
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
        //Get Post By Id
        public async Task<SingleResponse<PostDto>> GetPostByIdAsync(string PostId)
        {
            var spec = new PostWithCommentsSpecs(PostId);
            var rPost = await _uof.GenericRepository<Post>().GetByIdWithSpecAsync(spec);
            if (rPost is null) return new SingleResponse<PostDto>(false,"Post Not Found");
            var MappedPost = _mapper.Map<Post, PostDto>(rPost);
           
            return new SingleResponse<PostDto>(true,"Post Retrieved Successfully",MappedPost);
        }
        //Get All Posts
        public async Task<ListResponse<PostDto>> GetAllPostAsync()
        {
            var spec = new PostWithCommentsSpecs();
            var rPosts =await _uof.GenericRepository<Post>().GetAllWithSpecAsync(spec);
            if (!rPosts.Any()) return new ListResponse<PostDto>(false, "No Posts Found",new List<PostDto>());
            var MappedPosts = _mapper.Map<IEnumerable<Post>, IReadOnlyList<PostDto>>(rPosts);  
            return new ListResponse<PostDto>(true,"Posts Retrieved Successfully" ,MappedPosts.ToList());
        }
        //Create Post
        public async Task<BaseResponse> CreatePostAsync(CreateOrUpdatePostDto dto,string UserId)
        {
            var MappedPost = _mapper.Map<CreateOrUpdatePostDto, Post>(dto);

            //var category = await _uof.CategoryRepository.GetCategoryByName(dto.CategoryName);
            if (_uof == null) throw new Exception("_uof is null");
            if (_uof.CategoryRepository == null) throw new Exception("CategoryRepository is null");

            var category = await _uof.CategoryRepository.GetCategoryByName(dto.CategoryName);
            if (category is null) return new BaseResponse(false,"Category Not Found");
            MappedPost.CategoryId = category.Id;
            MappedPost.UserId = UserId;
            await _uof.GenericRepository<Post>().AddAsync(MappedPost);
            if (await _uof.SaveChangesAsync() <= 0) return new BaseResponse(false,"Couldnt Create Post");
            return new BaseResponse(true,"Post Created Successfuly");
        }
        //Update Post
        public async Task<BaseResponse> UpdatePostAsync(CreateOrUpdatePostDto dto, string PostId)
        {
            var Post = await _uof.GenericRepository<Post>().GetByIdAsync(PostId);
            if(Post is null) return new BaseResponse(false,"Post Not Found");
            _mapper.Map(dto, Post);
            _uof.GenericRepository<Post>().Update(Post);
            if(await _uof.SaveChangesAsync() <=0 ) return new BaseResponse(false,"Couldnt Update Post");
            return new BaseResponse(true,"Post Updated Successfuly");
        }
        //Delete Post
        public async Task<BaseResponse> DeletePostAsync(string Id)
        {
            var rPost = await _uof.GenericRepository<Post>().GetByIdAsync(Id);
            if (rPost is null) return new BaseResponse(false, "Post Not Found");
            _uof.GenericRepository<Post>().Delete(rPost);
            if (await _uof.SaveChangesAsync() <= 0) return new BaseResponse(false,"Error During Delete Post");
            return new BaseResponse(true,"Post Deleted Successfully");
        }
    }
}
