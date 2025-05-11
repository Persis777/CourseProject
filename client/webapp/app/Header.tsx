import Link from "next/link"; // Убедитесь, что импортируете Link из next/link

export default function Header() {
  return (
    <header className="fixed h-16 top-0 w-full flex items-center justify-between p-4 border-b border-gray-600 bg-gray-900 shadow-md">
      <Link href="/">
        <h1 className="text-xl font-bold text-white">Limb</h1>
      </Link>

      <div className="flex flex-1 justify-center mx-4">
        <input
          type="text"
          placeholder="Search..."
          className="w-full max-w-xs px-3 py-1 text-gray-900 bg-gray-700 rounded-lg border border-gray-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>

      <div className="flex items-center gap-4">
        <Link href="/login">
          <button className="text-gray-300 hover:text-white transition-colors duration-300">
            Login
          </button>
        </Link>
        <Link href="/settings">
          <button className="text-gray-300 hover:text-white transition-colors duration-300">
            Settings
          </button>
        </Link>
      </div>
    </header>
  );
}
