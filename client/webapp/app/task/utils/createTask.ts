"use server";

import api from "@/api";
import { ITask } from "@/models/Task";
import { formatDateTimeForInput } from "@/app/task/utils/dateFormat";
import { revalidateTag } from "next/cache";
import { redirect } from "next/navigation";

export const createTask = async () => {
  let newId;

  try {
    const task = {
      title: "New Task",
      description: "",
      priority: "High",
      status: "Todo",
      deadline: formatDateTimeForInput(new Date()),
    } as ITask;
    const { data } = await api.task.create(task);
    newId = data.id;
  } catch (error) {
    console.error(error);
  }

  revalidateTag("task");
  redirect(`/task/${newId}`);
};
