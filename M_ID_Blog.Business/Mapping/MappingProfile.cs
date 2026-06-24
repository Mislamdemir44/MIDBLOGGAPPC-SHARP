using AutoMapper;
using M_ID_Blog.Business.DTOs.Category;
using M_ID_Blog.Business.DTOs.Comment;
using M_ID_Blog.Business.DTOs.Post;
using M_ID_Blog.Business.DTOs.User;
using M_ID_Blog.Entities.Concrete;

namespace M_ID_Blog.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<AppUser, UserDto>()
                .ReverseMap();
            CreateMap<UserUpdateDto, AppUser>();
            
            // Category mappings
            CreateMap<Category, CategoryDto>()
                .ReverseMap();
            CreateMap<CategoryAddDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            
            // Post mappings
            CreateMap<Post, PostDto>()
                .ReverseMap();
            CreateMap<Post, PostListDto>();
            CreateMap<PostAddDto, Post>()
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.VideoPath, opt => opt.Ignore());
            CreateMap<PostUpdateDto, Post>()
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.VideoPath, opt => opt.Ignore());
                
            // Comment mappings
            CreateMap<Comment, CommentDto>()
                .ReverseMap();
            CreateMap<Comment, CommentListDto>();
            CreateMap<CommentAddDto, Comment>();
            CreateMap<CommentReplyDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>()
                .ForMember(dest => dest.PostId, opt => opt.Ignore())
                .ForMember(dest => dest.ParentCommentId, opt => opt.Ignore());
        }
    }
}