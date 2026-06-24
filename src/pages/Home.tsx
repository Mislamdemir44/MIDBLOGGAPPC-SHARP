import { BookOpen } from 'lucide-react';

export default function Home() {
  return (
    <div className="max-w-4xl mx-auto text-center">
      <div className="flex justify-center">
        <BookOpen className="h-16 w-16 text-blue-600" />
      </div>
      <h1 className="mt-6 text-4xl font-bold text-gray-900">Welcome to M_İ_D Blog</h1>
      <p className="mt-4 text-xl text-gray-600">
        Share your thoughts, connect with others, and explore amazing content.
      </p>
      <div className="mt-8 grid gap-4 md:grid-cols-2 lg:grid-cols-3">
        {/* Featured posts will go here */}
      </div>
    </div>
  );
}