import type { FinalidadeCategoriaEnum } from "./ICategoria";
import type { PageResult } from "./IPageResult";

export interface RelatorioTotaisPessoaItem {
  id: number;
  nome: string;
  idade: number;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export type RelatorioTotaisPessoasResponse = {
  pessoas: PageResult<RelatorioTotaisPessoaItem>;
  totalReceitasGeral: number;
  totalDespesasGeral: number;
  saldoGeral: number;
};

export interface RelatorioTotaisCategoriaItem {
  id: number;
  descricao: string;
  finalidade: FinalidadeCategoriaEnum;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface RelatorioTotaisCategoriasResponse {
  categorias: PageResult<RelatorioTotaisCategoriaItem>;
  totalReceitasGeral: number;
  totalDespesasGeral: number;
  saldoGeral: number;
}
