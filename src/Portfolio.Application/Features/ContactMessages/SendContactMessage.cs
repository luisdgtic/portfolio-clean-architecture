using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Application.Common.Models;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.ContactMessages;

public sealed record SendContactMessageCommand(
    string Name,
    string Email,
    string Subject,
    string Body) : IRequest<Result<SendContactMessageResponse>>;

public sealed record SendContactMessageResponse(Guid Id, string Message);

public sealed class SendContactMessageValidator : AbstractValidator<SendContactMessageCommand>
{
    public SendContactMessageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(200)
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required.")
            .MaximumLength(200);

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Message body is required.")
            .MaximumLength(5000);
    }
}

public sealed class SendContactMessageHandler : IRequestHandler<SendContactMessageCommand, Result<SendContactMessageResponse>>
{
    private readonly IContactMessageRepository _contactMessageRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<SendContactMessageHandler> _logger;

    public SendContactMessageHandler(
        IContactMessageRepository contactMessageRepository,
        IEmailService emailService,
        ILogger<SendContactMessageHandler> logger)
    {
        _contactMessageRepository = contactMessageRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Result<SendContactMessageResponse>> Handle(SendContactMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new ContactMessage
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            Subject = request.Subject.Trim(),
            Body = request.Body.Trim(),
            SentAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        await _contactMessageRepository.AddAsync(message, cancellationToken);

        _ = Task.Run(async () =>
        {
            try
            {
                await _emailService.SendEmailAsync(
                    message.Email,
                    $"Thank you for your message: {message.Subject}",
                    $"Hi {message.Name},\n\nThank you for reaching out. I'll get back to you soon.\n\nBest regards",
                    CancellationToken.None);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to send confirmation email for contact message {Id}", message.Id);
            }
        }, CancellationToken.None);

        var response = new SendContactMessageResponse(message.Id, "Message sent successfully.");
        return Result<SendContactMessageResponse>.Success(response);
    }
}
