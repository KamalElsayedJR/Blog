using AutoMapper;
using Blog.Application.DTOs.Category;
using Blog.Application.DTOs.Comment;
using Blog.Application.DTOs.Post;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrUpdatePostDto,Post>();
            //CreateMap<Post, PostDto>().ForMember(P=>P.Comments);
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => c.Text).ToList()))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
            //CreateMap<Post, PostDto>()
            //         .ForMember(dest => dest.Comments,
            //                    opt => opt.MapFrom(src => src.Comments.Select(c => new CommentWithUser
            //                    {
            //                        Comment = c.Text,
            //                        UserName = c.UserName  // بما إنه مفيش جدول User، نرجع UserId
            //                    }).ToList()))
            //         .ForMember(dest => dest.Category,
            //                    opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Category, CategoryDto>();
        }

    }
}

