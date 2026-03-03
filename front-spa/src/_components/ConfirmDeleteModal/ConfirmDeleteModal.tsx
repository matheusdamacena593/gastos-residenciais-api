import React from "react";
import Modal from "../Modal/Modal";
import Button from "../Button/Button";

type Props = {
  open: boolean;
  title: string;
  children?: React.ReactNode;
  loading?: boolean;
  onClose: () => void;
  onConfirm: () => void;
};

export default function ConfirmDeleteModal({
  open,
  title,
  children,
  loading = false,
  onClose,
  onConfirm,
}: Props) {
  if (!open) return null;

  return (
    <Modal onClose={onClose} title={title} closeDisabled={loading}>
      {children}

      <div
        style={{
          display: "flex",
          justifyContent: "flex-end",
          gap: 10,
          marginTop: 14,
        }}
      >
        <Button
          text="Cancelar"
          variant="ghost"
          onClick={onClose}
          disabled={loading}
        />
        <Button
          text={loading ? "Excluindo..." : "Excluir"}
          variant="danger"
          onClick={onConfirm}
          disabled={loading}
        />
      </div>
    </Modal>
  );
}
