import axios from "@/utils/axios";
import { ITask } from "@/models/Task";
import { AxiosResponse } from "axios";

export const getAll = async (): Promise<AxiosResponse<ITask[]>> =>
  axios.get("/task");

export const getById = async (
  id: string | number,
): Promise<AxiosResponse<ITask>> => axios.get(`/task/${id}`);

export const create = async (
  payload: Partial<ITask>,
): Promise<AxiosResponse<ITask>> => axios.post("/task", payload);

export const update = async (
  payload: Partial<ITask>,
): Promise<AxiosResponse<ITask>> => axios.put(`/task/${payload.id}`, payload);

export const deleteById = async (id: number): Promise<AxiosResponse<void>> =>
  axios.delete(`/task/${id}`);
