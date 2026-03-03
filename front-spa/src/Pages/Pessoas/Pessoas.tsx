import { useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus, faPencil, faTrash } from "@fortawesome/free-solid-svg-icons";
import "./PessoasStyle.scss";
import { pessoasApiService } from "@/services/pessoas/pessoasApiService";
import type { Pessoa, PessoaCreateRequest } from "@/_interfaces/IPessoa";
import type { PageResult } from "@/_interfaces/IPageResult";
import toast from "react-hot-toast";
import PessoaFormModal from "./_componentes/PessoaFormModal";
import ConfirmDeleteModal from "@/_components/ConfirmDeleteModal/ConfirmDeleteModal";
import Button from "@/_components/Button/Button";
import CrudTable from "@/_components/CrudTable/CrudTable";

type ModalMode = "create" | "edit";

const initialPageResult: PageResult<Pessoa> = {
  page: 1,
  pageSize: 10,
  totalItems: 0,
  totalPages: 1,
  items: [],
};

export default function Pessoas() {
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [pageResult, setPageResult] =
    useState<PageResult<Pessoa>>(initialPageResult);

  const [errorMessages, setErrorMessages] = useState<string[]>([]);

  const [modalOpen, setModalOpen] = useState(false);
  const [modalMode, setModalMode] = useState<ModalMode>("create");
  const [editing, setEditing] = useState<Pessoa | null>(null);

  const [form, setForm] = useState<PessoaCreateRequest>({ nome: "", idade: 0 });

  const [deleteOpen, setDeleteOpen] = useState(false);
  const [deleting, setDeleting] = useState<Pessoa | null>(null);

  async function carregar() {
    setLoading(true);
    setErrorMessages([]);

    try {
      const res = await pessoasApiService.listar({ page, pageSize });
      setPageResult(res);
    } catch (err: any) {
      const msgs = Array.isArray(err) ? err : ["Erro ao carregar pessoas."];
      setErrorMessages(msgs);
      toast.error(msgs[0] ?? "Erro ao carregar pessoas.");
      setPageResult((prev) => ({ ...prev, items: [] }));
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    const anyOpen = modalOpen || deleteOpen;

    if (anyOpen) {
      const prev = document.body.style.overflow;
      document.body.style.overflow = "hidden";

      const onKeyDown = (e: KeyboardEvent) => {
        if (e.key === "Escape") {
          if (modalOpen) fecharModal();
          if (deleteOpen) fecharDelete();
        }
      };

      window.addEventListener("keydown", onKeyDown);

      return () => {
        document.body.style.overflow = prev;
        window.removeEventListener("keydown", onKeyDown);
      };
    }
  }, [modalOpen, deleteOpen, saving]);

  useEffect(() => {
    carregar();
  }, [page, pageSize]);

  function abrirCriar() {
    setModalMode("create");
    setEditing(null);
    setForm({ nome: "", idade: 0 });
    setErrorMessages([]);
    setModalOpen(true);
  }

  function abrirEditar(p: Pessoa) {
    setModalMode("edit");
    setEditing(p);
    setForm({ nome: p.nome, idade: p.idade });
    setErrorMessages([]);
    setModalOpen(true);
  }

  function abrirExcluir(p: Pessoa) {
    setDeleting(p);
    setDeleteOpen(true);
  }

  function fecharModal() {
    if (saving) return;
    setModalOpen(false);
  }

  function fecharDelete() {
    if (saving) return;
    setDeleteOpen(false);
    setDeleting(null);
  }

  function validarForm(): string[] {
    const errs: string[] = [];
    if (!form.nome.trim()) errs.push("Nome é obrigatório.");
    if (!Number.isFinite(form.idade) || form.idade < 0)
      errs.push("Idade deve ser um número maior ou igual a 0.");
    return errs;
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
      if (modalMode === "create") {
        await pessoasApiService.criar({
          nome: form.nome.trim(),
          idade: Number(form.idade),
        });

        toast.success("Pessoa cadastrada com sucesso!");
        setModalOpen(false);

        if (page === 1) {
          await carregar();
        } else {
          setPage(1);
        }
      } else {
        if (!editing) return;

        await pessoasApiService.atualizar(editing.id, {
          nome: form.nome.trim(),
          idade: Number(form.idade),
        });

        toast.success("Pessoa atualizada com sucesso!");
        setModalOpen(false);

        await carregar();
      }
    } catch (err: any) {
      const msgs = Array.isArray(err) ? err : ["Erro ao salvar."];
      setErrorMessages(msgs);
      toast.error(msgs[0] ?? "Erro ao salvar.");
    } finally {
      setSaving(false);
    }
  }

  async function confirmarExcluir() {
    if (!deleting) return;

    setSaving(true);
    setErrorMessages([]);

    try {
      await pessoasApiService.excluir(deleting.id);
      toast.success("Pessoa excluída com sucesso!");
      fecharDelete();

      const willBeEmpty = pageResult.items.length === 1 && page > 1;

      if (willBeEmpty) {
        setPage((p) => p - 1);
      } else {
        await carregar();
      }
    } catch (err: any) {
      const msgs = Array.isArray(err) ? err : ["Erro ao excluir."];
      setErrorMessages(msgs);
      toast.error(msgs[0] ?? "Erro ao excluir.");
    } finally {
      setSaving(false);
    }
  }

  return (
    <section className="pessoasPage">
      <header className="pessoasHeader">
        <div>
          <h1 className="pessoasTitle">Pessoas</h1>
          <p className="pessoasSubtitle">
            Cadastre, edite e remova pessoas do sistema.
          </p>
        </div>

        <div className="pessoasHeaderActions">
          <Button
            text="Nova Pessoa"
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

      <div className="card">
        <CrudTable<Pessoa>
          loading={loading}
          items={pageResult.items}
          getRowKey={(p) => p.id}
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
            { header: "Nome", accessor: "nome", mobileLabel: "Nome" },
            {
              header: "Idade",
              accessor: "idade",
              mobileLabel: "Idade",
              className: "colSmall",
            },
          ]}
          renderActions={(p) => (
            <>
              <Button
                text="Editar"
                icon={<FontAwesomeIcon icon={faPencil} />}
                iconOnly
                variant="ghost"
                onClick={() => abrirEditar(p)}
              />

              <Button
                text="Excluir"
                icon={<FontAwesomeIcon icon={faTrash} />}
                iconOnly
                variant="danger"
                onClick={() => abrirExcluir(p)}
              />
            </>
          )}
          emptyTitle="Nenhuma pessoa encontrada"
          emptyText="Cadastre uma nova pessoa para começar."
          emptyAction={
            <Button
              text="Cadastrar Pessoa"
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
      </div>

      <PessoaFormModal
        open={modalOpen}
        mode={modalMode}
        saving={saving}
        value={form}
        setValue={setForm}
        editing={editing}
        onClose={fecharModal}
        onSave={salvar}
        errors={errorMessages}
      />

      <ConfirmDeleteModal
        open={deleteOpen}
        title="Excluir pessoa"
        loading={saving}
        onClose={fecharDelete}
        onConfirm={confirmarExcluir}
      >
        <p>
          Tem certeza que deseja excluir <strong>{deleting?.nome}</strong>?
        </p>
        <p className="muted">Essa ação não poderá ser desfeita.</p>
      </ConfirmDeleteModal>
    </section>
  );
}
