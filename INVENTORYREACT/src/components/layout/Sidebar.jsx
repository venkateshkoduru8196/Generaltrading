// import { useState } from "react";
// import Icon from "./Icon";
// import icons from "./icons";

// import { useAuth } from "../../context/AuthContext";
// import useMenus from "../../hooks/useMenus";

// export default function Sidebar({
//   open,
//   onClose,
//   active,
//   setActive,
// }) {
//   const [expanded, setExpanded] = useState({});

//   const { user } = useAuth();

//   const menus = useMenus();

//   const toggle = (menuId) => {
//     setExpanded((prev) => ({
//       ...prev,
//       [menuId]: !prev[menuId],
//     }));
//   };

//   const getIcon = (menuName) => {
//     switch (menuName.toLowerCase()) {
//       case "dashboard":
//         return icons.dashboard;

//       case "master":
//         return icons.master;

//       case "purchase":
//         return icons.purchase;

//       case "sales":
//         return icons.sales;

//       case "inventory":
//         return icons.inventory;

//       case "accounts":
//         return icons.accounts;

//       case "gst reports":
//         return icons.reports;

//       case "reports":
//         return icons.reports;

//       case "crm":
//         return icons.crm;

//       case "user management":
//         return icons.users;

//       case "settings":
//         return icons.settings;

//       default:
//         return icons.dashboard;
//     }
//   };

//   return (
//     <>
//       <div
//         className={`sidebar-overlay ${
//           open ? "visible" : ""
//         }`}
//         onClick={onClose}
//       />

//       <aside
//         className={`sidebar ${
//           open ? "open" : ""
//         }`}
//       >
//         <div className="sidebar-brand">
//           <div className="brand-icon">
//             GST
//           </div>

//           <div className="brand-text">
//             <span className="brand-title">
//               ADVANCED GST
//             </span>

//             <span className="brand-sub">
//               Billing & Inventory
//             </span>
//           </div>

//           <button
//             className="sidebar-close-btn"
//             onClick={onClose}
//           >
//             <Icon
//               d={icons.close}
//               size={18}
//             />
//           </button>
//         </div>

//         <nav className="sidebar-nav">
//           {menus.map((menu) => (
//             <div
//               key={menu.menuId}
//               className="nav-group"
//             >
//               <button
//                 className={`nav-item ${
//                   active === menu.menuId
//                     ? "nav-active"
//                     : ""
//                 }`}
//                 onClick={() => {
//                   setActive(menu.menuId);

//                   if (
//                     menu.children?.length > 0
//                   ) {
//                     toggle(menu.menuId);
//                   }
//                 }}
//               >
//                 <span className="nav-icon">
//                   <Icon
//                     d={getIcon(menu.menuName)}
//                     size={17}
//                   />
//                 </span>

//                 <span className="nav-label">
//                   {menu.menuName}
//                 </span>

//                 {menu.children?.length >
//                   0 && (
//                   <span
//                     className={`nav-chevron ${
//                       expanded[
//                         menu.menuId
//                       ]
//                         ? "rotated"
//                         : ""
//                     }`}
//                   >
//                     <Icon
//                       d={icons.chevron}
//                       size={14}
//                     />
//                   </span>
//                 )}
//               </button>

//               {menu.children?.length >
//                 0 &&
//                 expanded[
//                   menu.menuId
//                 ] && (
//                   <div className="nav-sub">
//                     {menu.children.map(
//                       (child) => (
//                         <button
//                           key={
//                             child.menuId
//                           }
//                           className="nav-sub-item"
//                         >
//                           {child.menuName}
//                         </button>
//                       )
//                     )}
//                   </div>
//                 )}
//             </div>
//           ))}
//         </nav>

//         <div className="sidebar-footer">
//           <div className="user-chip">
//             <div className="user-avatar">
//               {user?.fullName
//                 ?.charAt(0)
//                 ?.toUpperCase() || "U"}
//             </div>

//             <div>
//               <div className="user-name">
//                 {user?.fullName}
//               </div>

