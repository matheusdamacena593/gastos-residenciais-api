import styles from "./SelectStyle.module.scss";

export type SelectOption<T extends string | number> = {
  value: T;
  label: string;
};

type Props<T extends string | number> = {
  label?: string;
  value: T;
  options: SelectOption<T>[];

  onChange: (value: T) => void;
  onBlur?: () => void;

  placeholder?: string;
  disabled?: boolean;

  error?: string;
  className?: string;
};

export default function Select<T extends string | number>({
  label,
  value,
  options,
  onChange,
  onBlur,
  placeholder = "Selecione...",
  disabled = false,
  error,
  className,
}: Props<T>) {
  return (
    <div className={`${styles.wrapper} ${className ?? ""}`}>
      {label ? <span className={styles.label}>{label}</span> : null}

      <select
        className={`${styles.select} ${error ? styles.selectError : ""}`}
        value={String(value)}
        onChange={(e) => onChange(Number(e.target.value) as unknown as T)}
        onBlur={onBlur}
        disabled={disabled}
      >
        {/* placeholder opcional */}
        {placeholder ? (
          <option value="" disabled>
            {placeholder}
          </option>
        ) : null}

        {options.map((opt) => (
          <option key={String(opt.value)} value={String(opt.value)}>
            {opt.label}
          </option>
        ))}
      </select>

      {error ? <small className={styles.error}>{error}</small> : null}
    </div>
  );
}
