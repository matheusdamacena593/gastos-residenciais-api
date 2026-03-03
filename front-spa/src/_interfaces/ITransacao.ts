export enum TipoTransacaoEnum {
  Despesa = 0,
  Receita = 1,
}

export interface Transacao {
  id: number;
  descricao: string;
  valor: number;
  tipoTransacao: TipoTransacaoEnum;
  categoriaId: number;
  pessoaId: number;
}

export interface TransacaoDetails extends Transacao {
  createdAt?: string;
  updatedAt?: string | null;
}

export interface TransacaoCreateRequest {
  descricao: string;
  valor: number;
  tipoTransacao: TipoTransacaoEnum;
  categoriaId: number;
  pessoaId: number;
}