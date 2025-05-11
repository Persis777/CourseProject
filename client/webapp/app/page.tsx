import React from "react";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Hub - Limb",
  description:
    "A digital space for managing tasks, finances, library, and more. Enhance your productivity with a comprehensive suite of intuitive tools.",
};

export default function Home() {
  const services = [
    {
      title: "Tasks",
      description: "Manage your tasks efficiently.",
      href: "/task",
      bgColor: "bg-blue-500",
      textColor: "text-white",
    },
    {
      title: "Spends",
      description: "Track and manage your expenses.",
      href: "/spends",
      bgColor: "bg-green-500",
      textColor: "text-white",
    },
    {
      title: "Library",
      description: "Access and organize your library.",
      href: "/library",
      bgColor: "bg-purple-500",
      textColor: "text-white",
    },
    {
      title: "Goals",
      description: "Set and track your long-term goals.",
      href: "/goals",
      bgColor: "bg-yellow-500",
      textColor: "text-white",
    },
    {
      title: "Habits",
      description: "Build and maintain good habits.",
      href: "/habits",
      bgColor: "bg-teal-500",
      textColor: "text-white",
    },
    {
      title: "Notes",
      description: "Keep your notes and ideas organized.",
      href: "/notes",
      bgColor: "bg-orange-500",
      textColor: "text-white",
    },
    {
      title: "Calendar",
      description: "Plan and visualize your schedule.",
      href: "/calendar",
      bgColor: "bg-red-500",
      textColor: "text-white",
    },
    {
      title: "Contacts",
      description: "Manage your personal and professional contacts.",
      href: "/contacts",
      bgColor: "bg-indigo-500",
      textColor: "text-white",
    },
    {
      title: "Projects",
      description: "Organize your ongoing projects.",
      href: "/projects",
      bgColor: "bg-pink-500",
      textColor: "text-white",
    },
  ];

  return (
    <div className="flex flex-col items-center justify-center p-4">
      <h1 className="text-4xl font-bold text-gray-300 mb-4">Welcome</h1>
      <p className="text-gray-400 mb-8 text-center">
        This is a simple service manager where you can navigate to various
        service.
      </p>
      <div className="flex flex-wrap justify-center gap-6">
        {services.map((service) => (
          <a
            key={service.title}
            href={service.href}
            className={`w-64 p-6 rounded-lg shadow-lg ${service.bgColor} ${service.textColor} text-center transition-transform transform hover:scale-105`}
          >
            <h2 className="text-2xl font-semibold mb-2">{service.title}</h2>
            <p className="text-lg">{service.description}</p>
          </a>
        ))}
      </div>
    </div>
  );
}
