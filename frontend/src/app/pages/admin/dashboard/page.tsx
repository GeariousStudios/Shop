"use client";

import { useAuth } from "@/app/context/useAuth";
import { useRouter } from "next/navigation";
import React, { useEffect } from "react";
import styles from "./page.module.css";
import "@/app/styles/globals.css";
import Navbar from "@/app/components/admin/navbar";

const Dashboard = () => {
  const { user, isLoggedIn } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (!isLoggedIn) {
      router.push("/pages/admin/login");
    }
  }, [isLoggedIn, router]);

  if (!user) {
    return <p>Redirecting to login...</p>;
  }

  return (
    <>
      <Navbar />
      <div className={styles.page}>
        <p>Content</p>
      </div>
    </>
  );
};

export default Dashboard;
