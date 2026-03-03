import styles from "./CardStyle.module.scss";
import React, {type ReactNode} from "react";

interface CardProps {
    message: string;
    number: number;
    icon?: ReactNode;
    messageColor?: string;
    numberColor?: string;
    style?: React.CSSProperties;
    messageFontSize?: React.CSSProperties["fontSize"];
    numberFontSize?: React.CSSProperties["fontSize"];
}

export default function Card({
 message,
 number,
 icon,
 messageColor,
 numberColor,
 style,
 messageFontSize,
 numberFontSize,
}: CardProps) {
    return (
        <div className={styles.cardContainer} style={style}>
            {icon && <div className={styles.cardIcon}>{icon}</div>}
            <h2
                className={styles.cardNumber}
                style={{ color: numberColor, fontSize: numberFontSize }}
            >
                {number}
            </h2>
            <p
                className={styles.cardMessage}
                style={{ color: messageColor, fontSize: messageFontSize }}
            >
                {message}
            </p>
        </div>
    );
}
