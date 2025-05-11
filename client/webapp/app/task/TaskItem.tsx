import Link from "next/link";
import { formatDate } from "@/app/task/utils/dateFormat";
import { ITask } from "@/models/Task";

interface Props {
  task: ITask;
}

function TaskItem({ task }: Props) {
  const getDaysUntilDeadline = (deadline) => {
    const today = new Date();
    const deadlineDate = new Date(deadline);
    const diffTime = deadlineDate - today;
    return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
  };

  const daysUntilDeadline = getDaysUntilDeadline(task.deadline!);
  const isUrgent = daysUntilDeadline < 1;

  return (
    <Link
      href={`/task/${task.id}`}
      className="w-full text-blue-400 font-semibold hover:underline truncate block"
    >
      <li className="bg-gray-700 text-sm shadow-md p-4 rounded-lg grid grid-cols-12 gap-4 items-center transition-transform duration-200 hover:bg-gray-600">
        <span className="col-span-3 text-gray-300 truncate flex gap-4 items-center">
          <span className="font-bold">{task.id}</span>
          {task.title}
        </span>

        <span className="col-span-4 text-gray-300 truncate">
          {task.description || "No description"}
        </span>

        <span
          className={`col-span-2 text-xs text-center font-bold px-2 py-1 rounded-lg text-white ${task.priority === "High" ? "bg-red-500" : task.priority === "Medium" ? "bg-yellow-500" : "bg-green-500"}`}
        >
          {task.priority}
        </span>

        <span
          className={`col-span-2 text-xs text-center font-semibold px-2 py-1 rounded-lg ${task.status === "Done" ? "bg-green-600 text-white" : task.status === "In Progress" ? "bg-yellow-600 text-white" : "bg-gray-600 text-gray-300"}`}
        >
          {task.status}
        </span>

        <span
          className={`col-span-1 text-center text-xs font-medium px-2 py-1 rounded-lg ${isUrgent ? "bg-red-500" : "bg-yellow-600"}`}
        >
          {formatDate(task.deadline!)}
        </span>
      </li>
    </Link>
  );
}

export default TaskItem;
