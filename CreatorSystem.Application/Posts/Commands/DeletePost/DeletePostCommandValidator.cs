using FluentValidation;

namespace CreatorSystem.Application.Posts.Commands.DeletePost
{
    public class UpdatePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();         
        }
    }
}
