import { useState } from 'react';
import { Link } from 'react-router-dom';
import { useBlogPosts } from '@/api/hooks';
import { Badge } from '@/components/ui/Badge';
import { Button } from '@/components/ui/Button';

export default function BlogPage() {
  const [page, setPage] = useState(1);
  const { data, isLoading } = useBlogPosts(page, 6);

  if (isLoading) {
    return <div className="flex min-h-[60vh] items-center justify-center"><div className="h-8 w-8 animate-spin rounded-full border-4 border-sky-500 border-t-transparent" /></div>;
  }

  const totalPages = data ? Math.ceil(data.totalCount / data.pageSize) : 0;

  return (
    <div className="mx-auto max-w-4xl px-4 py-12">
      <h1 className="mb-2 text-3xl font-bold text-slate-900 dark:text-white">Blog</h1>
      <p className="mb-8 text-slate-600 dark:text-slate-400">
        Thoughts, tutorials, and insights on software development.
      </p>

      {data && data.posts.length > 0 ? (
        <>
          <div className="space-y-6">
            {data.posts.map((post) => (
              <Link
                key={post.id}
                to={`/blog/${post.slug}`}
                className="block rounded-xl border border-slate-200 bg-white p-6 transition-shadow hover:shadow-md dark:border-slate-700 dark:bg-slate-800"
              >
                <h2 className="mb-2 text-xl font-semibold text-slate-900 dark:text-white">{post.title}</h2>
                <p className="mb-3 text-sm text-slate-600 dark:text-slate-400">{post.summary}</p>
                <div className="flex flex-wrap items-center gap-3">
                  <div className="flex gap-1.5">
                    {post.tags.map((tag) => (
                      <Badge key={tag} variant="primary">{tag}</Badge>
                    ))}
                  </div>
                  <span className="ml-auto text-xs text-slate-400 dark:text-slate-500">
                    {new Date(post.publishedAt).toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' })} &middot; {post.readTimeMinutes} min read
                  </span>
                </div>
              </Link>
            ))}
          </div>

          {totalPages > 1 && (
            <div className="mt-8 flex items-center justify-center gap-3">
              <Button variant="outline" disabled={page <= 1} onClick={() => setPage((p) => p - 1)}>Previous</Button>
              <span className="text-sm text-slate-600 dark:text-slate-400">Page {page} of {totalPages}</span>
              <Button variant="outline" disabled={page >= totalPages} onClick={() => setPage((p) => p + 1)}>Next</Button>
            </div>
          )}
        </>
      ) : (
        <p className="text-center text-slate-500 dark:text-slate-400">No blog posts yet.</p>
      )}
    </div>
  );
}
