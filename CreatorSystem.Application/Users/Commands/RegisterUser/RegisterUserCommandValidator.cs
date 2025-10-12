using CreatorSystem.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace CreatorSystem.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IAppDbContext _context;

        public RegisterUserCommandValidator (IAppDbContext context)
        {
            _context = context;

            // Email validation
            RuleFor(command => command.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required.")
            .Must(BeValidEmail).WithMessage("Please enter a valid email address (e.g. name@example.com).")
            .MaximumLength(254).WithMessage("Email address is too long.")
            .MustAsync(BeUniqueEmail).WithMessage("An account with this email already exists.");

            // Password validation
            RuleFor(command => command.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            // Confirm password validation
            RuleFor(command => command.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match.");
        }

        /// <summary>
        /// Validates email format using a regex pattern.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool BeValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (email.Contains(' ') || email.Contains("..")) return false;

            // Parse via .NET (beter dan regex)
            if (!MailAddress.TryCreate(email, out var addr)) return false;

            // Zorg dat wat geparsed is ook gelijk is aan de input (voorkomt rare normalisaties)
            if (!string.Equals(addr.Address, email, StringComparison.OrdinalIgnoreCase)) return false;

            // Eenvoudige TLD-check: host moet punt bevatten en tld ≥ 2
            var hostParts = addr.Host.Split('.');
            if (hostParts.Length < 2) return false;
            var tld = hostParts[^1];
            if (tld.Length < 2) return false;

            // (optioneel) lokale deel max 64 chars
            var local = email.Split('@')[0];
            if (local.Length is < 1 or > 64) return false;

            return true;
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var normalized = email.Trim().ToLowerInvariant();
            return !await _context.Users.AnyAsync(user => user.Email == normalized, cancellationToken); 
        }
    }
}

