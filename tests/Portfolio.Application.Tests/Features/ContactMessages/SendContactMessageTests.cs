using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using Microsoft.Extensions.Logging;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Application.Features.ContactMessages;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Tests.Features.ContactMessages;

public sealed class SendContactMessageValidatorTests
{
    private readonly SendContactMessageValidator _validator;

    public SendContactMessageValidatorTests()
    {
        _validator = new SendContactMessageValidator();
    }

    [Fact]
    public void Should_Pass_WhenCommandIsValid()
    {
        var command = new SendContactMessageCommand("Test User", "test@example.com", "Hello", "Message body");

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Fail_WhenNameIsEmpty()
    {
        var command = new SendContactMessageCommand("", "test@example.com", "Hello", "Body");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Fail_WhenEmailIsInvalid()
    {
        var command = new SendContactMessageCommand("User", "invalid-email", "Hello", "Body");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_Fail_WhenSubjectIsEmpty()
    {
        var command = new SendContactMessageCommand("User", "test@example.com", "", "Body");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Subject);
    }

    [Fact]
    public void Should_Fail_WhenBodyIsEmpty()
    {
        var command = new SendContactMessageCommand("User", "test@example.com", "Hello", "");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Body);
    }
}

public sealed class SendContactMessageHandlerTests
{
    private readonly Mock<IContactMessageRepository> _repositoryMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<ILogger<SendContactMessageHandler>> _loggerMock;
    private readonly SendContactMessageHandler _handler;

    public SendContactMessageHandlerTests()
    {
        _repositoryMock = new Mock<IContactMessageRepository>();
        _emailServiceMock = new Mock<IEmailService>();
        _loggerMock = new Mock<ILogger<SendContactMessageHandler>>();
        _handler = new SendContactMessageHandler(
            _repositoryMock.Object, _emailServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddMessageAndReturnSuccess()
    {
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<ContactMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ContactMessage());

        var command = new SendContactMessageCommand("Test User", "test@example.com", "Hello", "Message body");

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Message.Should().Be("Message sent successfully.");

        _repositoryMock.Verify(r => r.AddAsync(
            It.Is<ContactMessage>(m =>
                m.Name == "Test User" &&
                m.Email == "test@example.com" &&
                m.Subject == "Hello" &&
                m.Body == "Message body"),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldTrimWhitespace()
    {
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<ContactMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ContactMessage());

        var command = new SendContactMessageCommand("  User  ", "  TEST@EXAMPLE.COM  ", "  Hello  ", "  Body  ");

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();

        _repositoryMock.Verify(r => r.AddAsync(
            It.Is<ContactMessage>(m =>
                m.Name == "User" &&
                m.Email == "test@example.com" &&
                m.Subject == "Hello" &&
                m.Body == "Body"),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
