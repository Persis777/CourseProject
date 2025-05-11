import React from "react";
import { ITask } from "@/models/Task";
import api from "@/api";
import TaskPlan from "@/app/task/[id]/TaskPlan";

interface Props {
  task: ITask;
}

export default function TaskForm({ task }: Props) {
  const { id, title, description, priority, status, deadline } = task;

  const handleRequest = async (FormData: FormData) => {
    "use server";

    try {
      const payload = {
        id: FormData.get("id") ? Number(FormData.get("id")) : undefined,
        title: FormData.get("title") as string,
        description: FormData.get("description") as string,
        priority: FormData.get("priority") as "High" | "Medium" | "Low",
        status: FormData.get("status") as "Todo" | "In Progress" | "Done",
        deadline: FormData.get("deadline") as string,
      };

      await api.task.update(payload);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <form
      action={handleRequest}
      className="m-4 bg-gray-800 p-6 rounded-lg shadow-lg border border-gray-700 releative"
    >
      <div className="flex gap-6">
        <div className="space-y-6 w-1/3">
          <div className="flex flex-col">
            <label
              htmlFor="title"
              className="text-gray-300 mb-2 font-semibold text-lg"
            >
              Title
            </label>
            <input
              defaultValue={title}
              placeholder="Enter task title..."
              name="title"
              required
              className="bg-gray-700 border border-gray-600 text-gray-200 p-3 rounded-lg focus:ring-2 focus:ring-blue-500"
            />
          </div>

          <div className="grid grid-cols-2 gap-6">
            <div className="flex flex-col">
              <label
                htmlFor="priority"
                className="text-gray-300 mb-2 font-semibold text-lg"
              >
                Priority
              </label>
              <select
                name="priority"
                defaultValue={priority}
                required
                className="bg-gray-700 border border-gray-600 text-gray-200 p-3 rounded-lg focus:ring-2 focus:ring-blue-500"
              >
                <option value="High">High</option>
                <option value="Medium">Medium</option>
                <option value="Low">Low</option>
              </select>
            </div>

            <div className="flex flex-col ">
              <label
                htmlFor="deadline"
                className="text-gray-300 mb-2 font-semibold text-lg"
              >
                End Date
              </label>

              <input
                type="datetime-local"
                defaultValue={deadline}
                required
                name="deadline"
                className="bg-gray-700 border border-gray-600 text-gray-200 p-2 rounded-lg focus:ring-2 focus:ring-blue-500"
              />
            </div>
          </div>

          <div className="flex flex-col">
            <label
              htmlFor="status"
              className="text-gray-300 mb-2 font-semibold text-lg"
            >
              Status
            </label>
            <select
              name="status"
              defaultValue={status}
              required
              className="bg-gray-700 border border-gray-600 text-gray-200 p-3 rounded-lg focus:ring-2 focus:ring-blue-500"
            >
              <option value="Todo">Todo</option>
              <option value="In Progress">In Progress</option>
              <option value="Done">Done</option>
            </select>
          </div>

          <div className="flex flex-col">
            <label
              htmlFor="description"
              className="text-gray-300 mb-2 font-semibold text-lg"
            >
              Description
            </label>
            <textarea
              name="description"
              defaultValue={description}
              className="bg-gray-700 border border-gray-600 text-gray-200 p-3 rounded-lg focus:ring-2 focus:ring-blue-500 h-24 resize-none"
              placeholder="Write a detailed description of the task"
            />
          </div>
        </div>

        <TaskPlan />
      </div>

      <input
        name="id"
        defaultValue={id}
        className="absolute inset-0 w-0 h-0 opacity-0"
      />

      <div className="absolute inset-0 w-0 h-0 opacity-0">
        <button type="submit">Save</button>
      </div>
    </form>
  );
}
