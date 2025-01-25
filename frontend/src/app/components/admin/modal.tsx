import React, { FC, ReactNode, useEffect } from "react";
import styles from "./modal.module.css";
import { X } from "lucide-react";

type ModalProps = {
  isOpen: boolean;
  onClose: () => void;
  children: ReactNode;
  windowName?: string;
  closeButton?: boolean;
  saveButton?: boolean;
  onSave?: () => void;
};

const Modal: FC<ModalProps> = ({ isOpen, onClose, children, windowName, closeButton, saveButton, onSave }) => {
  useEffect(() => {
    if (isOpen) {
      document.body.style.overflow = "hidden";
    } else {
      document.body.style.overflow = "auto";
    }
    return () => {
      document.body.style.overflow = "auto";
    };
  }, [isOpen]);

  if (!isOpen) return null;

  return (
    <div className={styles.modalOverlay}>
      <div className={styles.modalContent} onClick={(e) => e.stopPropagation()}>
        <div className={styles.modalHeader}>
          {windowName}
          <X className={styles.closeButton} onClick={onClose} aria-label="Close" />
        </div>
        <div className={styles.modalBody}>{children}</div>
        <div className={styles.modalActions}>
          {closeButton && <button className="modalCloseButton" onClick={onClose} />}
          {saveButton && <button className="modalSaveButton" onClick={onSave} />}
        </div>
      </div>
    </div>
  );
};

export default Modal;
