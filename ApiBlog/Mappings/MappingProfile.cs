using ApiBlog.DTOs;
using ApiBlog.Models;
using AutoMapper;

namespace ApiBlog.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();


            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
        }
    }
}
