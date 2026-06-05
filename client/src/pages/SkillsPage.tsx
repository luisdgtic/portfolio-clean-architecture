import { useSkills } from '@/api/hooks';
import type { Skill } from '@/api/types';
import { SkillBadge } from '@/components/SkillBadge';

const categoryOrder = ['Backend', 'Frontend', 'DevOps', 'Database', 'Cloud', 'Tools', 'Other'];

function groupByCategory(skills: Skill[]) {
  return categoryOrder
    .map((cat) => ({
      category: cat,
      skills: skills.filter((s) => s.category === cat),
    }))
    .filter((g) => g.skills.length > 0);
}

export default function SkillsPage() {
  const { data: skills, isLoading } = useSkills();

  if (isLoading) {
    return <div className="flex min-h-[60vh] items-center justify-center"><div className="h-8 w-8 animate-spin rounded-full border-4 border-sky-500 border-t-transparent" /></div>;
  }

  const grouped = skills ? groupByCategory(skills) : [];

  return (
    <div className="mx-auto max-w-6xl px-4 py-12">
      <h1 className="mb-2 text-3xl font-bold text-slate-900 dark:text-white">Skills & Technologies</h1>
      <p className="mb-8 text-slate-600 dark:text-slate-400">
        Technologies and tools I work with on a daily basis.
      </p>

      {grouped.map(({ category, skills: catSkills }) => (
        <section key={category} className="mb-10">
          <h2 className="mb-4 text-xl font-semibold text-slate-900 dark:text-white">{category}</h2>
          <div className="grid gap-3 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4">
            {catSkills.map((skill) => (
              <SkillBadge key={skill.id} skill={skill} />
            ))}
          </div>
        </section>
      ))}
    </div>
  );
}
