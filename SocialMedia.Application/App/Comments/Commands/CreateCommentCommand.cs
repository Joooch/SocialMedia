using AutoMapper;
using MediatR;
using SocialMedia.Application.App.Comments.Responses;
using SocialMedia.Application.Common.Interfaces.Repository;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.App.Comments.Commands
{
    public class CreateCommentRequest
    {
        public string PostId { get; set; }
        public string Content { get; set; }
    }

    public class CreateCommentCommand : IRequest<CommentDto>
    {
        public CreateCommentRequest Request { get; set; }
        public Guid UserId { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, IPostsRepository postsRepository, IProfileRepository profileRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _profileRepository = profileRepository;
            _postsRepository = postsRepository;
            _mapper = mapper;
        }

        public async Task<CommentDto> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            var postId = Guid.Parse(request.PostId);

            var isValidPost = await _postsRepository.IsExistsById(postId);
            if (!isValidPost)
            {
                throw new Exception("Post not found");
            }

            var user = await _profileRepository.GetByUserId(command.UserId);

            var comment = new CommentEntity
            {
                Content = request.Content,
                Owner = user!,
                CreatedAt = DateTime.Now,
                PostId = postId
            };

            _commentRepository.Add(comment);
            await _commentRepository.SaveAsync();

            return _mapper.Map<CommentDto>(comment);
        }
    }
}
