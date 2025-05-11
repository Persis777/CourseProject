import React from "react";

interface Props {
  children: React.ReactNode;
}

export default function LiCard({ children }: Props) {
  return (
    <div className="p-1 border bg-gray-600 border-gray-500 shadow-2xl w-fit">
      {children}
    </div>
  );
}
