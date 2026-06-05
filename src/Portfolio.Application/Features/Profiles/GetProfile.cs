using MediatR;
using Portfolio.Application.Common.Models;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Profiles;

public sealed record GetProfileQuery : IRequest<Result<ProfileDto>>;

public sealed record ProfileDto(
    string FullName,
    string Title,
    string Summary,
    string? PhotoUrl,
    string? ResumeUrl,
    string? Email,
    string? Phone,
    string? Location,
    string? LinkedInUrl,
    string? GitHubUrl,
    string? TwitterUrl,
    string? WebsiteUrl);

public sealed class GetProfileHandler : IRequestHandler<GetProfileQuery, Result<ProfileDto>>
{
    private readonly IProfileRepository _profileRepository;

    public GetProfileHandler(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<Result<ProfileDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var profiles = await _profileRepository.GetAllAsync(cancellationToken);
        var profile = profiles.FirstOrDefault();

        if (profile is null)
            return Result<ProfileDto>.Failure("Profile not found.");

        var dto = new ProfileDto(
            profile.FullName,
            profile.Title,
            profile.Summary,
            profile.PhotoUrl,
            profile.ResumeUrl,
            profile.Email,
            profile.Phone,
            profile.Location,
            profile.LinkedInUrl,
            profile.GitHubUrl,
            profile.TwitterUrl,
            profile.WebsiteUrl);

        return Result<ProfileDto>.Success(dto);
    }
}
