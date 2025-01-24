import React, { useRef, useState } from "react";
import styles from "./imageUploadBox.module.css";
import { Camera } from "lucide-react";

type ImageUploadBoxProps = {
  onUpload: (file: File) => void;
  image?: string;
};

const ImageUploadBox: React.FC<ImageUploadBoxProps> = ({ onUpload, image }) => {
  const fileInputRef = useRef<HTMLInputElement | null>(null);

  const handleClick = () => {
    fileInputRef.current?.click();
  };

  const handleUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files[0]) {
      onUpload(event.target.files[0]);
    }
  };

  return (
    <div className={styles.imageUploadBox} onClick={handleClick}>
      {image ? (
        <img src={image} alt="Uploaded preview" className={styles.uploadedImage} />
      ) : (
        <>
          <input type="file" accept="image/*" ref={fileInputRef} style={{ display: "none" }} onChange={handleUpload} />
          <span>
            <Camera className={styles.pictureIcon} />
            Välj bilder
          </span>
        </>
      )}
    </div>
  );
};

export default ImageUploadBox;