//               <div className="user-role">
//                 {user?.roleName}
//               </div>
//             </div>
//           </div>
//         </div>
//       </aside>
//     </>
//   );
// }



import { useState } from "react";
import { useNavigate } from "react-router-dom";

import Icon from "./Icon";
import icons from "./icons";

import { useAuth } from "../../context/AuthContext";
import useMenus from "../../hooks/useMenus";

export default function Sidebar({
  open,
  onClose,
  active,
  setActive,
}) {
  const [expanded, setExpanded] = useState({});

  const navigate = useNavigate();

  const { user } = useAuth();

  const menus = useMenus();

  const toggle = (menuId) => {
    setExpanded((prev) => ({
      ...prev,
      [menuId]: !prev[menuId],
    }));
  };

  const getIcon = (menuName) => {
    switch (menuName.toLowerCase()) {
      case "dashboard":
        return icons.dashboard;

      case "master":
        return icons.master;

      case "purchase":
        return icons.purchase;

      case "sales":
        return icons.sales;

      case "inventory":
        return icons.inventory;

      case "accounts":
        return icons.accounts;

      case "gst reports":
        return icons.reports;

      case "reports":
        return icons.reports;

      case "crm":
        return icons.crm;

      case "user management":
        return icons.users;

      case "settings":
        return icons.settings;

      default:
        return icons.dashboard;
    }
  };

  const handleNavigation = (menu) => {
    setActive(menu.menuId);

    if (menu.children?.length > 0) {
      toggle(menu.menuId);
      return;
    }

    if (menu.menuUrl) {
      navigate(menu.menuUrl);
      onClose();
    }
  };

  const handleChildNavigation = (child) => {
    navigate(child.menuUrl);
    onClose();
  };

  return (
    <>
      <div
        className={`sidebar-overlay ${
          open ? "visible" : ""
        }`}
        onClick={onClose}
      />

      <aside
        className={`sidebar ${
          open ? "open" : ""
        }`}
      >
        <div className="sidebar-brand">
          <div className="brand-icon">
            GST
          </div>

          <div className="brand-text">
            <span className="brand-title">
              ADVANCED GST
            </span>

            <span className="brand-sub">
              Billing & Inventory
            </span>
          </div>

          <button
            className="sidebar-close-btn"
            onClick={onClose}
          >
            <Icon
              d={icons.close}
              size={18}
            />
          </button>
        </div>

        <nav className="sidebar-nav">

          {menus.map((menu) => (

            <div
              key={menu.menuId}
              className="nav-group"
            >

              <button
                className={`nav-item ${
                  active === menu.menuId
                    ? "nav-active"
                    : ""
                }`}
                onClick={() =>
                  handleNavigation(menu)
                }
              >

                <span className="nav-icon">
                  <Icon
                    d={getIcon(menu.menuName)}
                    size={17}
                  />
                </span>

                <span className="nav-label">
                  {menu.menuName}
                </span>

                {menu.children?.length > 0 && (
                  <span
                    className={`nav-chevron ${
                      expanded[menu.menuId]
                        ? "rotated"
                        : ""
                    }`}
                  >
                    <Icon
                      d={icons.chevron}
                      size={14}
                    />
                  </span>
                )}

              </button>

              {menu.children?.length > 0 &&
                expanded[menu.menuId] && (

                  <div className="nav-sub">

                    {menu.children.map(
                      (child) => (

                        <button
                          key={child.menuId}
                          className="nav-sub-item"
                          onClick={() =>
                            handleChildNavigation(
                              child
                            )
                          }
                        >
                          {child.menuName}
                        </button>

                      )
                    )}

                  </div>

                )}

            </div>

          ))}

        </nav>

        <div className="sidebar-footer">

          <div className="user-chip">

            <div className="user-avatar">
              {user?.fullName
                ?.charAt(0)
                ?.toUpperCase() || "U"}
            </div>

            <div>

              <div className="user-name">
                {user?.fullName}
              </div>

              <div className="user-role">
                {user?.roleName}
              </div>

            </div>

          </div>

        </div>

      </aside>
    </>
  );
}