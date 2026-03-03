import apiService from "../ApiService";
import type {
  Pessoa,
  PessoaCreateRequest,
  PessoaDetails,
} from "../../_interfaces/IPessoa";
import {
  extractErrorMessages,
  parseCount,
  type CountResponse,
} from "@/_utils/apiError";
import type { PageResult } from "@/_interfaces/IPageResult";

export const pessoasApiService = {
  async listar(params?: {
    page?: number;
    pageSize?: number;
  }): Promise<PageResult<Pessoa>> {
    const page = params?.page ?? 1;
    const pageSize = params?.pageSize ?? 10;

    try {
      const res = await apiService.get<PageResult<Pessoa>>("/pessoas", {
        params: { page, pageSize },
      });

      if (res.status === 204) {
        return { page, pageSize, totalItems: 0, totalPages: 1, items: [] };
      }

      const data = res.data;

      return {
        page: data?.page ?? page,
        pageSize: data?.pageSize ?? pageSize,
        totalItems: data?.totalItems ?? 0,
        totalPages: data?.totalPages ?? 1,
        items: data?.items ?? [],
      };
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async buscarPorId(id: number): Promise<PessoaDetails> {
    try {
      const res = await apiService.get<PessoaDetails>(`/pessoas/${id}`);
      return res.data;
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async criar(payload: PessoaCreateRequest): Promise<Pessoa> {
    try {
      const res = await apiService.post<Pessoa>("/pessoas", payload);
      return res.data;
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async atualizar(id: number, payload: PessoaCreateRequest): Promise<void> {
    try {
      await apiService.put(`/pessoas/${id}`, payload);
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async excluir(id: number): Promise<void> {
    try {
      await apiService.delete(`/pessoas/${id}`);
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async count(): Promise<number> {
    try {
      const res = await apiService.get<CountResponse>("/pessoas/count");
      return parseCount(res.data);
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },
};
