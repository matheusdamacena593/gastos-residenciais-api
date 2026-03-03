import Sidebar from "../../_components/Sidebar/Sidebar";
import { Outlet } from "react-router-dom";

export default function PageBase() {
  return (
    <div className="d-flex flex-column min-vh-100">
      <div className="d-flex flex-grow-1">
        <Sidebar />

        <main
          className="flex-grow-1 p-4"
          style={{ backgroundColor: "#f8f9fa" }}
        >
          <Outlet />
        </main>
      </div>
    </div>
  );
}
