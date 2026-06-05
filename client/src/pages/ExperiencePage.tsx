import { useExperiences, useEducation, useCertifications } from '@/api/hooks';
import { Timeline } from '@/components/Timeline';

function formatDate(dateStr: string): string {
  return new Date(dateStr).toLocaleDateString('en-US', { year: 'numeric', month: 'short' });
}

export default function ExperiencePage() {
  const { data: experiences, isLoading: expLoading } = useExperiences();
  const { data: education, isLoading: eduLoading } = useEducation();
  const { data: certifications, isLoading: certLoading } = useCertifications();

  const isLoading = expLoading || eduLoading || certLoading;

  if (isLoading) {
    return <div className="flex min-h-[60vh] items-center justify-center"><div className="h-8 w-8 animate-spin rounded-full border-4 border-sky-500 border-t-transparent" /></div>;
  }

  return (
    <div className="mx-auto max-w-4xl px-4 py-12">
      <h1 className="mb-2 text-3xl font-bold text-slate-900 dark:text-white">Experience</h1>
      <p className="mb-10 text-slate-600 dark:text-slate-400">
        My professional journey and education.
      </p>

      {experiences && experiences.length > 0 && (
        <section className="mb-12">
          <h2 className="mb-6 text-2xl font-semibold text-slate-900 dark:text-white">Work</h2>
          <Timeline experiences={experiences} />
        </section>
      )}

      {education && education.length > 0 && (
        <section className="mb-12">
          <h2 className="mb-6 text-2xl font-semibold text-slate-900 dark:text-white">Education</h2>
          <div className="space-y-4">
            {education.map((edu) => (
              <div key={edu.id} className="rounded-xl border border-slate-200 bg-white p-5 dark:border-slate-700 dark:bg-slate-800">
                <h3 className="font-semibold text-slate-900 dark:text-white">{edu.degree} in {edu.fieldOfStudy}</h3>
                <p className="text-sm text-sky-600 dark:text-sky-400">{edu.institution}</p>
                <p className="text-sm text-slate-500 dark:text-slate-400">
                  {formatDate(edu.startDate)} &mdash; {edu.isCurrent ? 'Present' : formatDate(edu.endDate!)}
                </p>
                {edu.gpa && <p className="text-sm text-slate-500">GPA: {edu.gpa}</p>}
                {edu.description && <p className="mt-2 text-sm text-slate-600 dark:text-slate-400">{edu.description}</p>}
              </div>
            ))}
          </div>
        </section>
      )}

      {certifications && certifications.length > 0 && (
        <section>
          <h2 className="mb-6 text-2xl font-semibold text-slate-900 dark:text-white">Certifications</h2>
          <div className="grid gap-4 sm:grid-cols-2">
            {certifications.map((cert) => (
              <div key={cert.id} className="rounded-xl border border-slate-200 bg-white p-5 dark:border-slate-700 dark:bg-slate-800">
                {cert.url ? (
                  <a href={cert.url} target="_blank" rel="noopener noreferrer" className="font-semibold text-slate-900 hover:text-sky-500 dark:text-white dark:hover:text-sky-400">
                    {cert.name} &rarr;
                  </a>
                ) : (
                  <h3 className="font-semibold text-slate-900 dark:text-white">{cert.name}</h3>
                )}
                <p className="text-sm text-slate-500 dark:text-slate-400">{cert.issuer}</p>
                <p className="text-sm text-slate-500">{formatDate(cert.issueDate)}</p>
              </div>
            ))}
          </div>
        </section>
      )}
    </div>
  );
}
