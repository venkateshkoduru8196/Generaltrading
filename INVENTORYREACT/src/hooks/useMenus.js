import {
  useEffect,
  useState,
} from "react";

import {
  getMenusByRole,
} from "../services/menuService";

import {
  getCurrentUser,
} from "../services/authService";

export default function useMenus() {

  const [menus, setMenus] =
    useState([]);

  const user =
    getCurrentUser();

  useEffect(() => {

    if (!user?.roleId)
      return;

    loadMenus();

  }, []);

  const loadMenus =
    async () => {

      try {

        const data =
          await getMenusByRole(
            user.roleId
          );

        setMenus(data);

      }
      catch (error) {

        console.error(error);

      }

    };

  return menus;
}