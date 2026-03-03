import React from "react";
import styles from "./ModalStyle.module.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faXmark } from "@fortawesome/free-solid-svg-icons";

interface ModalProps {
  onClose: () => void;
  title?: string;
  children?: React.ReactNode;
  modalStyle?: React.CSSProperties;
  showCloseButton?: boolean;
  closeDisabled?: boolean;
}

export default function Modal({
  onClose,
  title,
  children,
  modalStyle,
  showCloseButton = true,
  closeDisabled = false,
}: ModalProps) {
  const safeClose = () => {
    if (closeDisabled) return;
    onClose();
  };

  return (
    <div
      className={styles.overlay}
      role="dialog"
      aria-modal="true"
      aria-label={title ?? "Modal"}
      onMouseDown={(e) => {
        if (e.target === e.currentTarget) safeClose();
      }}
    >
      <div
        className={styles.modal}
        onMouseDown={(e) => e.stopPropagation()}
        style={modalStyle}
      >
        {(title || showCloseButton) && (
          <div className={styles.header}>
            {title ? <h2 className={styles.title}>{title}</h2> : null}

            {showCloseButton ? (
              <button
                className={styles.close}
                onClick={safeClose}
                disabled={closeDisabled}
                title="Fechar"
                type="button"
              >
                <FontAwesomeIcon icon={faXmark} />
              </button>
            ) : null}
          </div>
        )}

        <div className={styles.content}>{children}</div>
      </div>
    </div>
  );
}
