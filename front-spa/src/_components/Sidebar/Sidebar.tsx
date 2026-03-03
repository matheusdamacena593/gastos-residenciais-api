import { useEffect, useMemo, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faUsers,
  faTags,
  faMoneyBillWave,
  faChartLine,
  faBars,
  faAnglesLeft,
  faAnglesRight,
  faTachometerAlt,
  faChevronDown,
} from "@fortawesome/free-solid-svg-icons";
import "./SidebarStyle.scss";
import logoMd from "@assets/logo-matheus-damacena.png";

const Sidebar = () => {
  const location = useLocation();
  const navigate = useNavigate();

  const [showSidebar, setShowSidebar] = useState(false);
  const [collapsed, setCollapsed] = useState(false);
  const [reportsOpen, setReportsOpen] = useState(false);

  const sidebarBg = "#0B0B0D";
  const hoverBg = "rgba(255,255,255,0.08)";
  const activeBg = "#FFFFFF";
  const activeText = "#0B0B0D";

  const reportChildren = useMemo(
    () => [
      { label: "Totais por Pessoas", path: "/relatorios/totais-pessoas" },
      { label: "Totais por Categorias", path: "/relatorios/totais-categorias" },
    ],
    [],
  );

  const isRelatoriosRoute = location.pathname.startsWith("/relatorios");

  useEffect(() => {
    if (isRelatoriosRoute) setReportsOpen(true);
  }, [isRelatoriosRoute]);

  const toggleSidebar = () => setShowSidebar((s) => !s);
  const toggleCollapsed = () => setCollapsed((c) => !c);

  const isMobile = () => window.matchMedia("(max-width: 767.98px)").matches;
  const isDesktopCollapsed = () => collapsed && !isMobile();

  function handleToggleReports() {
    if (isDesktopCollapsed()) {
      navigate(reportChildren[0].path);
      setShowSidebar(false);
      return;
    }
    setReportsOpen((v) => !v);
  }

  return (
    <>
      <div
        className="d-md-none text-white p-2"
        style={{ backgroundColor: sidebarBg }}
      >
        <button
          className="btn btn-outline-light"
          onClick={toggleSidebar}
          aria-label="Abrir menu"
        >
          <FontAwesomeIcon icon={faBars} />
        </button>
      </div>

      <div
        className={[
          "sidebar-container",
          "text-white",
          showSidebar ? "show-sidebar" : "",
          collapsed ? "is-collapsed" : "",
        ].join(" ")}
        style={{ backgroundColor: sidebarBg }}
      >
        <div className="sidebar-header p-4 border-bottom border-secondary d-flex justify-content-between align-items-center">
          <div className="sidebar-brand d-flex align-items-center">
            <img
              src={logoMd}
              alt="Logo"
              className="sidebar-logo"
              draggable={false}
            />
          </div>

          <div className="sidebar-header-actions d-flex align-items-center gap-2">
            <button
              className="btn btn-outline-light d-none d-md-inline-flex"
              onClick={toggleCollapsed}
              aria-label={collapsed ? "Expandir menu" : "Colapsar menu"}
              title={collapsed ? "Expandir" : "Colapsar"}
            >
              <FontAwesomeIcon
                icon={collapsed ? faAnglesRight : faAnglesLeft}
              />
            </button>

            <button
              className="btn btn-outline-light d-md-none"
              onClick={toggleSidebar}
              aria-label="Fechar menu"
            >
              <FontAwesomeIcon icon={faAnglesLeft} />
            </button>
          </div>
        </div>

        <nav className="flex-grow-1 px-3 mt-3">
          <ul className="nav flex-column">
            <li className="nav-item mb-2">
              <Link
                to="/"
                className="nav-link d-flex align-items-center px-3 py-2 rounded"
                title={collapsed ? "Dashboard" : undefined}
                style={{
                  backgroundColor:
                    location.pathname === "/" ? activeBg : "transparent",
                  color: location.pathname === "/" ? activeText : "white",
                  WebkitTextFillColor:
                    location.pathname === "/" ? activeText : "white",
                  transition: "background-color .15s ease, color .15s ease",
                }}
                onMouseEnter={(e) => {
                  if (location.pathname !== "/")
                    e.currentTarget.style.backgroundColor = hoverBg;
                }}
                onMouseLeave={(e) => {
                  if (location.pathname !== "/")
                    e.currentTarget.style.backgroundColor = "transparent";
                }}
                onClick={() => setShowSidebar(false)}
              >
                <FontAwesomeIcon
                  icon={faTachometerAlt}
                  className="sidebar-icon"
                />
                <span className="sidebar-label">Dashboard</span>
              </Link>
            </li>

            <li className="nav-item mb-2">
              <Link
                to="/pessoas"
                className="nav-link d-flex align-items-center px-3 py-2 rounded"
                title={collapsed ? "Pessoas" : undefined}
                style={{
                  backgroundColor: location.pathname.startsWith("/pessoas")
                    ? activeBg
                    : "transparent",
                  color: location.pathname.startsWith("/pessoas")
                    ? activeText
                    : "white",
                  WebkitTextFillColor: location.pathname.startsWith("/pessoas")
                    ? activeText
                    : "white",
                  transition: "background-color .15s ease, color .15s ease",
                }}
                onMouseEnter={(e) => {
                  if (!location.pathname.startsWith("/pessoas"))
                    e.currentTarget.style.backgroundColor = hoverBg;
                }}
                onMouseLeave={(e) => {
                  if (!location.pathname.startsWith("/pessoas"))
                    e.currentTarget.style.backgroundColor = "transparent";
                }}
                onClick={() => setShowSidebar(false)}
              >
                <FontAwesomeIcon icon={faUsers} className="sidebar-icon" />
                <span className="sidebar-label">Pessoas</span>
              </Link>
            </li>

            <li className="nav-item mb-2">
              <Link
                to="/categorias"
                className="nav-link d-flex align-items-center px-3 py-2 rounded"
                title={collapsed ? "Categorias" : undefined}
                style={{
                  backgroundColor: location.pathname.startsWith("/categorias")
                    ? activeBg
                    : "transparent",
                  color: location.pathname.startsWith("/categorias")
                    ? activeText
                    : "white",
                  WebkitTextFillColor: location.pathname.startsWith(
                    "/categorias",
                  )
                    ? activeText
                    : "white",
                  transition: "background-color .15s ease, color .15s ease",
                }}
                onMouseEnter={(e) => {
                  if (!location.pathname.startsWith("/categorias"))
                    e.currentTarget.style.backgroundColor = hoverBg;
                }}
                onMouseLeave={(e) => {
                  if (!location.pathname.startsWith("/categorias"))
                    e.currentTarget.style.backgroundColor = "transparent";
                }}
                onClick={() => setShowSidebar(false)}
              >
                <FontAwesomeIcon icon={faTags} className="sidebar-icon" />
                <span className="sidebar-label">Categorias</span>
              </Link>
            </li>

            <li className="nav-item mb-2">
              <Link
                to="/transacoes"
                className="nav-link d-flex align-items-center px-3 py-2 rounded"
                title={collapsed ? "Transações" : undefined}
                style={{
                  backgroundColor: location.pathname.startsWith("/transacoes")
                    ? activeBg
                    : "transparent",
                  color: location.pathname.startsWith("/transacoes")
                    ? activeText
                    : "white",
                  WebkitTextFillColor: location.pathname.startsWith(
                    "/transacoes",
                  )
                    ? activeText
                    : "white",
                  transition: "background-color .15s ease, color .15s ease",
                }}
                onMouseEnter={(e) => {
                  if (!location.pathname.startsWith("/transacoes"))
                    e.currentTarget.style.backgroundColor = hoverBg;
                }}
                onMouseLeave={(e) => {
                  if (!location.pathname.startsWith("/transacoes"))
                    e.currentTarget.style.backgroundColor = "transparent";
                }}
                onClick={() => setShowSidebar(false)}
              >
                <FontAwesomeIcon
                  icon={faMoneyBillWave}
                  className="sidebar-icon"
                />
                <span className="sidebar-label">Transações</span>
              </Link>
            </li>

            <li className="nav-item mb-2">
              <button
                type="button"
                className="nav-link d-flex align-items-center px-3 py-2 rounded w-100 justify-content-between"
                title={collapsed ? "Relatórios" : undefined}
                style={{
                  backgroundColor: isRelatoriosRoute ? activeBg : "transparent",
                  color: isRelatoriosRoute ? activeText : "white",
                  WebkitTextFillColor: isRelatoriosRoute ? activeText : "white",
                  transition: "background-color .15s ease, color .15s ease",
                  border: "none",
                }}
                onMouseEnter={(e) => {
                  if (!isRelatoriosRoute)
                    e.currentTarget.style.backgroundColor = hoverBg;
                }}
                onMouseLeave={(e) => {
                  if (!isRelatoriosRoute)
                    e.currentTarget.style.backgroundColor = "transparent";
                }}
                onClick={handleToggleReports}
              >
                <span className="d-flex align-items-center">
                  <FontAwesomeIcon
                    icon={faChartLine}
                    className="sidebar-icon"
                  />
                  <span className="sidebar-label">Relatórios</span>
                </span>

                <FontAwesomeIcon
                  icon={faChevronDown}
                  className={`report-chevron ${reportsOpen ? "open" : ""}`}
                />
              </button>

              {reportsOpen && (
                <ul className="nav flex-column report-submenu">
                  {reportChildren.map((c) => {
                    const active =
                      location.pathname === c.path ||
                      location.pathname.startsWith(c.path + "/");

                    return (
                      <li key={c.path} className="nav-item">
                        <Link
                          to={c.path}
                          className="nav-link px-3 py-2 rounded report-subitem"
                          style={{
                            backgroundColor: active
                              ? "rgba(255,255,255,0.12)"
                              : "transparent",
                            color: "white",
                            transition: "background-color .15s ease",
                          }}
                          onClick={() => setShowSidebar(false)}
                        >
                          {c.label}
                        </Link>
                      </li>
                    );
                  })}
                </ul>
              )}
            </li>
          </ul>
        </nav>
      </div>
    </>
  );
};

export default Sidebar;
