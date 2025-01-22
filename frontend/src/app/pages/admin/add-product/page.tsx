"use client";

import { useAuth } from "@/app/context/useAuth";
import { useRouter } from "next/navigation";
import React, { useEffect, useState } from "react";
import styles from "./page.module.css";
import "../../../globals.css";
import Navbar from "@/app/components/admin/navbar";

const AddProduct = () => {
  const { user, isLoggedIn } = useAuth();
  const router = useRouter();

  const [formData, setFormData] = useState({
    title: "",
    description: "",
    price: "",
    category: "",
  });

  useEffect(() => {
    if (!isLoggedIn) {
      router.push("/pages/admin/login");
    }
  }, [isLoggedIn, router]);

  if (!user) {
    return <p>Redirecting to login...</p>;
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Add logic to submit the product
    console.log("Product submitted:", formData);
  };

  return (
    <>
      <Navbar />
      <div className={styles.page}>
        <div className={styles.productForm}>
          <h1>Lägg till ny produkt</h1>
          <form onSubmit={handleSubmit} className={styles.form}>
            <div className={styles.field}>
              <label htmlFor="title">Produktnamn</label>
              <input type="text" id="title" name="title" value={formData.title} onChange={handleInputChange} required />
            </div>
            <div className={styles.field}>
              <label htmlFor="description">Produktbeskrivning</label>
              <textarea
                id="description"
                name="description"
                value={formData.description}
                onChange={handleInputChange}
                required
              ></textarea>
            </div>
            <div className={styles.field}>
              <label htmlFor="price">Pris</label>
              <input
                type="number"
                id="price"
                name="price"
                value={formData.price}
                onChange={handleInputChange}
                required
              />
            </div>
            <div className={styles.field}>
              <label htmlFor="category">Kategorier</label>
              <input
                type="text"
                id="category"
                name="category"
                value={formData.category}
                onChange={handleInputChange}
                required
              />
            </div>
            <button type="submit" className={styles.submitButton}>
              Add Product
            </button>
          </form>
        </div>
      </div>
    </>
  );
};

export default AddProduct;
