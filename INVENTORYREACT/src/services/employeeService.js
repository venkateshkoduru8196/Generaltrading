import { createEmployee as createEmployeeApi }
from "../api/employeeApi";

export const createEmployee = async (data) => {

  const response =
    await createEmployeeApi(data);

  return response.data;

};