import type { Skill } from '@/api/types';

const categoryColors: Record<string, string> = {
  Backend: 'bg-emerald-100 text-emerald-700 dark:bg-emerald-900 dark:text-emerald-300',
  Frontend: 'bg-blue-100 text-blue-700 dark:bg-blue-900 dark:text-blue-300',
  DevOps: 'bg-purple-100 text-purple-700 dark:bg-purple-900 dark:text-purple-300',
  Database: 'bg-amber-100 text-amber-700 dark:bg-amber-900 dark:text-amber-300',
  Cloud: 'bg-cyan-100 text-cyan-700 dark:bg-cyan-900 dark:text-cyan-300',
  Tools: 'bg-rose-100 text-rose-700 dark:bg-rose-900 dark:text-rose-300',
  Other: 'bg-slate-100 text-slate-700 dark:bg-slate-700 dark:text-slate-300',
};

interface SkillBadgeProps {
  skill: Skill;
  showProficiency?: boolean;
}

export function SkillBadge({ skill, showProficiency = true }: SkillBadgeProps) {
  const colorClass = categoryColors[skill.category] || categoryColors.Other;

  return (
    <div className="flex flex-col items-center gap-2 rounded-xl border border-slate-200 bg-white p-4 shadow-sm dark:border-slate-700 dark:bg-slate-800">
      <span className={`rounded-full px-3 py-1 text-xs font-medium ${colorClass}`}>
        {skill.category}
      </span>
      <span className="text-sm font-semibold text-slate-900 dark:text-white">{skill.name}</span>
      {showProficiency && (
        <div className="flex w-full items-center gap-2">
          <div className="h-1.5 flex-1 overflow-hidden rounded-full bg-slate-200 dark:bg-slate-700">
            <div
              className="h-full rounded-full bg-sky-500 transition-all duration-500"
              style={{ width: `${skill.proficiency}%` }}
            />
          </div>
          <span className="text-xs text-slate-500 dark:text-slate-400">{skill.proficiency}%</span>
        </div>
      )}
    </div>
  );
}
