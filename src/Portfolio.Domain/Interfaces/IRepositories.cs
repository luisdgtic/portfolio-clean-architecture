using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Interfaces;

public interface IProfileRepository : IGenericRepository<Profile>;

public interface IExperienceRepository : IGenericRepository<Experience>;

public interface IEducationRepository : IGenericRepository<Education>;

public interface ICertificationRepository : IGenericRepository<Certification>;

public interface IBlogPostRepository : IGenericRepository<BlogPost>
{
    Task<BlogPost?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<BlogPost>> GetPublishedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}

public interface IContactMessageRepository : IGenericRepository<ContactMessage>;
