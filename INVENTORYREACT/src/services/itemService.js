import axiosClient from "../api/axiosClient";

const ENDPOINT = "/Item";

export const getItems = async () => {
  const response = await axiosClient.get(ENDPOINT);
  return response.data;
};

export const getItemById = async (id) => {
  const response = await axiosClient.get(`${ENDPOINT}/${id}`);
  return response.data;
};

export const createItem = async (data) => {
  const response = await axiosClient.post(ENDPOINT, data);
  return response.data;
};

export const updateItem = async (id, data) => {
  const response = await axiosClient.put(`${ENDPOINT}/${id}`, data);
  return response.data;
};

export const deleteItem = async (id) => {
  await axiosClient.delete(`${ENDPOINT}/${id}`);
};