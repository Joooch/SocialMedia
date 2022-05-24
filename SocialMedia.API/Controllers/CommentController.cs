using SocialMedia.Application.Common.Interfaces.Repository;

namespace SocialMedia.API.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController( ICommentRepository commentRepository )
        {
            _commentRepository = commentRepository;
        }
    }
}
