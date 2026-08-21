import {
  createContext,
  useContext,
  useState,
} from "react";

import * as authService from "../services/authService";

const AuthContext = createContext();

export function AuthProvider({
  children,
}) {
  const [user, setUser] =
    useState(
      authService.getCurrentUser()
    );

  // ===========================
  // Login
  // ===========================
  const loginUser = async (
    loginData
  ) => {
    const response =
      await authService.login(
        loginData
      );

    setUser(
      authService.getCurrentUser()
    );

    return response;
  };

  // ===========================
  // Customer Registration
  // ===========================
  const registerUser =
    async (registerData) => {

      const response =
        await authService.register(
          registerData
        );

      return response;
    };

  // ===========================
  // Logout
  // ===========================
  const logoutUser = () => {
    authService.logout();
    setUser(null);
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        loginUser,
        registerUser,
        logoutUser,
        isAuthenticated:
          authService.isAuthenticated(),
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export const useAuth = () =>
  useContext(AuthContext);