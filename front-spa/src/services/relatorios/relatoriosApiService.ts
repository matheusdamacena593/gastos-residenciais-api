import apiService from "../ApiService";
import { extractErrorMessages } from "@/_utils/apiError";
import type {
  RelatorioTotaisPessoasResponse,
  RelatorioTotaisCategoriasResponse,
} from "@/_interfaces/IRelatorios";

type PdfDownload = { blob: Blob; filename: string };

function getFilenameFromContentDisposition(
  contentDisposition?: string,
  fallback = "relatorio.pdf",
): string {
  if (!contentDisposition) return fallback;

  const matchStar = contentDisposition.match(
    /filename\*\s*=\s*UTF-8''([^;]+)/i,
  );
  if (matchStar?.[1]) return decodeURIComponent(matchStar[1].trim());

  const match = contentDisposition.match(/filename\s*=\s*"?([^"]+)"?/i);
  if (match?.[1]) return match[1].trim();

  return fallback;
}

export const relatoriosApiService = {
  async totaisPessoas(
    page = 1,
    pageSize = 10,
  ): Promise<RelatorioTotaisPessoasResponse | null> {
    try {
      const res = await apiService.get<RelatorioTotaisPessoasResponse>(
        "/relatorios/totais-pessoas",
        { params: { page, pageSize } },
      );

      if (res.status === 204) return null;
      return res.data;
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async totaisCategorias(
    page = 1,
    pageSize = 10,
  ): Promise<RelatorioTotaisCategoriasResponse | null> {
    try {
      const res = await apiService.get<RelatorioTotaisCategoriasResponse>(
        "/relatorios/totais-categorias",
        { params: { page, pageSize } },
      );

      if (res.status === 204) return null;
      return res.data;
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async pdfTotaisPessoas(): Promise<PdfDownload | null> {
    try {
      const res = await apiService.get<Blob>("/relatorios/pdf-totais-pessoas", {
        responseType: "blob",
      });

      if (res.status === 204) return null;

      const filename = getFilenameFromContentDisposition(
        res.headers?.["content-disposition"],
        "relatorio-pessoas.pdf",
      );

      return { blob: res.data, filename };
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },

  async pdfTotaisCategorias(): Promise<PdfDownload | null> {
    try {
      const res = await apiService.get<Blob>(
        "/relatorios/pdf-totais-categorias",
        {
          responseType: "blob",
        },
      );

      if (res.status === 204) return null;

      const filename = getFilenameFromContentDisposition(
        res.headers?.["content-disposition"],
        "relatorio-categorias.pdf",
      );

      return { blob: res.data, filename };
    } catch (err) {
      throw extractErrorMessages(err);
    }
  },
};
