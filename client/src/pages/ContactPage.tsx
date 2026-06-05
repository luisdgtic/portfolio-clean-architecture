import { ContactForm } from '@/components/ContactForm';

export default function ContactPage() {
  return (
    <div className="mx-auto max-w-2xl px-4 py-12">
      <h1 className="mb-2 text-3xl font-bold text-slate-900 dark:text-white">Contact</h1>
      <p className="mb-8 text-slate-600 dark:text-slate-400">
        Have a question or want to work together? Send me a message.
      </p>

      <div className="rounded-xl border border-slate-200 bg-white p-6 dark:border-slate-700 dark:bg-slate-800">
        <ContactForm />
      </div>
    </div>
  );
}
