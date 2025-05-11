import React from "react";

export default function Footer() {
  return (
    <footer className="bg-gray-800 text-white text-center p-2 mt-auto">
      <div className="container mx-auto px-4">
        <p className="text-sm ">
          &copy; 2024{" "}
          <a
            href="https://t.me/iktnb"
            className="hover:underline text-blue-400"
          >
            @iktnb
          </a>
          &
          <a
            href="https://t.me/Persis777"
            className="hover:underline text-blue-400"
          >
            @Persis777
          </a>
        </p>
      </div>
    </footer>
  );
}
