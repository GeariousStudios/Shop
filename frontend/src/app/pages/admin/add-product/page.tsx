"use client";

import { useAuth } from "@/app/context/useAuth";
import { useRouter } from "next/navigation";
import React, { ChangeEvent, FormEvent, useEffect, useRef, useState } from "react";
import styles from "./page.module.css";
import "@/app/styles/globals.css";
import "@/app/styles/fonts.css";
import "@/app/styles/quill.css";
import "@/app/styles/admin/buttons.css";
import Navbar from "@/app/components/admin/navbar";
import Quill from "quill";
import ImageUploadBox from "@/app/components/admin/imageUploadBox";
import Variant from "@/app/components/admin/variant";

type Product = {
  title: string;
  description: string;
  price: string;
  category: string;
  variants: string;
};

const AddProduct = () => {
  // Product related code.
  const [formData, setFormData] = useState<Product>({
    title: "",
    description: "",
    price: "",
    category: "",
    variants: "",
  });

  // Image upload code.
  type ImageWithAlt = {
    url: string;
    alt: string;
  };

  const [images, setImages] = useState<ImageWithAlt[]>([]);

  const handleAddImage = (file: File) => {
    const imageUrl = URL.createObjectURL(file);
    setImages((prev) => [...prev, { url: imageUrl, alt: "" }]);
  };

  const handleRemoveImage = (index: number) => {
    setImages((prev) => prev.filter((_, i) => i !== index));
  };

  const handleAltTextChange = (index: number, altText: string) => {
    setImages((prev) => prev.map((image, i) => (i === index ? { ...image, alt: altText } : image)));
  };

  // Input related code.
  const [isVariantsEnabled, setIsVariantsEnabled] = useState(false);
  const [variants, setVariants] = useState<{ title: string; options: string[] }[]>([]);
  const containerRef = useRef<HTMLDivElement | null>(null);

  const handleInputChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value, type } = e.target;
    const isChecked = (e.target as HTMLInputElement).checked;

    if (name === "variants" && type === "checkbox") {
      setIsVariantsEnabled(isChecked);
      if (isChecked) {
        setVariants([{ title: "", options: [] }]);
      } else {
        setVariants([]);
      }
      return;
    }
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

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();

    const productData = {
      ...formData,
      variants,
    };
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
      <Navbar>
        <div className={styles.page}>
          {/* Add product form */}
          <div className={styles.productForm}>
            <h3>Lägg till produkt</h3>
            <form onSubmit={handleSubmit} className={styles.form}>
              <div className={styles.titleAndPrice}>
                <div className={styles.field}>
                  <label>Rubrik</label>
                  <input
                    type="text"
                    id="title"
                    name="title"
                    value={formData.title}
                    onChange={handleInputChange}
                    required
                  />
                  <h5>Namnge produkt</h5>
                </div>

                {!isVariantsEnabled && (
                  <div className={styles.field}>
                    <label>Pris</label>
                    <div className={styles.price}>
                      <input
                        type="number"
                        id="price"
                        name="price"
                        value={formData.price}
                        onChange={(e) => {
                          let value = e.target.value;

                          if (value.startsWith("0") && value.length > 1) {
                            value = value.replace(/^0+/, "");
                          }

                          value = value === "" || parseFloat(value) > 0 ? value : "1";

                          setFormData((prevData) => ({
                            ...prevData,
                            price: value,
                          }));
                        }}
                        onKeyDown={(e) => {
                          // Prevent invalid key inputs
                          if (e.key === "-" || e.key === "e" || e.key === "+" || e.key === "." || e.key === ",") {
                            e.preventDefault();
                          }
                        }}
                        min="0" // Prevent the stepper from going below 0.
                        required
                      />
                      <h6>SEK</h6>
                    </div>
                    <h5>Ange pris</h5>
                  </div>
                )}
              </div>

              {/* Picture section. */}
              <div className={styles.field}>
                <label className={styles.notRequired}>Bilder</label>
                <div className={styles.imageContainer}>
                  {images.map((image, index) => (
                    <ImageUploadBox
                      key={index}
                      image={image.url}
                      onUpload={() => {}}
                      onRemove={() => handleRemoveImage(index)}
                      onAltTextChange={(altText) => handleAltTextChange(index, altText)}
                    />
                  ))}
                  <ImageUploadBox onUpload={handleAddImage} onRemove={() => {}} onAltTextChange={() => {}} />
                </div>
              </div>

              {/* Description section. */}
              <div className={styles.field}>
                <label className={styles.notRequired}>Beskrivning</label>
                <div ref={quillRef} id="description"></div>
                <h5>Beskriv produkten</h5>
              </div>

              {/* Category section. */}
              <div className={styles.field}>
                <label>Kategorier</label>
                <input
                  type="text"
                  id="category"
                  name="category"
                  value={formData.category}
                  onChange={handleInputChange}
                  required
                />
                <h5>Ange i vilka kategorier som produkten kan hittas</h5>
              </div>

              {/* Product variants. */}
              <div className={styles.field}>
                <label className={styles.notRequired}>Produktvarianter</label>
                <div className={styles.checkbox}>
                  <input
                    type="checkbox"
                    id="variants"
                    name="variants"
                    checked={isVariantsEnabled}
                    value={formData.variants}
                    onChange={handleInputChange}
                  />
                  <h6>Lägg till</h6>
                </div>
                <h5>Aktivera om det finns olika varianter av produkten</h5>
              </div>

              <div ref={containerRef}>
                {isVariantsEnabled && <Variant variants={variants} setVariants={setVariants} />}
              </div>
            </form>
          </div>
        </div>
      </Navbar>
    </>
  );
};

export default AddProduct;
