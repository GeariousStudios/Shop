"use client";

import { createContext, useContext, useEffect, useState } from "react";
import axios from "axios";
import { UserProfile } from "../models/user";
import { toast } from "react-toastify";
import React from "react";
import { loginAPI, registerAPI } from "../services/authService";

type UserContextType = {
  user: UserProfile | null;
  token: string | null;
  registerUser: (email: string, password: string) => Promise<boolean>;
  loginUser: (email: string, password: string) => Promise<boolean>;
  logoutUser: () => void;
  isLoggedIn: () => boolean;
};

type Props = { children: React.ReactNode };

const UserContext = createContext<UserContextType>({} as UserContextType);

export const UserProvider = ({ children }: Props) => {
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<UserProfile | null>(null);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    const user = localStorage.getItem("user");
    const token = localStorage.getItem("token");

    if (user && token) {
      setUser(JSON.parse(user));
      setToken(token);
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    }
    setIsReady(true);
  }, []);

  const registerUser = async (email: string, password: string): Promise<boolean> => {
    try {
      const res = await registerAPI(email, password); // Await the API call...
      if (res && res.data) {
        // If the API response contains the expected data.
        localStorage.setItem("token", res.data.token);
        const userObj = { email: res.data.email };
        localStorage.setItem("user", JSON.stringify(userObj));
        setToken(res.data.token);
        setUser(userObj);
        toast.success("Registered successfully!");
        return true; // Return true to indicate success!
      }
      return false; // Return false if response is not as expected.
    } catch (error) {
      console.error("Register error:", error);
      return false; // Return false to indicate failure.
    }
  };

  const loginUser = async (email: string, password: string): Promise<boolean> => {
    try {
      const res = await loginAPI(email, password); // Await the API call...
      if (res && res.data) {
        // If the API response contains the expected data.
        localStorage.setItem("token", res.data.token);
        const userObj = { email: res.data.email };
        localStorage.setItem("user", JSON.stringify(userObj));
        setToken(res.data.token);
        setUser(userObj);
        toast.success("Login success!");
        return true; // Return true to indicate success!
      }
      return false; // Return false if response is not as expected.
    } catch (error) {
      console.error("Login error:", error);
      toast.error("Invalid credentials or account does not exist.");
      return false; // Return false to indicate failure.
    }
  };

  const isLoggedIn = () => {
    return !!user;
  };

  const logoutUser = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
    setToken("");
  };

  return (
    <UserContext.Provider value={{ loginUser, user, token, logoutUser, isLoggedIn, registerUser }}>
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(UserContext);
  if (!context) {
    throw new Error("useAuth must be used within a UserProvider");
  }
  return context;
};
