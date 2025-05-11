export default function Sidenav() {
  return (
    <div className="w-64 pt-16 fixed h-screen text-white flex flex-col border-gray-600 border bg-gray-900">
      <div className="p-4 text-lg font-semibold">Menu</div>
      <div className="flex-1 overflow-y-auto">
        <ul>
          <li className="hover:bg-gray-700 p-4 cursor-pointer">Dashboard</li>
          <li className="hover:bg-gray-700 p-4 cursor-pointer">Tasks</li>
          <li className="hover:bg-gray-700 p-4 cursor-pointer">Settings</li>
        </ul>
      </div>
      <div className="border-t border-gray-600">
        <ul>
          <li className="hover:bg-gray-700 cursor-pointer p-4">Logout</li>
        </ul>
      </div>
    </div>
  );
}
