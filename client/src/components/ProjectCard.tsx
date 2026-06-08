import { Link } from 'react-router-dom';
import { Card, CardContent, CardFooter } from '@/components/ui/Card';
import { Badge } from '@/components/ui/Badge';
import type { Project } from '@/api/types';

interface ProjectCardProps {
  project: Project;
}

export function ProjectCard({ project }: ProjectCardProps) {
  return (
    <Card className="group flex flex-col overflow-hidden">
      {project.imageUrls.length > 0 && (
        <div className="-mx-6 -mt-6 mb-4 aspect-video overflow-hidden bg-slate-100 dark:bg-slate-700">
          <img
            src={project.imageUrls[0]}
            alt={project.title}
            className="h-full w-full object-cover transition-transform duration-300 group-hover:scale-105"
            loading="lazy"
          />
        </div>
      )}
      <CardContent className="flex flex-1 flex-col">
        <h3 className="mb-2 text-lg font-semibold text-slate-900 dark:text-white">
          {project.title}
        </h3>
        <p className="mb-4 flex-1 text-sm leading-relaxed text-slate-600 dark:text-slate-400">
          {project.description.length > 150
            ? project.description.slice(0, 150) + '...'
            : project.description}
        </p>
        <div className="flex flex-wrap gap-1.5">
          {project.techStack.slice(0, 5).map((tech) => (
            <Badge key={tech} variant="default">
              {tech}
            </Badge>
          ))}
          {project.techStack.length > 5 && (
            <Badge variant="outline">+{project.techStack.length - 5}</Badge>
          )}
        </div>
      </CardContent>
      <CardFooter>
        <Link
          to={`/projects/${project.id}`}
          className="text-sm font-medium text-sky-500 hover:text-sky-600 dark:hover:text-sky-400"
        >
          View Details &rarr;
        </Link>
        {project.gitHubUrl && (
          <a
            href={project.gitHubUrl}
            target="_blank"
            rel="noopener noreferrer"
            className="ml-auto text-sm text-slate-500 hover:text-slate-700 dark:text-slate-400 dark:hover:text-slate-300"
          >
            GitHub
          </a>
        )}
        {project.liveUrl && (
          <a
            href={project.liveUrl}
            target="_blank"
            rel="noopener noreferrer"
            className="text-sm text-slate-500 hover:text-slate-700 dark:text-slate-400 dark:hover:text-slate-300"
          >
            Live Demo
          </a>
        )}
      </CardFooter>
    </Card>
  );
}
