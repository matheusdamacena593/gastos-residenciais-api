import styles from "./ButtonStyle.module.scss";
import React from "react";

type TypeButtonProps = "submit" | "reset" | "button";
type ButtonVariant = "primary" | "ghost" | "danger";

interface ButtonProps {
  text: string;
  onClick?: () => void;
  type?: TypeButtonProps;
  disabled?: boolean;
  style?: React.CSSProperties;

  icon?: React.ReactNode;
  iconOnly?: boolean;
  variant?: ButtonVariant;
  title?: string;
  className?: string;
  size?: "sm" | "md";
}

export default function Button({
  text,
  onClick,
  type = "button",
  disabled = false,
  style,
  icon,
  iconOnly = false,
  variant = "primary",
  title,
  className,
  size = "md"
}: ButtonProps) {
  return (
    <button
      type={type}
      onClick={onClick}
      title={title ?? text}
      className={[
        styles.customButton,
        styles[size],
        styles[variant],
        iconOnly ? styles.iconOnly : "",
        className ?? "",
      ].join(" ")}
      disabled={disabled}
      style={style}
      aria-label={iconOnly ? text : undefined}
    >
      {icon ? <span className={styles.icon}>{icon}</span> : null}
      {iconOnly ? null : text}
    </button>
  );
}
