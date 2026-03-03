import { useEffect, useState } from "react";
import Modal from "../../../_components/Modal/Modal";
import Input from "../../../_components/Input/Input";
import Button from "../../../_components/Button/Button";
import Feedback from "../../../_components/Feedback/Feedback";
import type { Pessoa, PessoaCreateRequest } from "../../../_interfaces/IPessoa";

type ModalMode = "create" | "edit";

type Props = {
  open: boolean;
  mode: ModalMode;
  saving?: boolean;

  value: PessoaCreateRequest;
  setValue: React.Dispatch<React.SetStateAction<PessoaCreateRequest>>;

  editing?: Pessoa | null;

  onClose: () => void;
  onSave: () => void;

  errors?: string[];
};

export default function PessoaFormModal({
  open,
  mode,
  saving = false,
  value,
  setValue,
  editing,
  onClose,
  onSave,
  errors = [],
}: Props) {
  const [touched, setTouched] = useState({ nome: false, idade: false });

  useEffect(() => {
    if (!open) return;
    setTouched({ nome: false, idade: false });
  }, [open, mode, editing]);

  if (!open) return null;

  const nomeInvalido = touched.nome && !value.nome.trim();
  const idadeNumber = Number(value.idade);
  const idadeInvalida =
    touched.idade && (!Number.isFinite(idadeNumber) || idadeNumber < 0);

  const title = mode === "create" ? "Nova pessoa" : "Editar pessoa";

  return (
    <Modal onClose={onClose} title={title} closeDisabled={saving}>
      {/* ✅ FORM (recomendado) */}
      <form
        onSubmit={(e) => {
          e.preventDefault();
          setTouched({ nome: true, idade: true });
          onSave();
        }}
      >
        {errors.length > 0 ? (
          <div style={{ marginBottom: 10 }}>
            <Feedback
              text={errors[0]}
              color="#dc3545"
              textColor="#fff"
              width={"100%"}
              height={44}
            />
          </div>
        ) : null}

        <div style={{ display: "grid", gap: 10 }}>
          <div>
            <Input
              placeholder="Nome"
              value={value.nome}
              onChange={(e) =>
                setValue((s) => ({ ...s, nome: e.target.value }))
              }
              onBlur={() => setTouched((t) => ({ ...t, nome: true }))}
              className={nomeInvalido ? "inputError" : ""}
            />
            {nomeInvalido ? (
              <small className="fieldError">Nome é obrigatório.</small>
            ) : null}
          </div>

          <div>
            <Input
              placeholder="Idade"
              type="number"
              min={0}
              value={value.idade}
              onChange={(e) =>
                setValue((s) => ({ ...s, idade: Number(e.target.value) }))
              }
              onBlur={() => setTouched((t) => ({ ...t, idade: true }))}
              className={idadeInvalida ? "inputError" : ""}
            />
            {idadeInvalida ? (
              <small className="fieldError">
                Idade deve ser maior ou igual a 0.
              </small>
            ) : null}
          </div>
        </div>

        <div
          style={{
            display: "flex",
            justifyContent: "flex-end",
            gap: 10,
            marginTop: 14,
          }}
        >
          <Button
            text="Cancelar"
            variant="ghost"
            onClick={onClose}
            disabled={saving}
          />
          <Button
            text={saving ? "Salvando..." : "Salvar"}
            type="submit"
            variant="primary"
            disabled={saving}
          />
        </div>
      </form>
    </Modal>
  );
}
