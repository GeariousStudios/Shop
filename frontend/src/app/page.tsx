"use client";

import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { UserProvider } from "./context/useAuth";
import { useRouter } from "next/navigation";
import { useEffect } from "react";
import "./styles/globals.css";

export default function Home() {
  const router = useRouter();

  useEffect(() => {
    router.push("pages/admin/login");
  }, [router]);

  return (
    <UserProvider>
      <ToastContainer />
    </UserProvider>
  );
}
