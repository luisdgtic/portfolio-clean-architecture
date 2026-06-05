import { useParams, Link } from 'react-router-dom';
import { useBlogPost } from '@/api/hooks';
import { Badge } from '@/components/ui/Badge';
import { Button } from '@/components/ui/Button';

export default function BlogPostPage() {
  const { slug } = useParams<{ slug: string }>();
  const { data: post, isLoading } = useBlogPost(slug!);

  if (isLoading) {
    return <div className="flex min-h-[60vh] items-center justify-center"><div className="h-8 w-8 animate-spin rounded-full border-4 border-sky-500 border-t-transparent" /></div>;
  }

  if (!post) {
    return (
      <div className="mx-auto max-w-3xl px-4 py-12 text-center">
        <h1 className="mb-4 text-2xl font-bold text-slate-900 dark:text-white">Post Not Found</h1>
        <Link to="/blog"><Button variant="ghost">Back to Blog</Button></Link>
      </div>
    );
  }

  return (
    <div className="mx-auto max-w-3xl px-4 py-12">
      <Link to="/blog" className="mb-6 inline-block text-sm text-sky-500 hover:text-sky-600">&larr; Back to Blog</Link>

      <h1 className="mb-3 text-3xl font-bold text-slate-900 dark:text-white">{post.title}</h1>

      <div className="mb-6 flex flex-wrap items-center gap-3">
        <div className="flex gap-1.5">
          {post.tags.map((tag) => (
            <Badge key={tag} variant="primary">{tag}</Badge>
          ))}
        </div>
        <span className="text-sm text-slate-400 dark:text-slate-500">
          {new Date(post.publishedAt).toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' })} &middot; {post.readTimeMinutes} min read
        </span>
      </div>

      <div className="prose prose-slate max-w-none dark:prose-invert">
        <p>{post.content}</p>
      </div>
    </div>
  );
}
