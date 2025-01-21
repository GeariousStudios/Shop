// page.tsx
"use client";

import { useAuth } from "@/app/context/useAuth";
import { useRouter } from "next/navigation";
import React, { useEffect, useState } from "react";
import "./page.css";

const Dashboard = () => {
  const { user, isLoggedIn, logoutUser } = useAuth();
  const router = useRouter();

  const [dropdownOpen, setDropdownOpen] = useState(false);

  useEffect(() => {
    if (!isLoggedIn) {
      goToLogin();
    }
  }, [isLoggedIn, router]);

  if (!user) {
    return <div>Not allowed</div>;
  }

  const handleLogout = () => {
    logoutUser();
    goToLogin();
  };

  const goToLogin = () => {
    router.push("/admin/login");
  };

  const toggleDropdown = () => {
    setDropdownOpen(!dropdownOpen);
  };

  return (
    <div className="dashboard-container">
      <nav className="navbar">
        <div className="navbar-brand">Admin Dashboard</div>
        <ul className="navbar-links">
          <li className="dropdown">
            <button className="dropdown-button">Produkter ▼</button>
            <ul className="dropdown-menu">
              <li className="hoverable" onClick={() => router.push("/admin/add-product")}>
                Lägg till ny produkt
              </li>
              <li className="hoverable" onClick={() => router.push("/admin/view-products")}>
                Visa/redigera befintliga produkter
              </li>
            </ul>
          </li>
          <li className="dropdown">
            <button className="dropdown-button">{user.email} ▼</button>
            <ul className="dropdown-menu">
              <li className="hoverable" onClick={() => router.push("/admin/settings")}>
                Inställningar
              </li>
              <li>
                <button className="logout-button" onClick={handleLogout}>
                  Logga ut
                </button>
              </li>
            </ul>
          </li>
        </ul>
      </nav>
      <main className="dashboard-main"></main>
    </div>
  );
};

export default Dashboard;
