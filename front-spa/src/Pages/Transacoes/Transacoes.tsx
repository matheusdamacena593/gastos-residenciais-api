import { useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import toast from "react-hot-toast";

import Button from "@/_components/Button/Button";
import CrudTable from "@/_components/CrudTable/CrudTable";

import { transacoesApiService } from "@/services/transacoes/transacoesApiService";
import { categoriasApiService } from "@/services/categorias/categoriasApiService";
import { pessoasApiService } from "@/services/pessoas/pessoasApiService";

import type {
  Transacao,
  TransacaoCreateRequest,
} from "@/_interfaces/ITransacao";
import { TipoTransacaoEnum } from "@/_interfaces/ITransacao";
import type { Categoria } from "@/_interfaces/ICategoria";
import type { Pessoa } from "@/_interfaces/IPessoa";
import type { PageResult } from "@/_interfaces/IPageResult";

import "./TransacoesStyle.scss";
import TransacaoFormModal from "./_components/TransacaoFormModal";

const initialPageResult: PageResult<Transacao> = {
  page: 1,
  pageSize: 10,
  totalItems: 0,
  totalPages: 1,
  items: [],
};

export default function Transacoes() {
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  // ✅ paginação
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [pageResult, setPageResult] =
    useState<PageResult<Transacao>>(initialPageResult);

  const [errorMessages, setErrorMessages] = useState<string[]>([]);

  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [categorias, setCategorias] = useState<Categoria[]>([]);

  const [modalOpen, setModalOpen] = useState(false);
  const [form, setForm] = useState<TransacaoCreateRequest>({
    descricao: "",
    valor: 0,
    tipoTransacao: TipoTransacaoEnum.Despesa,
    categoriaId: 0,
    pessoaId: 0,
  });

  async function carregar() {
    setLoading(true);
    setErrorMessages([]);

    try {
      const res = await transacoesApiService.listar({ page, pageSize });
      setPageResult(res);
    } catch (err: any) {
      const msgs = Array.isArray(err) ? err : ["Erro ao carregar transações."];
      setErrorMessages(msgs);
      toast.error(msgs[0] ?? "Erro ao carregar transações.");
      setPageResult((prev) => ({ ...prev, items: [] }));
    } finally {
      setLoading(false);
    }
  }

  async function carregarCombos() {
    try {
      // ✅ como listar agora é paginado, pegamos uma pageSize grande para combos
      const [pRes, cRes] = await Promise.all([
        pessoasApiService.listar({ page: 1, pageSize: 9999 }),
        categoriasApiService.listar({ page: 1, pageSize: 9999 }),
      ]);

      setPessoas(pRes.items ?? []);
      setCategorias(cRes.items ?? []);
    } catch (err: any) {
      const msgs = Array.isArray(err)
        ? err
        : ["Não foi possível carregar pessoas e categorias."];

      toast.error(msgs[0] ?? "Erro ao carregar combos.");
    }
  }

  // ✅ combos só 1x
  useEffect(() => {
    carregarCombos();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  // ✅ recarrega lista ao mudar page/pageSize
  useEffect(() => {
    carregar();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [page, pageSize]);

  useEffect(() => {
    if (!modalOpen) return;

    const prev = document.body.style.overflow;
    document.body.style.overflow = "hidden";

    const onKeyDown = (e: KeyboardEvent) => {
      if (e.key === "Escape") fecharModal();
    };

    window.addEventListener("keydown", onKeyDown);

    return () => {
      document.body.style.overflow = prev;
      window.removeEventListener("keydown", onKeyDown);
    };
  }, [modalOpen, saving]);

  function abrirCriar() {
    setErrorMessages([]);

    const firstPessoaId = pessoas[0]?.id ?? 0;
    const firstCategoriaId = categorias[0]?.id ?? 0;

    setForm({
      descricao: "",
      valor: 0,
      tipoTransacao: TipoTransacaoEnum.Despesa,
      pessoaId: firstPessoaId,
      categoriaId: firstCategoriaId,
    });

    setModalOpen(true);
  }

  function fecharModal() {
    if (saving) return;
    setModalOpen(false);
  }

  function validarForm(): string[] {
    const errs: string[] = [];

    if (!form.descricao.trim()) errs.push("Descrição é obrigatória.");
    if (!Number.isFinite(Number(form.valor)) || Number(form.valor) <= 0)
      errs.push("Valor deve ser maior que 0.");

    if (!Number.isFinite(Number(form.tipoTransacao)))
      errs.push("Tipo de transação inválido.");

    if (!Number.isFinite(Number(form.pessoaId)) || Number(form.pessoaId) <= 0)
      errs.push("Selecione uma pessoa válida.");

    if (
      !Number.isFinite(Number(form.categoriaId)) ||
      Number(form.categoriaId) <= 0
    )
      errs.push("Selecione uma categoria válida.");

    return errs;
  }

  function tipoLabel(tipo: number) {
    if (tipo === TipoTransacaoEnum.Receita) return "Receita";
    if (tipo === TipoTransacaoEnum.Despesa) return "Despesa";
    return String(tipo);
  }

  function pessoaLabel(id: number) {
    return pessoas.find((p) => p.id === id)?.nome ?? `#${id}`;
  }

  function categoriaLabel(id: number) {
    return categorias.find((c) => c.id === id)?.descricao ?? `#${id}`;
  }

  async function salvar() {
    const errs = validarForm();
    if (errs.length) {
      setErrorMessages(errs);
      toast.error(errs[0]);
      return;
    }

    setSaving(true);
    setErrorMessages([]);

    try {
      await transacoesApiService.criar({
        descricao: form.descricao.trim(),
        valor: Number(form.valor),
        tipoTransacao: Number(form.tipoTransacao),
        pessoaId: Number(form.pessoaId),
        categoriaId: Number(form.categoriaId),
      });

      toast.success("Transação cadastrada com sucesso!");
      setModalOpen(false);

      // ✅ garante atualização (mesmo se já estiver na página 1)
      if (page === 1) {
        await carregar();
      } else {
        setPage(1);
      }
    } catch (err: any) {
      const msgs = Array.isArray(err) ? err : ["Erro ao salvar."];
      setErrorMessages(msgs);
      toast.error(msgs[0] ?? "Erro ao salvar.");
    } finally {
      setSaving(false);
    }
  }

  return (
    <section className="transacoesPage">
      <header className="transacoesHeader">
        <div>
          <h1 className="transacoesTitle">Transações</h1>
          <p className="transacoesSubtitle">
            Cadastre e consulte transações do sistema.
          </p>
        </div>

        <div className="transacoesHeaderActions">
          <Button
            text="Nova Transação"
            icon={<FontAwesomeIcon icon={faPlus} />}
            onClick={abrirCriar}
            variant="primary"
            size="sm"
          />
        </div>
      </header>

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

      <CrudTable<Transacao>
        loading={loading}
        items={pageResult.items}
        getRowKey={(t) => t.id}
        totalLabel={<span>Total: {pageResult.totalItems}</span>}
        headerRight={
          <Button
            text="Atualizar"
            onClick={carregar}
            disabled={loading}
            variant="primary"
            size="sm"
          />
        }
        columns={[
          {
            header: "Descrição",
            accessor: "descricao",
            mobileLabel: "Descrição",
          },
          {
            header: "Valor",
            mobileLabel: "Valor",
            className: "colSmall",
            render: (t) =>
              Number(t.valor).toLocaleString("pt-BR", {
                style: "currency",
                currency: "BRL",
              }),
          },
          {
            header: "Tipo",
            mobileLabel: "Tipo",
            className: "colSmall",
            render: (t) => tipoLabel(t.tipoTransacao),
          },
          {
            header: "Pessoa",
            mobileLabel: "Pessoa",
            render: (t) => pessoaLabel(t.pessoaId),
          },
          {
            header: "Categoria",
            mobileLabel: "Categoria",
            render: (t) => categoriaLabel(t.categoriaId),
          },
        ]}
        emptyTitle="Nenhuma transação encontrada"
        emptyText="Cadastre uma transação para começar."
        emptyAction={
          <Button
            text="Cadastrar Transação"
            icon={<FontAwesomeIcon icon={faPlus} />}
            onClick={abrirCriar}
            variant="primary"
            size="sm"
          />
        }
        pagination={{
          page,
          pageSize,
          totalPages: pageResult.totalPages,
          totalItems: pageResult.totalItems,
          onPageChange: (p) => setPage(p),
          onPageSizeChange: (ps) => {
            setPageSize(ps);
            setPage(1);
          },
          disabled: loading || saving,
        }}
      />

      <TransacaoFormModal
        open={modalOpen}
        saving={saving}
        value={form}
        setValue={setForm}
        onClose={fecharModal}
        onSave={salvar}
        errors={errorMessages}
        pessoas={pessoas}
        categorias={categorias}
      />
    </section>
  );
}
