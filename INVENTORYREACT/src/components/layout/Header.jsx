// import Icon from "./Icon";
// import icons from "./icons";

// export default function Header({
//   onMenuClick,
// }) {
//   const today =
//     new Date().toLocaleDateString(
//       "en-IN"
//     );

//   return (
//     <header className="topbar">
//       <div className="topbar-left">
//         <button
//           className="menu-btn"
//           onClick={onMenuClick}
//         >
//           <Icon
//             d={icons.menu}
//             size={22}
//           />
//         </button>
//       </div>

//       <div className="topbar-right">
//         <div className="topbar-date">
//           {today}
//         </div>

//         <button className="topbar-btn">
//           <Icon
//             d={icons.bell}
//             size={18}
//           />
//         </button>

//         <div className="topbar-user">
//           <div className="topbar-avatar">
//             A
//           </div>

//           <span className="topbar-username">
//             admin
//           </span>
//         </div>
//       </div>
//     </header>
//   );
// }

import "./Header.css";

import { useNavigate } from "react-router-dom";

import Icon from "./Icon";
import icons from "./icons";

import { useAuth } from "../../context/AuthContext";

export default function Header({
  onMenuClick,
}) {
  const navigate = useNavigate();

  const {
    user,
    logoutUser,
  } = useAuth();

  const today =
    new Date().toLocaleDateString("en-IN");

  const handleLogout = () => {
    logoutUser();

    navigate("/login", {
      replace: true,
    });
  };

  return (
    <header className="topbar">

      <div className="topbar-left">
        <button
          className="menu-btn"
          onClick={onMenuClick}
        >
          <Icon
            d={icons.menu}
            size={22}
          />
        </button>
      </div>

      <div className="topbar-right">

        <div className="topbar-date">
          {today}
        </div>

        <button className="topbar-btn">
          <Icon
            d={icons.bell}
            size={18}
          />
        </button>

        <div className="topbar-user">

          <div className="topbar-avatar">
            {user?.fullName?.charAt(0).toUpperCase()}
          </div>

          <span className="topbar-username">
            {user?.fullName}
          </span>

        </div>

        <button
          className="logout-btn"
          onClick={handleLogout}
        >
          Logout
        </button>

      </div>

    </header>
  );
}