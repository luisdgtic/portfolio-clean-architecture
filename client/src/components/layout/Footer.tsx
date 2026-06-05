export function Footer() {
  const year = new Date().getFullYear();

  return (
    <footer className="border-t border-slate-200 bg-white py-8 dark:border-slate-800 dark:bg-slate-900">
      <div className="mx-auto max-w-6xl px-4 text-center">
        <p className="text-sm text-slate-500 dark:text-slate-400">
          &copy; {year} Portfolio. Built with .NET Core + React.
        </p>
      </div>
    </footer>
  );
}
