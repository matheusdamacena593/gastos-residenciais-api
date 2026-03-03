import { useEffect, useState } from "react";
import toast from "react-hot-toast";

import Button from "@/_components/Button/Button";
import CrudTable from "@/_components/CrudTable/CrudTable";

import { relatoriosApiService } from "@/services/relatorios/relatoriosApiService";

import type {
  RelatorioTotaisCategoriasResponse,
  RelatorioTotaisCategoriaItem,
} from "@/_interfaces/IRelatorios";

import { FinalidadeCategoriaEnum } from "@/_interfaces/ICategoria";

import "./TotaisCategoriasStyle.scss";

function downloadBlob(blob: Blob, filename: string) {
  const url = window.URL.createObjectURL(blob);
  const a = document.createElement("a");
  a.href = url;
  a.download = filename;
  document.body.appendChild(a);
  a.click();
  a.remove();
  window.URL.revokeObjectURL(url);
}

export default function TotaisCategorias() {
  const [loading, setLoading] = useState(true);
  const [errorMessages, setErrorMessages] = useState<string[]>([]);
  const [data, setData] = useState<RelatorioTotaisCategoriasResponse | null>(
    null,
  );

  // ✅ paginação
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  const [openPdfModal, setOpenPdfModal] = useState(false);
  const [downloadingPdf, setDownloadingPdf] = useState(false);

  async function carregar() {
    setLoading(true);
    setErrorMessages([]);

    try {
      const res = await relatoriosApiService.totaisCategorias(page, pageSize);
      setData(res);
    } catch (err: any) {
      const msgs = Array.isArray(err) ? err : ["Erro ao carregar relatório."];
      setErrorMessages(msgs);
      toast.error(msgs[0] ?? "Erro ao carregar relatório.");
      setData(null);
    } finally {
      setLoading(false);
    }
  }

  async function baixarPdfCategorias() {
    setDownloadingPdf(true);
    try {
      const file = await relatoriosApiService.pdfTotaisCategorias();

      if (!file) {
        toast("Não há dados para gerar o PDF.");
        setOpenPdfModal(false);
        return;
      }

      downloadBlob(file.blob, file.filename);
      toast.success("Download iniciado.");
      setOpenPdfModal(false);
    } catch (err: any) {
      const msgs = Array.isArray(err) ? err : ["Erro ao baixar PDF."];
      toast.error(msgs[0] ?? "Erro ao baixar PDF.");
    } finally {
      setDownloadingPdf(false);
    }
  }

  // ✅ recarrega quando page/pageSize mudarem
  useEffect(() => {
    carregar();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [page, pageSize]);

  const categoriasPage = data?.categorias;
  const categorias = categoriasPage?.items ?? [];
  const totalItems = categoriasPage?.totalItems ?? 0;
  const totalPages = categoriasPage?.totalPages ?? 1;

  const moeda = (v: number) =>
    Number(v).toLocaleString("pt-BR", { style: "currency", currency: "BRL" });

  function finalidadeLabel(v: number) {
    if (v === FinalidadeCategoriaEnum.Despesa) return "Despesa";
    if (v === FinalidadeCategoriaEnum.Receita) return "Receita";
    if (v === FinalidadeCategoriaEnum.Ambas) return "Ambas";
    return String(v);
  }

  return (
    <section className="totaisCategoriasPage">
      <header className="totaisCategoriasHeader">
        <div>
          <h1 className="totaisCategoriasTitle">Totais por Categorias</h1>
          <p className="totaisCategoriasSubtitle">
            Visualize receitas, despesas e saldo agrupados por categoria.
          </p>
        </div>

        <div className="totaisCategoriasHeaderActions">
          <Button
            text="Exportar PDF"
            onClick={() => setOpenPdfModal(true)}
            disabled={loading || downloadingPdf}
            variant="primary"
            size="sm"
          />
          <Button
            text="Atualizar"
            onClick={carregar}
            disabled={loading}
            variant="primary"
            size="sm"
          />
        </div>
      </header>

      {openPdfModal ? (
        <div
          className="downloadModalOverlay"
          onClick={() => !downloadingPdf && setOpenPdfModal(false)}
        >
          <div
            className="downloadModal"
            role="dialog"
            aria-modal="true"
            onClick={(e) => e.stopPropagation()}
          >
            <h3>Exportar relatório</h3>
            <p>
              Será feito o download do{" "}
              <strong>relatório de totais por categorias</strong> em PDF. Deseja
              continuar?
            </p>

            <div className="downloadModalActions">
              <button
                className="downloadModalCancel"
                onClick={() => setOpenPdfModal(false)}
                disabled={downloadingPdf}
              >
                Cancelar
              </button>

              <Button
                text={downloadingPdf ? "Baixando..." : "Baixar PDF"}
                onClick={baixarPdfCategorias}
                disabled={downloadingPdf}
                variant="primary"
                size="sm"
              />
            </div>
          </div>
        </div>
      ) : null}

      {errorMessages.length > 0 ? (
        <div className="alertError">
          <strong>Ocorreram erros:</strong>
          <ul>
            {errorMessages.map((m, i) => (
              <li key={i}>{m}</li>
            ))}
          </ul>
        </div>
      ) : null}

      {data ? (
        <div className="totaisResumo">
          <div className="totaisResumoItem">
            <span>Receitas (geral)</span>
            <strong>{moeda(data.totalReceitasGeral)}</strong>
          </div>
          <div className="totaisResumoItem">
            <span>Despesas (geral)</span>
            <strong>{moeda(data.totalDespesasGeral)}</strong>
          </div>
          <div className="totaisResumoItem">
            <span>Saldo (geral)</span>
            <strong>{moeda(data.saldoGeral)}</strong>
          </div>
        </div>
      ) : null}

      <CrudTable<RelatorioTotaisCategoriaItem>
        loading={loading}
        items={categorias}
        getRowKey={(c) => c.id}
        totalLabel={<span>Total: {totalItems}</span>}
        headerRight={null}
        columns={[
          {
            header: "Categoria",
            accessor: "descricao",
            mobileLabel: "Categoria",
          },
          {
            header: "Finalidade",
            mobileLabel: "Finalidade",
            className: "colSmall",
            render: (c) => finalidadeLabel(c.finalidade),
          },
          {
            header: "Receitas",
            mobileLabel: "Receitas",
            className: "colMoney",
            render: (c) => moeda(c.totalReceitas),
          },
          {
            header: "Despesas",
            mobileLabel: "Despesas",
            className: "colMoney",
            render: (c) => moeda(c.totalDespesas),
          },
          {
            header: "Saldo",
            mobileLabel: "Saldo",
            className: "colMoney",
            render: (c) => moeda(c.saldo),
          },
        ]}
        emptyTitle="Sem dados para exibir"
        emptyText="Ainda não há totais por categorias."
        pagination={
          categoriasPage
            ? {
                page,
                pageSize,
                totalPages,
                totalItems,
                onPageChange: (p) => setPage(p),
                onPageSizeChange: (ps) => {
                  setPageSize(ps);
                  setPage(1);
                },
                disabled: loading || downloadingPdf,
              }
            : undefined
        }
      />
    </section>
  );
}
