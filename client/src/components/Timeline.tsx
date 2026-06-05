import type { Experience } from '@/api/types';
import { Badge } from '@/components/ui/Badge';

interface TimelineProps {
  experiences: Experience[];
}

function formatDate(dateStr: string): string {
  const date = new Date(dateStr);
  return date.toLocaleDateString('en-US', { year: 'numeric', month: 'short' });
}

export function Timeline({ experiences }: TimelineProps) {
  return (
    <div className="relative ml-4 border-l-2 border-sky-200 pl-8 dark:border-sky-800">
      {experiences.map((exp, index) => (
        <div key={exp.id} className={`relative mb-10 ${index === experiences.length - 1 ? 'mb-0' : ''}`}>
          <div className="absolute -left-[2.15rem] top-1.5 h-3 w-3 rounded-full border-2 border-sky-500 bg-white dark:bg-slate-900" />
          <div className="mb-1">
            <h3 className="text-lg font-semibold text-slate-900 dark:text-white">{exp.position}</h3>
            <div className="flex flex-wrap items-center gap-x-3 gap-y-1 text-sm text-slate-600 dark:text-slate-400">
              <span className="font-medium text-sky-600 dark:text-sky-400">{exp.company}</span>
              <span className="text-slate-400 dark:text-slate-500">|</span>
              <span>
                {formatDate(exp.startDate)} &mdash; {exp.isCurrent ? 'Present' : formatDate(exp.endDate!)}
              </span>
            </div>
          </div>
          {exp.description && (
            <p className="mb-3 text-sm leading-relaxed text-slate-600 dark:text-slate-400">{exp.description}</p>
          )}
          <div className="flex flex-wrap gap-1.5">
            {exp.technologies.map((tech) => (
              <Badge key={tech} variant="default">{tech}</Badge>
            ))}
          </div>
        </div>
      ))}
    </div>
  );
}
