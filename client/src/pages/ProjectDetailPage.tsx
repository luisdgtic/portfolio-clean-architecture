import { useParams, Link } from 'react-router-dom';
import { Button } from '@/components/ui/Button';
import { Badge } from '@/components/ui/Badge';
import { useProject } from '@/api/hooks';

export default function ProjectDetailPage() {
  const { id } = useParams<{ id: string }>();
  const { data: project, isLoading } = useProject(id!);

  if (isLoading) {
    return <div className="flex min-h-[60vh] items-center justify-center"><div className="h-8 w-8 animate-spin rounded-full border-4 border-sky-500 border-t-transparent" /></div>;
  }

  if (!project) {
    return (
      <div className="mx-auto max-w-3xl px-4 py-12 text-center">
        <h1 className="mb-4 text-2xl font-bold text-slate-900 dark:text-white">Project Not Found</h1>
        <Link to="/projects"><Button variant="ghost">Back to Projects</Button></Link>
      </div>
    );
  }

  return (
    <div className="mx-auto max-w-4xl px-4 py-12">
      <Link to="/projects" className="mb-6 inline-block text-sm text-sky-500 hover:text-sky-600">&larr; Back to Projects</Link>

      {project.imageUrl && (
        <div className="mb-8 overflow-hidden rounded-xl bg-slate-100 dark:bg-slate-800">
          <img src={project.imageUrl} alt={project.title} className="w-full object-cover" loading="lazy" />
        </div>
      )}

      <h1 className="mb-4 text-3xl font-bold text-slate-900 dark:text-white">{project.title}</h1>

      <div className="mb-6 flex flex-wrap gap-1.5">
        {project.techStack.map((tech) => (
          <Badge key={tech} variant="default">{tech}</Badge>
        ))}
      </div>

      <p className="mb-8 text-lg leading-relaxed text-slate-600 dark:text-slate-400">{project.description}</p>

      <div className="flex flex-wrap gap-3">
        {project.gitHubUrl && (
          <a href={project.gitHubUrl} target="_blank" rel="noopener noreferrer">
            <Button>View on GitHub</Button>
          </a>
        )}
        {project.liveUrl && (
          <a href={project.liveUrl} target="_blank" rel="noopener noreferrer">
            <Button variant="outline">Live Demo</Button>
          </a>
        )}
      </div>
    </div>
  );
}
