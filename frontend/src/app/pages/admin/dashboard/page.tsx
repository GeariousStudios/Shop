"use client";

import { useAuth } from "@/app/context/useAuth";
import { useRouter } from "next/navigation";
import React, { useEffect } from "react";

interface Props {}

const Dashboard = (props: Props) => {
  const { user, isLoggedIn, logoutUser } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (!isLoggedIn) {
      router.push("/admin/login");
    }
  }, [isLoggedIn, router]);

  if (!user) {
    return <div>Not allowed</div>;
  }

  const handleLogout = () => {
    logoutUser();
    router.push("/pages/admin/login");
  };

  return (
    <div>
      <h1>Dashboard</h1>
      <p>Welcome, {user.email}!</p>
      <button onClick={handleLogout}>Logout</button>
    </div>
  );
};

export default Dashboard;
