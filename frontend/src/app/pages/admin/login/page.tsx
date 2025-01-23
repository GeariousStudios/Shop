"use client";

import React, { useState } from "react";
import * as Yup from "yup";
import { useAuth } from "../../../context/useAuth";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import styles from "./page.module.css";
import "@/app/styles/globals.css";
import "@/app/styles/buttons.css";
import { useRouter } from "next/navigation";

interface Props {}

type LoginFormsInputs = {
  email: string;
  password: string;
};

const validation = Yup.object().shape({
  email: Yup.string().email(" ").required(" "),
  password: Yup.string().required(" "),
});

const Login = (props: Props) => {
  const { loginUser } = useAuth();
  const router = useRouter();
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

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
      setErrorMessage("Fel mejladress eller lösenord. Var god försök igen.");
    }
  };
  return (
    <div className={styles.page}>
      <form className={styles.loginForm} onSubmit={handleSubmit(handleLogin)}>
        <div className={styles.formGroup}>
          <label>Mejladress</label>
          <input type="email" {...register("email")} />
          {errors.email ? <p>{errors.email.message}</p> : ""}
        </div>
        <div className={styles.formGroup}>
          <label>Lösenord</label>
          <input type="password" {...register("password")} />
          {errors.password ? <p>{errors.password.message}</p> : ""}
        </div>
        {errorMessage && <p className={styles.errorMessage}>{errorMessage}</p>} {/* Display login error */}
        <button className="loginButton" type="submit">
          Logga in
        </button>
      </form>
    </div>
  );
};

export default Login;
