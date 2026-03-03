export enum FinalidadeCategoriaEnum {
  Despesa = 0,
  Receita = 1,
  Ambas = 2,
}

export interface Categoria {
  id: number;
  descricao: string;
  finalidade: FinalidadeCategoriaEnum;
}

export interface CategoriaDetails extends Categoria {
  createdAt?: string;
  updatedAt?: string | null;
}

export interface CategoriaCreateRequest {
  descricao: string;
  finalidade: FinalidadeCategoriaEnum;
}