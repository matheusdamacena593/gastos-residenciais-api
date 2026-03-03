import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Dashboard from "./Pages/Dashboard/Dashboard.tsx";
import PageBase from "./Pages/PageBase/PageBase.tsx";
import Pessoas from "./Pages/Pessoas/Pessoas.tsx";
import Categorias from "./Pages/Categorias/Categorias.tsx";
import Transacoes from "./Pages/Transacoes/Transacoes.tsx";
import TotaisPessoas from "./Pages/Relatorios/TotaisPessoas/TotaisPessoas.tsx";
import TotaisCategorias from "./Pages/Relatorios/TotaisCategorias/TotaisCategorias.tsx";

export default function AppRouter() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<PageBase />}>
          <Route index element={<Dashboard />} />

          <Route path="pessoas" element={<Pessoas />} />
          <Route path="categorias" element={<Categorias />} />
          <Route path="transacoes" element={<Transacoes />} />

          <Route path="relatorios">
            <Route index element={<Navigate to="totais-pessoas" replace />} />
            <Route path="totais-pessoas" element={<TotaisPessoas />} />
            <Route path="totais-categorias" element={<TotaisCategorias />} />
          </Route>
        </Route>

        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </Router>
  );
}
