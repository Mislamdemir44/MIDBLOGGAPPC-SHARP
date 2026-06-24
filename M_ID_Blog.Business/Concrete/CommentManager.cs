using AutoMapper;
using M_ID_Blog.Business.Abstract;
using M_ID_Blog.Business.DTOs.Comment;
using M_ID_Blog.DataAccess.Concrete.UnitOfWork;
using M_ID_Blog.Entities.Concrete;

namespace M_ID_Blog.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentManager(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommentDto?> GetByIdAsync(int commentId)
        {
            var comment = await _unitOfWork.Comments.GetAsync(
                c => c.Id == commentId && c.IsActive,
                c => c.User, c => c.Replies);
                
            if (comment == null)
                return null;
                
            var commentDto = _mapper.Map<CommentDto>(comment);
            commentDto.LikesCount = await _unitOfWork.Comments.GetCommentLikesCountAsync(commentId);
            
            return commentDto;
        }

        public async Task<IList<CommentListDto>> GetAllByPostIdAsync(int postId)
        {
            var comments = await _unitOfWork.Comments.GetAllByPostIdAsync(postId);
            var commentDtos = _mapper.Map<IList<CommentListDto>>(comments);
            
            foreach (var commentDto in commentDtos)
            {
                commentDto.LikesCount = await _unitOfWork.Comments.GetCommentLikesCountAsync(commentDto.Id);
            }
            
            return commentDtos;
        }

        public async Task<IList<CommentListDto>> GetAllByUserIdAsync(string userId)
        {
            var comments = await _unitOfWork.Comments.GetAllByUserIdAsync(userId);
            var commentDtos = _mapper.Map<IList<CommentListDto>>(comments);
            
            foreach (var commentDto in commentDtos)
            {
                commentDto.LikesCount = await _unitOfWork.Comments.GetCommentLikesCountAsync(commentDto.Id);
            }
            
            return commentDtos;
        }

        public async Task<CommentDto> AddAsync(CommentAddDto commentAddDto)
        {
            var comment = _mapper.Map<Comment>(commentAddDto);
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();
            
            return _mapper.Map<CommentDto>(
                await _unitOfWork.Comments.GetAsync(
                    c => c.Id == comment.Id,
                    c => c.User));
        }

        public async Task<CommentDto> AddReplyAsync(CommentReplyDto commentReplyDto)
        {
            var comment = _mapper.Map<Comment>(commentReplyDto);
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();
            
            return _mapper.Map<CommentDto>(
                await _unitOfWork.Comments.GetAsync(
                    c => c.Id == comment.Id,
                    c => c.User));
        }

        public async Task<CommentDto> UpdateAsync(CommentUpdateDto commentUpdateDto)
        {
            var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentUpdateDto.Id);
            if (comment != null && comment.UserId == commentUpdateDto.UserId)
            {
                comment = _mapper.Map(commentUpdateDto, comment);
                comment.ModifiedDate = DateTime.UtcNow;
                await _unitOfWork.Comments.UpdateAsync(comment);
                await _unitOfWork.SaveAsync();
            }
            
            return _mapper.Map<CommentDto>(
                await _unitOfWork.Comments.GetAsync(
                    c => c.Id == comment.Id,
                    c => c.User));
        }

        public async Task DeleteAsync(int commentId)
        {
            var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId);
            if (comment != null)
            {
                comment.IsActive = false;
                await _unitOfWork.Comments.UpdateAsync(comment);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<bool> LikeCommentAsync(int commentId, string userId)
        {
            var existingLike = await _unitOfWork.CommentLikes.GetByCommentAndUserIdAsync(commentId, userId);
            if (existingLike != null)
                return false;
                
            var commentLike = new CommentLike
            {
                CommentId = commentId,
                UserId = userId
            };
            
            await _unitOfWork.CommentLikes.AddAsync(commentLike);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UnlikeCommentAsync(int commentId, string userId)
        {
            var commentLike = await _unitOfWork.CommentLikes.GetByCommentAndUserIdAsync(commentId, userId);
            if (commentLike == null)
                return false;
                
            await _unitOfWork.CommentLikes.DeleteAsync(commentLike);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}