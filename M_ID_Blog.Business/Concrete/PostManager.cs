using AutoMapper;
using M_ID_Blog.Business.Abstract;
using M_ID_Blog.Business.DTOs.Comment;
using M_ID_Blog.Business.DTOs.Post;
using M_ID_Blog.Business.Helpers;
using M_ID_Blog.DataAccess.Concrete.UnitOfWork;
using M_ID_Blog.Entities.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace M_ID_Blog.Business.Concrete
{
    public class PostManager : IPostService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostManager(UnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<PostDto?> GetByIdAsync(int postId)
        {
            var post = await _unitOfWork.Posts.GetAsync(
                p => p.Id == postId && p.IsActive,
                p => p.User, p => p.Category);
                
            if (post == null)
                return null;
                
            var postDto = _mapper.Map<PostDto>(post);
            
            var comments = await _unitOfWork.Comments.GetAllByPostIdAsync(postId);
            postDto.Comments = _mapper.Map<IList<CommentListDto>>(comments);
            
            return postDto;
        }

        public async Task<IList<PostListDto>> GetAllAsync()
        {
            var posts = await _unitOfWork.Posts.GetAllWithDetailsAsync();
            var postDtos = _mapper.Map<IList<PostListDto>>(posts);
            
            foreach (var postDto in postDtos)
            {
                postDto.CommentCount = await _unitOfWork.Comments.CountAsync(c => c.PostId == postDto.Id && c.IsActive);
            }
            
            return postDtos;
        }

        public async Task<IList<PostListDto>> GetAllByUserIdAsync(string userId)
        {
            var posts = await _unitOfWork.Posts.GetAllByUserIdAsync(userId);
            var postDtos = _mapper.Map<IList<PostListDto>>(posts);
            
            foreach (var postDto in postDtos)
            {
                postDto.CommentCount = await _unitOfWork.Comments.CountAsync(c => c.PostId == postDto.Id && c.IsActive);
            }
            
            return postDtos;
        }

        public async Task<IList<PostListDto>> GetAllByCategoryIdAsync(int categoryId)
        {
            var posts = await _unitOfWork.Posts.GetAllByCategoryIdAsync(categoryId);
            var postDtos = _mapper.Map<IList<PostListDto>>(posts);
            
            foreach (var postDto in postDtos)
            {
                postDto.CommentCount = await _unitOfWork.Comments.CountAsync(c => c.PostId == postDto.Id && c.IsActive);
            }
            
            return postDtos;
        }

        public async Task<PostDto> AddAsync(PostAddDto postAddDto)
        {
            var post = _mapper.Map<Post>(postAddDto);
            
            if (postAddDto.Image != null)
            {
                post.ImagePath = await FileHelper.SaveFileAsync(postAddDto.Image, "posts", _webHostEnvironment);
            }
            
            if (postAddDto.Video != null)
            {
                post.VideoPath = await FileHelper.SaveFileAsync(postAddDto.Video, "posts", _webHostEnvironment);
            }
            
            await _unitOfWork.Posts.AddAsync(post);
            await _unitOfWork.SaveAsync();
            
            return _mapper.Map<PostDto>(
                await _unitOfWork.Posts.GetAsync(
                    p => p.Id == post.Id, 
                    p => p.User, p => p.Category));
        }

        public async Task<PostDto> UpdateAsync(PostUpdateDto postUpdateDto)
        {
            var post = await _unitOfWork.Posts.GetAsync(p => p.Id == postUpdateDto.Id);
            
            if (post != null)
            {
                var oldImagePath = post.ImagePath;
                var oldVideoPath = post.VideoPath;
                
                post = _mapper.Map(postUpdateDto, post);
                post.ModifiedDate = DateTime.UtcNow;
                
                // Handle image update
                if (postUpdateDto.Image != null)
                {
                    post.ImagePath = await FileHelper.SaveFileAsync(postUpdateDto.Image, "posts", _webHostEnvironment);
                    
                    // Delete old image if exists and a new one is uploaded
                    if (!string.IsNullOrEmpty(oldImagePath) && !postUpdateDto.KeepExistingImage)
                    {
                        FileHelper.DeleteFile(oldImagePath, _webHostEnvironment);
                    }
                }
                else if (!postUpdateDto.KeepExistingImage)
                {
                    // Delete image if user unchecked keep existing
                    if (!string.IsNullOrEmpty(oldImagePath))
                    {
                        FileHelper.DeleteFile(oldImagePath, _webHostEnvironment);
                    }
                    post.ImagePath = null;
                }
                
                // Handle video update
                if (postUpdateDto.Video != null)
                {
                    post.VideoPath = await FileHelper.SaveFileAsync(postUpdateDto.Video, "posts", _webHostEnvironment);
                    
                    // Delete old video if exists and a new one is uploaded
                    if (!string.IsNullOrEmpty(oldVideoPath) && !postUpdateDto.KeepExistingVideo)
                    {
                        FileHelper.DeleteFile(oldVideoPath, _webHostEnvironment);
                    }
                }
                else if (!postUpdateDto.KeepExistingVideo)
                {
                    // Delete video if user unchecked keep existing
                    if (!string.IsNullOrEmpty(oldVideoPath))
                    {
                        FileHelper.DeleteFile(oldVideoPath, _webHostEnvironment);
                    }
                    post.VideoPath = null;
                }
                
                await _unitOfWork.Posts.UpdateAsync(post);
                await _unitOfWork.SaveAsync();
            }
            
            return _mapper.Map<PostDto>(
                await _unitOfWork.Posts.GetAsync(
                    p => p.Id == post.Id, 
                    p => p.User, p => p.Category));
        }

        public async Task DeleteAsync(int postId)
        {
            var post = await _unitOfWork.Posts.GetAsync(p => p.Id == postId);
            if (post != null)
            {
                post.IsActive = false;
                await _unitOfWork.Posts.UpdateAsync(post);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task IncrementViewCountAsync(int postId)
        {
            await _unitOfWork.Posts.IncrementViewCountAsync(postId);
            await _unitOfWork.SaveAsync();
        }
    }
}