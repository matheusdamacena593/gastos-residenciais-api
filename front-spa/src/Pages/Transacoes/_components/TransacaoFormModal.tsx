import { useEffect, useState } from "react";
import Modal from "@/_components/Modal/Modal";
import Input from "@/_components/Input/Input";
import Button from "@/_components/Button/Button";
import Feedback from "@/_components/Feedback/Feedback";
import Select from "@/_components/Select/Select";

import type { TransacaoCreateRequest } from "@/_interfaces/ITransacao";
import { TipoTransacaoEnum } from "@/_interfaces/ITransacao";
import type { Pessoa } from "@/_interfaces/IPessoa";
import type { Categoria } from "@/_interfaces/ICategoria";

type Props = {
  open: boolean;
  saving?: boolean;

  value: TransacaoCreateRequest;
  setValue: React.Dispatch<React.SetStateAction<TransacaoCreateRequest>>;

  pessoas: Pessoa[];
  categorias: Categoria[];

  onClose: () => void;
  onSave: () => void;

  errors?: string[];
};

export default function TransacaoFormModal({
  open,
  saving = false,
  value,
  setValue,
  pessoas,
  categorias,
  onClose,
  onSave,
  errors = [],
}: Props) {
  const [touched, setTouched] = useState({
    descricao: false,
    valor: false,
    tipo: false,
    pessoa: false,
    categoria: false,
  });

  useEffect(() => {
    if (!open) return;
    setTouched({
      descricao: false,
      valor: false,
      tipo: false,
      pessoa: false,
      categoria: false,
    });
  }, [open]);

  if (!open) return null;

  const descricaoInvalida = touched.descricao && !value.descricao.trim();
  const valorNum = Number(value.valor);
  const valorInvalido =
    touched.valor && (!Number.isFinite(valorNum) || valorNum <= 0);

  const pessoaInvalida =
    touched.pessoa &&
    (!Number.isFinite(Number(value.pessoaId)) || value.pessoaId <= 0);
  const categoriaInvalida =
    touched.categoria &&
    (!Number.isFinite(Number(value.categoriaId)) || value.categoriaId <= 0);

  return (
    <Modal onClose={onClose} title="Nova transação" closeDisabled={saving}>
      <form
        onSubmit={(e) => {
          e.preventDefault();
          setTouched({
            descricao: true,
            valor: true,
            tipo: true,
            pessoa: true,
            categoria: true,
          });
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

          <div>
            <Input
              placeholder="Valor"
              type="number"
              value={value.valor}
              onChange={(e) =>
                setValue((s) => ({ ...s, valor: Number(e.target.value) }))
              }
              onBlur={() => setTouched((t) => ({ ...t, valor: true }))}
              className={valorInvalido ? "inputError" : ""}
            />
            {valorInvalido ? (
              <small className="fieldError">Valor deve ser maior que 0.</small>
            ) : null}
          </div>

          <Select<number>
            label="Tipo de transação"
            value={value.tipoTransacao}
            options={[
              { value: TipoTransacaoEnum.Despesa, label: "Despesa" },
              { value: TipoTransacaoEnum.Receita, label: "Receita" },
            ]}
            onChange={(v) =>
              setValue((s) => ({ ...s, tipoTransacao: v as any }))
            }
            onBlur={() => setTouched((t) => ({ ...t, tipo: true }))}
          />

          <Select<number>
            label="Pessoa"
            value={value.pessoaId}
            options={pessoas.map((p) => ({ value: p.id, label: p.nome }))}
            onChange={(v) => setValue((s) => ({ ...s, pessoaId: v as any }))}
            onBlur={() => setTouched((t) => ({ ...t, pessoa: true }))}
            error={pessoaInvalida ? "Selecione uma pessoa válida." : undefined}
          />

          <Select<number>
            label="Categoria"
            value={value.categoriaId}
            options={categorias.map((c) => ({
              value: c.id,
              label: c.descricao,
            }))}
            onChange={(v) => setValue((s) => ({ ...s, categoriaId: v as any }))}
            onBlur={() => setTouched((t) => ({ ...t, categoria: true }))}
            error={
              categoriaInvalida ? "Selecione uma categoria válida." : undefined
            }
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
