import React from "react";

interface PriorityLevelsProps {
  // eslint-disable-next-line no-unused-vars
  onClick: (value: "High" | "Medium" | "Low") => void;
  value: "High" | "Medium" | "Low";
}

export default function PriorityLevels({
  onClick,
  value,
}: PriorityLevelsProps) {
  const priorities = [
    {
      level: "High",
      color: "bg-red-500",
    },
    {
      level: "Medium",
      color: "bg-yellow-500",
    },
    {
      level: "Low",
      color: "bg-green-500",
    },
  ];

  return (
    <div className="flex flex-col">
      <div className="flex flex-col justify-between">
        {priorities.map((priority) => (
          <button
            key={priority.level}
            className={`flex flex-row items-center cursor-pointer ${value === priority.level ? "opacity-100" : "opacity-50"}`}
            onClick={() => onClick(priority.level as "High" | "Medium" | "Low")}
          >
            <div
              className={`${priority.color} w-4 h-4 rounded-full ${value === priority.level ? "ring-2 ring-offset-2 ring-black" : ""}`}
            />
            <p className="ml-2">{priority.level}</p>
          </button>
        ))}
      </div>
    </div>
  );
}
