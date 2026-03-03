import { useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import toast from "react-hot-toast";
import Button from "@/_components/Button/Button";
import CrudTable from "@/_components/CrudTable/CrudTable";
import type {
  Categoria,
  CategoriaCreateRequest,
} from "@/_interfaces/ICategoria";
import type { PageResult } from "@/_interfaces/IPageResult";
import "./CategoriasStyle.scss";
import { categoriasApiService } from "@/services/categorias/categoriasApiService";
import CategoriaFormModal from "./_components/CategoriaFormModal";

const initialPageResult: PageResult<Categoria> = {
  page: 1,
  pageSize: 10,
  totalItems: 0,
  totalPages: 1,
  items: [],
};

export default function Categorias() {
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [pageResult, setPageResult] =
    useState<PageResult<Categoria>>(initialPageResult);

  const [errorMessages, setErrorMessages] = useState<string[]>([]);

  const [modalOpen, setModalOpen] = useState(false);
  const [form, setForm] = useState<CategoriaCreateRequest>({
    descricao: "",
    finalidade: 0,
  });

  async function carregar() {
    setLoading(true);
    setErrorMessages([]);

    try {
      const res = await categoriasApiService.listar({ page, pageSize });
      setPageResult(res);
    } catch (err: any) {
      const msgs = Array.isArray(err) ? err : ["Erro ao carregar categorias."];
      setErrorMessages(msgs);
      toast.error(msgs[0] ?? "Erro ao carregar categorias.");
      setPageResult((prev) => ({ ...prev, items: [] }));
    } finally {
      setLoading(false);
    }
  }

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

  useEffect(() => {
    carregar();
  }, [page, pageSize]);

  function abrirCriar() {
    setForm({ descricao: "", finalidade: 0 });
    setErrorMessages([]);
    setModalOpen(true);
  }

  function fecharModal() {
    if (saving) return;
    setModalOpen(false);
  }

  function validarForm(): string[] {
    const errs: string[] = [];
    if (!form.descricao.trim()) errs.push("Descrição é obrigatória.");
    if (!Number.isFinite(form.finalidade)) errs.push("Finalidade inválida.");
    return errs;
  }

  function finalidadeLabel(finalidade: number) {
    if (finalidade === 0) return "Despesa";
    if (finalidade === 1) return "Receita";
    if (finalidade === 2) return "Ambas";
    return String(finalidade);
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
      await categoriasApiService.criar({
        descricao: form.descricao.trim(),
        finalidade: Number(form.finalidade),
      });

      toast.success("Categoria cadastrada com sucesso!");
      setModalOpen(false);

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
    <section className="categoriasPage">
      <header className="categoriasHeader">
        <div>
          <h1 className="categoriasTitle">Categorias</h1>
          <p className="categoriasSubtitle">
            Cadastre e consulte categorias do sistema.
          </p>
        </div>

        <div className="categoriasHeaderActions">
          <Button
            text="Nova Categoria"
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

      <CrudTable<Categoria>
        loading={loading}
        items={pageResult.items}
        getRowKey={(c) => c.id}
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
            header: "Finalidade",
            mobileLabel: "Finalidade",
            render: (c) => finalidadeLabel(c.finalidade),
            className: "colSmall",
          },
        ]}
        emptyTitle="Nenhuma categoria encontrada"
        emptyText="Cadastre uma categoria para começar."
        emptyAction={
          <Button
            text="Cadastrar Categoria"
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

      <CategoriaFormModal
        open={modalOpen}
        saving={saving}
        value={form}
        setValue={setForm}
        onClose={fecharModal}
        onSave={salvar}
        errors={errorMessages}
      />
    </section>
  );
}
