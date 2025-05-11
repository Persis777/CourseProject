export interface IStep {
  id: number;
  planId: number;
  stepNumber: number;
  plan: string;
}

export interface IPlan {
  id: number;
  title: string;
  creationDate: string;
  userTaskId: number;
  userTask: string;
  steps: IStep[];
}

export type TTaskStatus = "InProgress" | "Completed" | "Pending";

export interface ITask {
  id: number;
  title: string;
  description: string;
  deadline: string;
  priority: "High" | "Medium" | "Low";
  status: TTaskStatus;
  plan?: IPlan;
}
