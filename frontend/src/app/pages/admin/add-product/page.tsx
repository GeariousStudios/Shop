"use client";

import { useAuth } from "@/app/context/useAuth";
import { useRouter } from "next/navigation";
import React, { useEffect, useRef, useState } from "react";
import styles from "./page.module.css";
import "@/app/styles/globals.css";
import "@/app/styles/fonts.css";
import "@/app/styles/quill.css";
import "@/app/styles/admin/buttons.css";
import Navbar from "@/app/components/admin/navbar";
import Quill from "quill";

type Product = {
  title: string;
  description: string;
  price: string;
  category: string;
};

const AddProduct = () => {
  // Product related code.
  const [productList, setProductList] = useState<Product[]>([]); // State to store added products.
  const [formData, setFormData] = useState<Product>({
    title: "",
    description: "",
    price: "",
    category: "",
  });

  // Input related code.
  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    if (name === "price") {
      const parsedValue = value === "" ? "" : Math.max(0, parseFloat(value)).toString();
      setFormData((prevData) => ({
        ...prevData,
        [name]: parsedValue,
      }));
    } else {
      setFormData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setProductList((prevList) => [...prevList, formData]);
    console.log("Product added:", formData);
  };

  // Quill-related code.
  const quillRef = useRef<HTMLDivElement | null>(null);
  const quillInstance = useRef<Quill | null>(null); // Store the Quill instance.

  useEffect(() => {
    if (!quillRef.current || quillInstance.current) {
      return;
    }

    const FontAttributor = Quill.import("attributors/class/font") as any;
    FontAttributor.whitelist = [
      "lato",
      "arial",
      "comic-sans",
      "courier",
      "georgia",
      "helvetica",
      "roboto",
      "times-new-roman",
      "trebuchet",
      "verdana",
    ];
    Quill.register(FontAttributor as any, true);

    quillInstance.current = new Quill(quillRef.current, {
      theme: "snow",
      modules: {
        toolbar: [
          {
            font: FontAttributor.whitelist,
          },
          { header: [1, 2, 3, 4, 5, 6, false] },
          "bold",
          "italic",
          "underline",
          "strike",
          { color: [] },
          { background: [] },
          { align: [] },
          { indent: "-1" },
          { indent: "+1" },
          { list: "ordered" },
          { list: "bullet" },
          "link",
        ],
      },
    });

    quillInstance.current.on("text-change", () => {
      setFormData((prev) => ({
        ...prev,
        description: quillInstance.current!.root.innerHTML,
      }));
    });
  }, []);

  // User related code (needs to be before return statement).
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
        {/* Add product form */}
        <div className={styles.productForm}>
          <h1>Lägg till ny produkt</h1>
          <form onSubmit={handleSubmit} className={styles.form}>
            <div className={styles.field}>
              <label htmlFor="title">Rubrik</label>
              <input type="text" id="title" name="title" value={formData.title} onChange={handleInputChange} required />
            </div>
            <div className={styles.field}>
              <label htmlFor="description" className={styles.notRequired}>
                Beskrivning
              </label>
              <div ref={quillRef} id="description"></div>
            </div>
            <div className={styles.field}>
              <label htmlFor="price">Pris</label>
              <input
                type="number"
                id="price"
                name="price"
                value={formData.price}
                onChange={(e) => {
                  const value = e.target.value;
                  setFormData((prevData) => ({
                    ...prevData,
                    price: value === "" || parseFloat(value) >= 0 ? value : "0", // Ensure positive value.
                  }));
                }}
                onKeyDown={(e) => {
                  if (e.key === "-" || e.key === "e" || e.key === "+") {
                    e.preventDefault();
                  }
                }}
                min="0" // Prevent the stepper from going below 0.
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
            <button type="submit" className="addProductButton">
              Lägg till
            </button>
            <button type="submit" className="previewProductButton">
              Förhandsgranska
            </button>
          </form>
        </div>

        {/* Product list */}
        <div className={styles.productList}>
          <h1>Produkter</h1>
          <ul>
            {productList.map((product, index) => (
              <li key={index} className={styles.productItem}>
                <strong>{product.title}</strong>
                <button type="submit" className="editButton">
                  <img src="/edit.svg" alt="Edit" />
                </button>
              </li>
            ))}
          </ul>
          <button type="submit" className="publishAllButton">
            Publicera alla
          </button>
          <button type="submit" className="publishSelectedButton">
            Publicera valda
          </button>
        </div>
      </div>
    </>
  );
};

export default AddProduct;
