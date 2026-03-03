import styles from "./InputStyle.module.scss";
import React from "react";

type TypeInputProps = "text" | "password" | "email" | "number";

interface InputProps {
  placeholder?: string;
  type?: TypeInputProps;
  value?: string | number;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onBlur?: (e: React.FocusEvent<HTMLInputElement>) => void;
  required?: boolean;
  className?: string;
  disabled?: boolean;
  min?: number;
}

export default function Input({
  placeholder,
  type = "text",
  value,
  onChange,
  onBlur,
  required = false,
  className,
  disabled = false,
  min,
}: InputProps) {
  return (
    <div>
      <input
        placeholder={placeholder}
        type={type}
        value={value ?? ""}
        onChange={onChange}
        onBlur={onBlur}
        className={`${styles.customInput} ${className ?? ""}`}
        required={required}
        disabled={disabled}
        min={type === "number" ? min : undefined}
      />
    </div>
  );
}
