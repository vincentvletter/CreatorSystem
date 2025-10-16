using FluentValidation;

namespace CreatorSystem.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200);
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content cannot be empty.");
            RuleFor(x => x.ScheduledAt)
                .GreaterThan(DateTime.UtcNow.AddMinutes(-1))
                .WithMessage("Scheduled date must be in the future or now.");
        }
    }
}
