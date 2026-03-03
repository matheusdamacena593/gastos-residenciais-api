export interface Pessoa {
  id: number;
  nome: string;
  idade: number;
}

export interface PessoaDetails extends Pessoa {
  createdAt: string;
  updatedAt?: string | null;
}

export interface PessoaCreateRequest {
  nome: string;
  idade: number;
}
