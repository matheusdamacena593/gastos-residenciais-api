import  styles from './TextAreaStyle.module.scss';

interface TextAreaProps {
    placeholder?: string;
    value?: string;
    onChange?: (e: React.ChangeEvent<HTMLTextAreaElement>) => void;
    required?: boolean;
    rows?: number;
}

export default function TextArea({
     placeholder,
     value,
     onChange,
     required = false,
     rows = 4
}: TextAreaProps) {
    return (
        <div className="mb-3">
            <textarea
                placeholder={placeholder}
                value={value}
                onChange={onChange}
                className={styles.customTextArea}
                required={required}
                rows={rows}
            />
        </div>
    );
}