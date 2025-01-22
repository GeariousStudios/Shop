"use client";

import { useAuth } from "@/app/context/useAuth";
import { useRouter } from "next/navigation";
import React, { useEffect, useState } from "react";
import styles from "./page.module.css";
import "../../../globals.css";

const Dashboard = () => {
  const { user, isLoggedIn, logoutUser } = useAuth();
  const router = useRouter();

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
    router.push("/pages/admin/login");
  };

  return (
    <div className={styles.page}>
      <nav className={styles.navbar}>
        <div className={styles.navbarBrand}>Admin Dashboard</div>
        <ul className={styles.navbarLinks}>
          {/* Produkter */}
          <li className={styles.dropdown}>
            <button className={styles.dropdownButton}>Produkter</button>
            <ul className={styles.dropdownMenu}>
              <li className={styles.hoverable} onClick={() => router.push("/pages/admin/add-product")}>
                Lägg till ny produkt
              </li>
              <li className={styles.hoverable} onClick={() => router.push("/pages/admin/view-products")}>
                Visa/redigera produkter
              </li>
            </ul>
          </li>
          {/* Kategorier */}
          <li className={styles.dropdown}>
            <button className={styles.dropdownButton}>Kategorier</button>
            <ul className={styles.dropdownMenu}>
              <li className={styles.hoverable} onClick={() => router.push("/pages/admin/add-product")}>
                Lägg till ny kategori
              </li>
              <li className={styles.hoverable} onClick={() => router.push("/pages/admin/view-products")}>
                Visa/redigera kategorier
              </li>
            </ul>
          </li>
          {/* Bildbibliotek */}
          <li className={styles.noDropdown} onClick={() => router.push("/pages/admin/media")}>
            Bildbibliotek
          </li>
          {/* Användare */}
          <li className={styles.dropdown}>
            <button className={styles.dropdownButton}>{user.email}</button>
            <ul className={styles.dropdownMenu}>
              <li className={styles.hoverable} onClick={() => router.push("/pages/admin/settings")}>
                Inställningar
              </li>
              <li className={styles.centered}>
                <button className={styles.logoutButton} onClick={handleLogout}>
                  Logga ut
                </button>
              </li>
            </ul>
          </li>
        </ul>
      </nav>
      <main className={styles.dashboardMain}></main>
    </div>
  );
};

export default Dashboard;
