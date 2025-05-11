"use client";

import { ITask } from "@/models/Task";
import { GoPencil, GoTrash } from "react-icons/go";
import api from "../../api";

export default function ActionButtons({ task }: { task: ITask }) {
  const deleteTask = async () => {
    await api.task.deleteById(task.id!);
  };

  return (
    <div className="flex gap-2 justify-end">
      <GoPencil className="text-gray-400 cursor-pointer" />
      <GoTrash className="text-gray-400 cursor-pointer" onClick={deleteTask} />
    </div>
  );
}
