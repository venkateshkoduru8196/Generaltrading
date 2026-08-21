import axiosClient from "./axiosClient";

export const createEmployee = (data) => {
  return axiosClient.post(
    "/Auth/create-employee",
    data
  );
};