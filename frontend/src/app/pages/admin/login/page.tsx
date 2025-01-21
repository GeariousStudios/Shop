"use client";

import React from "react";
import * as Yup from "yup";
import { useAuth } from "../../../context/useAuth";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import "./page.css";
import { useRouter } from "next/navigation";

interface Props {}

type LoginFormsInputs = {
  email: string;
  password: string;
};

const validation = Yup.object().shape({
  email: Yup.string().email().required("Username is required"),
  password: Yup.string().required("Password is required"),
});

const Login = (props: Props) => {
  const { loginUser } = useAuth();
  const router = useRouter();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormsInputs>({ resolver: yupResolver(validation) });

  const handleLogin = async (form: LoginFormsInputs) => {
    const isAuthenticated = await loginUser(form.email, form.password);
    if (isAuthenticated) {
      router.push("dashboard");
    } else {
      console.error("Login failed. Please check your credentials.");
    }
  };
  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleSubmit(handleLogin)}>
        <div className="form-group">
          <label>Email</label>
          <input type="email" {...register("email")} />
          {errors.email ? <p>{errors.email.message}</p> : ""}
        </div>
        <div className="form-group">
          <label>Password</label>
          <input type="password" {...register("password")} />
          {errors.password ? <p>{errors.password.message}</p> : ""}
        </div>
        <button className="submit-button" type="submit">
          Login
        </button>
      </form>
    </div>
  );
};

export default Login;
