import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import AppRouter from "./routes.tsx";
import { Toaster } from "react-hot-toast";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <AppRouter />

    <Toaster
      position="top-right"
      toastOptions={{
        duration: 2500,
        style: {
          color: "#fff",
          borderRadius: "12px",
          fontWeight: 700,
        },
        success: {
          style: { background: "#16a34a" },
        },
        error: {
          style: { background: "#dc3545" },
        },
      }}
    />
  </StrictMode>,
);
