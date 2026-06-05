using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Data.Repositories;

public sealed class ProfileRepository : GenericRepository<Profile>, IProfileRepository
{
    public ProfileRepository(PortfolioDbContext context) : base(context)
    {
    }
}

public sealed class ExperienceRepository : GenericRepository<Experience>, IExperienceRepository
{
    public ExperienceRepository(PortfolioDbContext context) : base(context)
    {
    }
}

public sealed class EducationRepository : GenericRepository<Education>, IEducationRepository
{
    public EducationRepository(PortfolioDbContext context) : base(context)
    {
    }
}

public sealed class CertificationRepository : GenericRepository<Certification>, ICertificationRepository
{
    public CertificationRepository(PortfolioDbContext context) : base(context)
    {
    }
}

public sealed class ContactMessageRepository : GenericRepository<ContactMessage>, IContactMessageRepository
{
    public ContactMessageRepository(PortfolioDbContext context) : base(context)
    {
    }
}
