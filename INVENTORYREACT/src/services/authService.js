import { jwtDecode } from "jwt-decode";
import {
  loginApi,
  registerApi,
  verifyEditPasswordApi
} from "../api/authApi";
import axiosClient from "../api/axiosClient";


const TOKEN_KEY = "token";
const REFRESH_TOKEN_KEY = "refreshToken";
const USER_KEY = "user";

// ===============================
// Login
// ===============================
export const login = async (loginData) => {
  const response = await loginApi(loginData);

  const data = response.data;

  localStorage.setItem(
    TOKEN_KEY,
    data.accessToken
  );

  localStorage.setItem(
    REFRESH_TOKEN_KEY,
    data.refreshToken
  );

  localStorage.setItem(
    USER_KEY,
    JSON.stringify({
      userId: data.userId,
      fullName: data.fullName,
      roleId: data.roleId,
      roleName: data.roleName,
    })
  );

  return data;
};

// ===============================
// Logout
// ===============================
export const logout = () => {
  localStorage.removeItem(TOKEN_KEY);
  localStorage.removeItem(
    REFRESH_TOKEN_KEY
  );
  localStorage.removeItem(USER_KEY);
};

// ===============================
// Get Token
// ===============================
export const getToken = () => {
  return localStorage.getItem(
    TOKEN_KEY
  );
};

// ===============================
// Get Refresh Token
// ===============================
export const getRefreshToken =
  () => {
    return localStorage.getItem(
      REFRESH_TOKEN_KEY
    );
  };

// ===============================
// Get Logged User
// ===============================
export const getCurrentUser =
  () => {
    const user =
      localStorage.getItem(USER_KEY);

    if (!user) return null;

    return JSON.parse(user);
  };

// ===============================
// Token Expired?
// ===============================
export const isTokenExpired =
  () => {
    const token = getToken();

    if (!token) return true;

    try {
      const decoded =
        jwtDecode(token);

      const currentTime =
        Date.now() / 1000;

      return (
        decoded.exp < currentTime
      );
    } catch {
      return true;
    }
  };

// ===============================
// Authenticated?
// ===============================
export const isAuthenticated =
  () => {
    const token = getToken();

    if (!token) return false;

    if (isTokenExpired()) {
      logout();
      return false;
    }

    return true;
  };

  // ===============================
// Customer Registration
// ===============================
export const register = async (
  registerData
) => {

  const response =
    await registerApi(registerData);

  return response.data;

};

export const verifyEditPassword = async (password) => {
    const response = await axiosClient.post(
        "/Auth/verify-edit-password",
        { password }
    );

    return response.data;
};
