using FluentValidation;

namespace CreatorSystem.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must be under 200 characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.");

        RuleFor(x => x.Platform)
            .NotEmpty().WithMessage("Platform is required.")
            .Must(p => new[] { "instagram", "facebook", "tiktok", "linkedin" }
                .Contains(p.ToLower()))
            .WithMessage("Platform must be one of: instagram, facebook, tiktok, linkedin.");

        RuleFor(x => x.ScheduledAt)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Scheduled time must be in the future.");
    }
}
