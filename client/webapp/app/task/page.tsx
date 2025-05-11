import React from "react";
import TaskItem from "@/app/task/TaskItem";
import { GoPaste } from "react-icons/go";
import api from "@/api";
import { createTask } from "@/app/task/utils/createTask";

export default async function Page() {
  const { data: tasks } = await api.task.getAll();
  const newId = tasks.length + 1;

  const handlerCreateTask = async () => {
    "use server";

    await createTask(newId);
  };

  return (
    <div className="flex flex-grow mt-auto h-full">
      <div className="w-full bg-gray-800 flex-shrink-0 h-full rounded-lg pt-20 px-2 shadow-lg">
        <form>
          <div className="mb-4">
            <input
              name="text"
              type="text"
              placeholder="Filter by name or description"
              className="w-full p-2 bg-gray-700 text-gray-200 rounded-lg border border-gray-600 focus:ring-2 focus:ring-blue-500"
            />
          </div>

          <div className="mb-4">
            <select
              name="priority"
              className="w-full p-2 bg-gray-700 text-gray-200 rounded-lg border border-gray-600 focus:ring-2 focus:ring-blue-500"
            >
              <option value="priority">Sort by Priority</option>
              <option value="deadline">Sort by End Date</option>
            </select>
          </div>
        </form>
        <ul className="space-y-2 h-full overflow-auto">
          <form action={handlerCreateTask}>
            <button
              type="submit"
              className="bg-blue-600 hover:bg-blue-700 text-white text-sm shadow-md py-2 flex items-center gap-4 justify-center w-full rounded-lg transition-transform duration-200"
            >
              <GoPaste /> <span>New</span>
            </button>
          </form>

          {tasks.reverse().map((task) => (
            <TaskItem task={task} key={task.id} />
          ))}
        </ul>
      </div>
    </div>
  );
}
