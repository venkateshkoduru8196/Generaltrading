import { useState } from "react";

import Sidebar from "../components/layout/Sidebar";
import Header from "../components/layout/Header";
import Footer from "../components/layout/Footer";

export default function MainLayout({
  children,
}) {
  const [sidebarOpen,
    setSidebarOpen] =
    useState(false);

  const [activeNav,
    setActiveNav] =
    useState("dashboard");

  return (
    <div className="app-shell">

      <Sidebar
        open={sidebarOpen}
        onClose={() =>
          setSidebarOpen(false)
        }
        active={activeNav}
        setActive={
          setActiveNav
        }
      />

      <div className="main-area">

        <Header
          onMenuClick={() =>
            setSidebarOpen(
              (p) => !p
            )
          }
        />

        <main
          className="content"
        >
          {children}
        </main>

        <Footer />

      </div>

    </div>
  );
}