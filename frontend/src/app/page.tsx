"use client";

import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { UserProvider } from "./context/useAuth";
import Link from "next/link";

export default function Home() {
  return (
    <UserProvider>
      <ToastContainer />
      <div>
        <h1>Welcome to the Home Page</h1>
        <nav>
          <ul>
            <li>
              <Link href="pages/admin/login">Go to Login</Link>
            </li>
            <li>
              <Link href="pages/admin/dashboard">Go to Dashboard</Link>
            </li>
          </ul>
        </nav>
      </div>
    </UserProvider>
  );
}
