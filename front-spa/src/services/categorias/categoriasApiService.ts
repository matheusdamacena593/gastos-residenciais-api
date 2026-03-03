import apiService from "../ApiService";
import type {
  Categoria,
  CategoriaCreateRequest,
  CategoriaDetails,
} from "@/_interfaces/ICategoria";
import type { PageResult } from "@/_interfaces/IPageResult";
import {
  extractErrorMessages,
  parseCount,
  type CountResponse,
} from "@/_utils/apiError";

export const categoriasApiService = {
  async listar(params?: {
    page?: number;
    pageSize?: number;
  }): Promise<PageResult<Categoria>> {
    const page = params?.page ?? 1;
    const pageSize = params?.pageSize ?? 10;

    try {
      const res = await apiService.get<PageResult<Categoria>>("/categorias", {
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

  async buscarPorId(id: number): Promise<CategoriaDetails> {
    try {
      const res = await apiService.get<CategoriaDetails>(`/categorias/${id}`);
      return res.data;
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async criar(payload: CategoriaCreateRequest): Promise<Categoria> {
    try {
      const res = await apiService.post<Categoria>("/categorias", payload);
      return res.data;
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async count(): Promise<number> {
    try {
      const res = await apiService.get<CountResponse>("/categorias/count");
      return parseCount(res.data);
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },
};
