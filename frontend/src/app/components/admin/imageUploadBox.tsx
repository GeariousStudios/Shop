import React, { ChangeEvent, FC, useRef, useState } from "react";
import styles from "./imageUploadBox.module.css";
import { Camera } from "lucide-react";

type ImageUploadBoxProps = {
  onUpload: (file: File) => void;
  onRemove: () => void;
  onAltTextChange: (altText: string) => void;
  image?: string;
};

const ImageUploadBox: FC<ImageUploadBoxProps> = ({ onUpload, onRemove, onAltTextChange, image }) => {
  const [altText, setAltText] = useState<string>("");
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const fileInputRef = useRef<HTMLInputElement | null>(null);

  const handleClick = () => {
    fileInputRef.current?.click();
  };

  const handleUpload = (event: ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files[0]) {
      onUpload(event.target.files[0]);
    }
  };

  const handleOpenModal = () => setIsModalOpen(true);
  const handleCloseModal = () => setIsModalOpen(false);
  const handleAltTextSubmit = () => {
    onAltTextChange(altText);
    handleCloseModal();
  };

  return (
    <div className={styles.imageUploadBox}>
      {image ? (
        <div className={styles.imageContainer}>
          <img src={image} alt={altText || "Uploaded preview"} className={styles.uploadedImage} />
          <div className={styles.imageActions}>
            <button className={styles.altTextButton} onClick={handleOpenModal}>
              Add AltText
            </button>
            <button className={styles.removeButton} onClick={onRemove}>
              Remove Image
            </button>
          </div>
        </div>
      ) : (
        <div onClick={handleClick}>
          <input type="file" accept="image/*" ref={fileInputRef} style={{ display: "none" }} onChange={handleUpload} />
          <span>
            <Camera className={styles.pictureIcon} />
            Välj bilder
          </span>
        </div>
      )}

      {isModalOpen && (
        <div className={styles.modal}>
          <div className={styles.modalContent}>
            <label>
              Alt Text:
              <input
                type="text"
                value={altText}
                onChange={(e) => setAltText(e.target.value)}
                className={styles.altTextInput}
              />
            </label>
            <div className={styles.modalActions}>
              <button onClick={handleAltTextSubmit}>Save</button>
              <button onClick={handleCloseModal}>Cancel</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default ImageUploadBox;
