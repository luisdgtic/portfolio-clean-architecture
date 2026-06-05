import type { HTMLAttributes, ReactNode } from 'react';
import { cn } from '@/lib/utils';

interface CardProps extends HTMLAttributes<HTMLDivElement> {
  children: ReactNode;
}

export function Card({ className, children, ...props }: CardProps) {
  return (
    <div
      className={cn(
        'rounded-xl border border-slate-200 bg-white p-6 shadow-sm transition-shadow hover:shadow-md',
        'dark:border-slate-700 dark:bg-slate-800',
        className
      )}
      {...props}
    >
      {children}
    </div>
  );
}

export function CardHeader({ className, children, ...props }: CardProps) {
  return <div className={cn('mb-4', className)} {...props}>{children}</div>;
}

export function CardContent({ className, children, ...props }: CardProps) {
  return <div className={cn('', className)} {...props}>{children}</div>;
}

export function CardFooter({ className, children, ...props }: CardProps) {
  return <div className={cn('mt-4 flex items-center gap-3 border-t border-slate-100 pt-4 dark:border-slate-700', className)} {...props}>{children}</div>;
}
