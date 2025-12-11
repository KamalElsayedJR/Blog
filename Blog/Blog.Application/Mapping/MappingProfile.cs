using AutoMapper;
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
            CreateMap<Post, PostDto>();
        }

    }
}

