using M_ID_Blog.Business.DTOs.User;

namespace M_ID_Blog.Business.DTOs.Comment
{
    public class CommentListDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
        public int LikesCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        
        public UserDto User { get; set; } = null!;
        public IList<CommentListDto> Replies { get; set; } = new List<CommentListDto>();
    }
}