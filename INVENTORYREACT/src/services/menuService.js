import axiosClient from "../api/axiosClient";

export const getMenusByRole = async (roleId) => {
  const response = await axiosClient.get(
    `/Menu/tree/${roleId}`
  );

  return response.data;
};