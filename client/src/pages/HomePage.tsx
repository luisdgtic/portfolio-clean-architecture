import { Link } from 'react-router-dom';
import { Button } from '@/components/ui/Button';
import { Badge } from '@/components/ui/Badge';
import { useProfile, useProjects, useSkills, useExperiences } from '@/api/hooks';

export default function HomePage() {
  const { data: profile, isLoading: profileLoading } = useProfile();
  const { data: projects } = useProjects(true);
  const { data: skills } = useSkills();
  const { data: experiences } = useExperiences();

  if (profileLoading) {
    return <div className="flex min-h-[60vh] items-center justify-center"><div className="h-8 w-8 animate-spin rounded-full border-4 border-sky-500 border-t-transparent" /></div>;
  }

  return (
    <div>
      <section className="bg-gradient-to-br from-sky-50 to-white px-4 py-20 dark:from-sky-950 dark:to-slate-950">
        <div className="mx-auto max-w-4xl text-center">
          <h1 className="mb-4 text-4xl font-bold tracking-tight text-slate-900 sm:text-5xl dark:text-white">
            {profile?.fullName}
          </h1>
          <p className="mb-2 text-xl text-sky-600 dark:text-sky-400">{profile?.title}</p>
          <p className="mx-auto mb-8 max-w-2xl text-lg leading-relaxed text-slate-600 dark:text-slate-400">
            {profile?.summary}
          </p>
          <div className="flex flex-wrap justify-center gap-3">
            <Link to="/projects"><Button>View Projects</Button></Link>
            <Link to="/contact"><Button variant="outline">Get in Touch</Button></Link>
          </div>

          {(profile?.linkedInUrl || profile?.gitHubUrl || profile?.resumeUrl) && (
            <div className="mt-6 flex flex-wrap justify-center gap-3">
              {profile.linkedInUrl && (
                <a href={profile.linkedInUrl} target="_blank" rel="noopener noreferrer">
                  <Button variant="outline">
                    <svg className="mr-2 h-4 w-4" fill="currentColor" viewBox="0 0 24 24"><path d="M20.447 20.452h-3.554v-5.569c0-1.328-.027-3.037-1.852-3.037-1.853 0-2.136 1.445-2.136 2.939v5.667H9.351V9h3.414v1.561h.046c.477-.9 1.637-1.85 3.37-1.85 3.601 0 4.267 2.37 4.267 5.455v6.286zM5.337 7.433a2.062 2.062 0 01-2.063-2.065 2.064 2.064 0 112.063 2.065zm1.782 13.019H3.555V9h3.564v11.452zM22.225 0H1.771C.792 0 0 .774 0 1.729v20.542C0 23.227.792 24 1.771 24h20.451C23.2 24 24 23.227 24 22.271V1.729C24 .774 23.2 0 22.222 0h.003z"/></svg>
                    LinkedIn
                  </Button>
                </a>
              )}
              {profile.gitHubUrl && (
                <a href={profile.gitHubUrl} target="_blank" rel="noopener noreferrer">
                  <Button variant="outline">
                    <svg className="mr-2 h-4 w-4" fill="currentColor" viewBox="0 0 24 24"><path d="M12 0C5.374 0 0 5.373 0 12c0 5.302 3.438 9.8 8.207 11.387.599.111.793-.261.793-.577v-2.234c-3.338.726-4.033-1.416-4.033-1.416-.546-1.387-1.333-1.756-1.333-1.756-1.089-.745.083-.729.083-.729 1.205.084 1.839 1.237 1.839 1.237 1.07 1.834 2.807 1.304 3.492.997.107-.775.418-1.305.762-1.604-2.665-.305-5.467-1.334-5.467-5.931 0-1.311.469-2.381 1.236-3.221-.124-.303-.535-1.524.117-3.176 0 0 1.008-.322 3.301 1.23A11.509 11.509 0 0112 5.803c1.02.005 2.047.138 3.006.404 2.291-1.552 3.297-1.23 3.297-1.23.653 1.653.242 2.874.118 3.176.77.84 1.235 1.911 1.235 3.221 0 4.609-2.807 5.624-5.479 5.921.43.372.823 1.102.823 2.222v3.293c0 .319.192.694.801.576C20.566 21.797 24 17.3 24 12c0-6.627-5.373-12-12-12z"/></svg>
                    GitHub
                  </Button>
                </a>
              )}
              {profile.resumeUrl && (
                <a href={profile.resumeUrl} target="_blank" rel="noopener noreferrer">
                  <Button variant="outline">
                    <svg className="mr-2 h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth={2}><path strokeLinecap="round" strokeLinejoin="round" d="M12 10v6m0 0l-3-3m3 3l3-3m2 8H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"/></svg>
                    Download CV
                  </Button>
                </a>
              )}
            </div>
          )}
        </div>
      </section>

      {projects && projects.length > 0 && (
        <section className="px-4 py-16">
          <div className="mx-auto max-w-6xl">
            <h2 className="mb-8 text-center text-3xl font-bold text-slate-900 dark:text-white">
              Featured Projects
            </h2>
            <div className="grid gap-6 sm:grid-cols-2">
              {projects.slice(0, 2).map((project) => (
                <Link key={project.id} to={`/projects/${project.id}`} className="group">
                  <div className="rounded-xl border border-slate-200 bg-white p-6 shadow-sm transition-shadow hover:shadow-md dark:border-slate-700 dark:bg-slate-800">
                    <h3 className="mb-2 text-lg font-semibold text-slate-900 dark:text-white">{project.title}</h3>
                    <p className="mb-3 text-sm text-slate-600 dark:text-slate-400">
                      {project.description.slice(0, 120)}...
                    </p>
                    <div className="flex flex-wrap gap-1.5">
                      {project.techStack.slice(0, 4).map((t) => (
                        <Badge key={t} variant="default">{t}</Badge>
                      ))}
                    </div>
                  </div>
                </Link>
              ))}
            </div>
            {projects.length > 2 && (
              <div className="mt-6 text-center">
                <Link to="/projects"><Button variant="ghost">View All Projects &rarr;</Button></Link>
              </div>
            )}
          </div>
        </section>
      )}

      {skills && skills.length > 0 && (
        <section className="bg-slate-50 px-4 py-16 dark:bg-slate-900">
          <div className="mx-auto max-w-6xl text-center">
            <h2 className="mb-8 text-3xl font-bold text-slate-900 dark:text-white">Skills</h2>
            <div className="flex flex-wrap justify-center gap-2">
              {skills.slice(0, 12).map((skill) => (
                <Badge key={skill.id} variant="primary">{skill.name}</Badge>
              ))}
            </div>
            <div className="mt-6">
              <Link to="/skills"><Button variant="ghost">View All Skills &rarr;</Button></Link>
            </div>
          </div>
        </section>
      )}

      {experiences && experiences.length > 0 && (
        <section className="px-4 py-16">
          <div className="mx-auto max-w-3xl">
            <h2 className="mb-8 text-center text-3xl font-bold text-slate-900 dark:text-white">Experience</h2>
            <div className="space-y-6">
              {experiences.slice(0, 2).map((exp) => (
                <div key={exp.id} className="rounded-xl border border-slate-200 bg-white p-5 dark:border-slate-700 dark:bg-slate-800">
                  <h3 className="font-semibold text-slate-900 dark:text-white">{exp.position}</h3>
                  <p className="text-sm text-sky-600 dark:text-sky-400">{exp.company}</p>
                  {exp.description && <p className="mt-2 text-sm text-slate-600 dark:text-slate-400">{exp.description.slice(0, 150)}...</p>}
                </div>
              ))}
            </div>
            <div className="mt-6 text-center">
              <Link to="/experience"><Button variant="ghost">View Full Experience &rarr;</Button></Link>
            </div>
          </div>
        </section>
      )}
    </div>
  );
}
