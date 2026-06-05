import { useProjects } from '@/api/hooks';
import { ProjectCard } from '@/components/ProjectCard';

export default function ProjectsPage() {
  const { data: projects, isLoading } = useProjects();

  if (isLoading) {
    return <div className="flex min-h-[60vh] items-center justify-center"><div className="h-8 w-8 animate-spin rounded-full border-4 border-sky-500 border-t-transparent" /></div>;
  }

  return (
    <div className="mx-auto max-w-6xl px-4 py-12">
      <h1 className="mb-2 text-3xl font-bold text-slate-900 dark:text-white">Projects</h1>
      <p className="mb-8 text-slate-600 dark:text-slate-400">
        A selection of projects I have built and contributed to.
      </p>

      {projects && projects.length > 0 ? (
        <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
          {projects.map((project) => (
            <ProjectCard key={project.id} project={project} />
          ))}
        </div>
      ) : (
        <p className="text-center text-slate-500 dark:text-slate-400">No projects to display yet.</p>
      )}
    </div>
  );
}
