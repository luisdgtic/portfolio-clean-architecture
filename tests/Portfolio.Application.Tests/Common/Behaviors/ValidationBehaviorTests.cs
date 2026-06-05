using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;

namespace Portfolio.Application.Tests.Common.Behaviors;

public sealed class ValidationBehaviorTests
{
    [Fact]
    public async Task Handle_ShouldCallNext_WhenNoValidators()
    {
        var validators = Enumerable.Empty<IValidator<TestRequest>>();
        var behavior = new Application.Common.Behaviors.ValidationBehavior<TestRequest, TestResponse>(validators);
        var expectedResponse = new TestResponse();

        var result = await behavior.Handle(
            new TestRequest(),
            _ => Task.FromResult(expectedResponse),
            CancellationToken.None);

        result.Should().Be(expectedResponse);
    }

    [Fact]
    public async Task Handle_ShouldCallNext_WhenValidationPasses()
    {
        var validator = new Mock<IValidator<TestRequest>>();
        validator.Setup(v => v.ValidateAsync(
                It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var behavior = new Application.Common.Behaviors.ValidationBehavior<TestRequest, TestResponse>(
            new[] { validator.Object });

        var expectedResponse = new TestResponse();

        var result = await behavior.Handle(
            new TestRequest(),
            _ => Task.FromResult(expectedResponse),
            CancellationToken.None);

        result.Should().Be(expectedResponse);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenValidationFails()
    {
        var validator = new Mock<IValidator<TestRequest>>();
        validator.Setup(v => v.ValidateAsync(
                It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[]
            {
                new ValidationFailure("Name", "Name is required")
            }));

        var behavior = new Application.Common.Behaviors.ValidationBehavior<TestRequest, TestResponse>(
            new[] { validator.Object });

        var act = () => behavior.Handle(
            new TestRequest(),
            _ => Task.FromResult(new TestResponse()),
            CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();
    }

}

public sealed record TestRequest : IRequest<TestResponse>;

public sealed record TestResponse;
