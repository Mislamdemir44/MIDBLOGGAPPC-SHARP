import { Link } from 'react-router-dom';
import { Menu, Home, BookOpen, FolderOpen, LogIn, UserPlus } from 'lucide-react';
import { useState } from 'react';

export default function Navbar() {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <nav className="bg-white shadow-lg">
      <div className="container mx-auto px-4">
        <div className="flex justify-between items-center h-16">
          <Link to="/" className="flex items-center space-x-2">
            <BookOpen className="h-8 w-8 text-blue-600" />
            <span className="text-xl font-bold text-gray-900">M_İ_D Blog</span>
          </Link>

          {/* Desktop Menu */}
          <div className="hidden md:flex items-center space-x-4">
            <Link to="/" className="flex items-center space-x-1 text-gray-700 hover:text-blue-600">
              <Home className="h-5 w-5" />
              <span>Home</span>
            </Link>
            <Link to="/posts" className="flex items-center space-x-1 text-gray-700 hover:text-blue-600">
              <BookOpen className="h-5 w-5" />
              <span>Posts</span>
            </Link>
            <Link to="/categories" className="flex items-center space-x-1 text-gray-700 hover:text-blue-600">
              <FolderOpen className="h-5 w-5" />
              <span>Categories</span>
            </Link>
            <Link to="/login" className="flex items-center space-x-1 text-gray-700 hover:text-blue-600">
              <LogIn className="h-5 w-5" />
              <span>Login</span>
            </Link>
            <Link to="/register" className="flex items-center space-x-1 text-white bg-blue-600 hover:bg-blue-700 px-4 py-2 rounded-md">
              <UserPlus className="h-5 w-5" />
              <span>Register</span>
            </Link>
          </div>

          {/* Mobile Menu Button */}
          <div className="md:hidden">
            <button
              onClick={() => setIsOpen(!isOpen)}
              className="text-gray-700 hover:text-blue-600"
            >
              <Menu className="h-6 w-6" />
            </button>
          </div>
        </div>

        {/* Mobile Menu */}
        {isOpen && (
          <div className="md:hidden pb-4">
            <Link to="/" className="block py-2 text-gray-700 hover:text-blue-600">Home</Link>
            <Link to="/posts" className="block py-2 text-gray-700 hover:text-blue-600">Posts</Link>
            <Link to="/categories" className="block py-2 text-gray-700 hover:text-blue-600">Categories</Link>
            <Link to="/login" className="block py-2 text-gray-700 hover:text-blue-600">Login</Link>
            <Link to="/register" className="block py-2 text-gray-700 hover:text-blue-600">Register</Link>
          </div>
        )}
      </div>
    </nav>
  );
}