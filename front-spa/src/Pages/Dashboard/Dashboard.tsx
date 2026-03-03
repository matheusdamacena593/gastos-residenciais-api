import { useCallback, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faUsers,
  faTags,
  faMoneyBillWave,
  faChartLine,
  faArrowUpRightFromSquare,
} from "@fortawesome/free-solid-svg-icons";
import "./DashboardStyle.scss";
import Button from "@/_components/Button/Button";

import { pessoasApiService } from "@/services/pessoas/pessoasApiService";
import { categoriasApiService } from "@/services/categorias/categoriasApiService";
import { transacoesApiService } from "@/services/transacoes/transacoesApiService";

type DashboardStats = {
  pessoas: number;
  categorias: number;
  transacoes: number;
};

type LoadState =
  | { status: "idle" | "loading" }
  | { status: "success"; data: DashboardStats }
  | { status: "error"; message: string };

function StatCard(props: {
  title: string;
  value: number;
  icon: any;
  hint?: string;
  to?: string;
  loading?: boolean;
}) {
  const { title, value, icon, hint, to, loading } = props;

  const content = (
    <>
      <div className="statCard__icon" aria-hidden="true">
        <FontAwesomeIcon icon={icon} />
      </div>

      <div className="statCard__body">
        <div className="statCard__title">{title}</div>
        <div className={`statCard__value ${loading ? "is-loading" : ""}`}>
          {loading ? "—" : value.toLocaleString("pt-BR")}
        </div>
        {hint ? <div className="statCard__hint">{hint}</div> : null}
      </div>

      {to ? (
        <div className="statCard__cta" aria-hidden="true">
          <FontAwesomeIcon icon={faArrowUpRightFromSquare} />
        </div>
      ) : null}
    </>
  );

  return to ? (
    <Link to={to} className="statCard statCard--link">
      {content}
    </Link>
  ) : (
    <div className="statCard">{content}</div>
  );
}

export default function Dashboard() {
  const [state, setState] = useState<LoadState>({ status: "loading" });

  const load = useCallback(async () => {
    try {
      setState({ status: "loading" });

      const [pessoas, categorias, transacoes] = await Promise.all([
        pessoasApiService.count(),
        categoriasApiService.count(),
        transacoesApiService.count(),
      ]);

      setState({
        status: "success",
        data: { pessoas, categorias, transacoes },
      });
    } catch (err: any) {
      const msg = Array.isArray(err)
        ? (err[0] ?? "Erro ao carregar os dados do dashboard")
        : "Erro ao carregar os dados do dashboard";

      setState({ status: "error", message: msg });
    }
  }, []);

  useEffect(() => {
    load();
  }, [load]);

  const isLoading = state.status === "loading";
  const isError = state.status === "error";

  const data: DashboardStats =
    state.status === "success"
      ? state.data
      : { pessoas: 0, categorias: 0, transacoes: 0 };

  return (
    <section className="dashboard">
      <header className="dashboard__header">
        <div>
          <h1 className="dashboard__title">Visão Geral</h1>
          <p className="dashboard__subtitle">
            Quantidades cadastradas e volume de transações.
          </p>
        </div>

        <div className="dashboard__actions">
          <Button
            text="Atualizar"
            onClick={load}
            disabled={isLoading}
            variant="primary"
            size="sm"
          />
        </div>
      </header>

      {isError ? (
        <div className="dashboard__error">
          <strong>Não foi possível carregar os dados.</strong>
          <span>{state.message}</span>
        </div>
      ) : null}

      <div className="dashboard__grid">
        <StatCard
          title="Pessoas cadastradas"
          value={data.pessoas}
          icon={faUsers}
          hint={isLoading ? "Carregando..." : "Total no cadastro"}
          to="/pessoas"
          loading={isLoading}
        />

        <StatCard
          title="Categorias cadastradas"
          value={data.categorias}
          icon={faTags}
          hint={isLoading ? "Carregando..." : "Total no cadastro"}
          to="/categorias"
          loading={isLoading}
        />

        <StatCard
          title="Transações realizadas"
          value={data.transacoes}
          icon={faMoneyBillWave}
          hint={isLoading ? "Carregando..." : "Total registrado"}
          to="/transacoes"
          loading={isLoading}
        />
      </div>

      <div className="quickActions">
        <h2 className="quickActions__title">Ações rápidas</h2>

        <div className="quickActions__grid">
          <Link className="quickAction" to="/pessoas">
            <FontAwesomeIcon icon={faUsers} />
            <span>Gerenciar pessoas</span>
          </Link>

          <Link className="quickAction" to="/categorias">
            <FontAwesomeIcon icon={faTags} />
            <span>Gerenciar categorias</span>
          </Link>

          <Link className="quickAction" to="/transacoes">
            <FontAwesomeIcon icon={faMoneyBillWave} />
            <span>Nova transação</span>
          </Link>

          <Link className="quickAction" to="/relatorios/totais-pessoas">
            <FontAwesomeIcon icon={faChartLine} />
            <span>Ver relatórios</span>
          </Link>
        </div>
      </div>
    </section>
  );
}
