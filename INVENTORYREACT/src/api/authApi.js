import axiosClient from "./axiosClient";

export const loginApi = (loginData) => {
  return axiosClient.post("/Auth/login", loginData);
};

export const refreshTokenApi = (refreshToken) => {
  return axiosClient.post("/Auth/refresh-token", {
    refreshToken,
  });
};

export const registerApi = (registerData) => {
  return axiosClient.post(
    "/Auth/register",
    registerData
  );
   
  
};

export const verifyEditPasswordApi = (password) => {
  return axiosClient.post(
    "/Auth/verify-edit-password",
    {
      password,
    }
  );
};