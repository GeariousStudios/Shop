import React, { ChangeEvent, FC, useEffect, useRef, useState } from "react";
import styles from "./imageUploadBox.module.css";
import { Camera, X, CircleHelp } from "lucide-react";
import Modal from "./modal";

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
  const [charCount, setCharCount] = useState<number>(0);

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

  const handleAltTextChange = (e: ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    if (value.length <= 150) {
      setAltText(value);
      setCharCount(value.length);
    }
  };

  return (
    <div className={styles.imageUploadBox}>
      {image ? (
        <>
          <a href={image} target="_blank" rel="noopener noreferrer">
            <img src={image} alt={altText || ""} className={styles.uploadedImage} />
          </a>
          <div className={styles.imageActions}>
            <button className={styles.altTextButton} onClick={handleOpenModal} type="button">
              <CircleHelp className={styles.altIcon} />
            </button>
            <button className={styles.removeButton} onClick={onRemove}>
              <X className={styles.closeIcon} />
            </button>
          </div>
        </>
      ) : (
        <div onClick={handleClick}>
          <input type="file" accept="image/*" ref={fileInputRef} style={{ display: "none" }} onChange={handleUpload} />
          <span>
            <Camera className={styles.pictureIcon} />
            Välj bilder
          </span>
        </div>
      )}

      <Modal
        isOpen={isModalOpen}
        onClose={handleCloseModal}
        windowName="Bildinformation"
        closeButton={true}
        saveButton={true}
        onSave={handleAltTextSubmit}
      >
        <span className={styles.charCount}>Alt-text {charCount}/150</span>
        <input type="text" value={altText} onChange={handleAltTextChange} className={styles.altTextInput} />
        <h5>Beskriv kortfattat vad det är för bild</h5>
      </Modal>
    </div>
  );
};

export default ImageUploadBox;
