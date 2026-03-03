import axios from "axios";
import type { ErrorResponse } from "@/_interfaces/IErrorResponse";

export function extractErrorMessages(err: unknown): string[] {
  if (!axios.isAxiosError(err)) return ["Erro inesperado."];

  const data = err.response?.data as Partial<ErrorResponse> | undefined;
  if (data?.errorMessages?.length) return data.errorMessages;

  if (err.message) return [err.message];
  return ["Erro na requisição."];
}

export type CountResponse = number | { total: number } | string;

export function parseCount(data: CountResponse): number {
  if (typeof data === "number") return data;

  if (typeof data === "string") {
    const parsed = Number(data);
    if (Number.isFinite(parsed)) return parsed;
  }

  if (data && typeof data === "object") {
    if ("total" in data && typeof (data as any).total === "number") return (data as any).total;
  }

  throw new Error("Resposta inesperada do endpoint de count.");
}
