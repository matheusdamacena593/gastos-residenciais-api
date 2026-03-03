import { useEffect, useState } from "react";
import Modal from "@/_components/Modal/Modal";
import Input from "@/_components/Input/Input";
import Button from "@/_components/Button/Button";
import Feedback from "@/_components/Feedback/Feedback";
import Select from "@/_components/Select/Select";
import type { CategoriaCreateRequest } from "@/_interfaces/ICategoria";
import { FinalidadeCategoriaEnum } from "@/_interfaces/ICategoria";

type Props = {
  open: boolean;
  saving?: boolean;

  value: CategoriaCreateRequest;
  setValue: React.Dispatch<React.SetStateAction<CategoriaCreateRequest>>;

  onClose: () => void;
  onSave: () => void;

  errors?: string[];
};

export default function CategoriaFormModal({
  open,
  saving = false,
  value,
  setValue,
  onClose,
  onSave,
  errors = [],
}: Props) {
  const [touched, setTouched] = useState({
    descricao: false,
    finalidade: false,
  });

  useEffect(() => {
    if (!open) return;
    setTouched({ descricao: false, finalidade: false });
  }, [open]);

  if (!open) return null;

  const descricaoInvalida = touched.descricao && !value.descricao.trim();

  const finalidadeInvalida =
    touched.finalidade && !Number.isFinite(Number(value.finalidade));

  return (
    <Modal onClose={onClose} title="Nova categoria" closeDisabled={saving}>
      <form
        onSubmit={(e) => {
          e.preventDefault();
          setTouched({ descricao: true, finalidade: true });
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
              placeholder="Descrição"
              value={value.descricao}
              onChange={(e) =>
                setValue((s) => ({ ...s, descricao: e.target.value }))
              }
              onBlur={() => setTouched((t) => ({ ...t, descricao: true }))}
              className={descricaoInvalida ? "inputError" : ""}
            />
            {descricaoInvalida ? (
              <small className="fieldError">Descrição é obrigatória.</small>
            ) : null}
          </div>

          <Select<number>
            label="Finalidade"
            value={value.finalidade}
            options={[
              { value: FinalidadeCategoriaEnum.Despesa, label: "Despesa" },
              { value: FinalidadeCategoriaEnum.Receita, label: "Receita" },
              { value: FinalidadeCategoriaEnum.Ambas, label: "Ambas" },
            ]}
            onChange={(v) => setValue((s) => ({ ...s, finalidade: v as any }))}
            onBlur={() => setTouched((t) => ({ ...t, finalidade: true }))}
            error={finalidadeInvalida ? "Finalidade inválida." : undefined}
          />
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
