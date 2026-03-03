import apiService from "../ApiService";
import type {
  Transacao,
  TransacaoCreateRequest,
  TransacaoDetails,
} from "@/_interfaces/ITransacao";
import type { PageResult } from "@/_interfaces/IPageResult";
import {
  extractErrorMessages,
  parseCount,
  type CountResponse,
} from "@/_utils/apiError";

export const transacoesApiService = {
  async listar(params?: {
    page?: number;
    pageSize?: number;
  }): Promise<PageResult<Transacao>> {
    const page = params?.page ?? 1;
    const pageSize = params?.pageSize ?? 10;

    try {
      const res = await apiService.get<PageResult<Transacao>>("/transacoes", {
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

  async buscarPorId(id: number): Promise<TransacaoDetails> {
    try {
      const res = await apiService.get<TransacaoDetails>(`/transacoes/${id}`);
      return res.data;
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async criar(payload: TransacaoCreateRequest): Promise<Transacao> {
    try {
      const res = await apiService.post<Transacao>("/transacoes", payload);
      return res.data;
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async count(): Promise<number> {
    try {
      const res = await apiService.get<CountResponse>("/transacoes/count");
      return parseCount(res.data);
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },
};
