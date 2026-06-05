import { useState } from 'react';
import { Button } from '@/components/ui/Button';
import { Input } from '@/components/ui/Input';
import { Textarea } from '@/components/ui/Textarea';
import { useSendContactMessage } from '@/api/hooks';

export function ContactForm() {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [subject, setSubject] = useState('');
  const [body, setBody] = useState('');
  const mutation = useSendContactMessage();

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    mutation.mutate({ name, email, subject, body });
  };

  if (mutation.isSuccess) {
    return (
      <div className="rounded-xl border border-emerald-200 bg-emerald-50 p-8 text-center dark:border-emerald-800 dark:bg-emerald-950">
        <h3 className="mb-2 text-xl font-semibold text-emerald-700 dark:text-emerald-300">
          Message Sent!
        </h3>
        <p className="text-emerald-600 dark:text-emerald-400">
          Thank you for reaching out. I will get back to you soon.
        </p>
      </div>
    );
  }

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <Input label="Name" value={name} onChange={(e) => setName(e.target.value)} placeholder="Your name" required />
      <Input label="Email" type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="your@email.com" required />
      <Input label="Subject" value={subject} onChange={(e) => setSubject(e.target.value)} placeholder="What's this about?" required />
      <Textarea label="Message" value={body} onChange={(e) => setBody(e.target.value)} placeholder="Your message..." rows={5} required />
      <Button type="submit" disabled={mutation.isPending} className="w-full">
        {mutation.isPending ? 'Sending...' : 'Send Message'}
      </Button>
      {mutation.isError && (
        <p className="text-sm text-red-500">Failed to send message. Please try again.</p>
      )}
    </form>
  );
}
