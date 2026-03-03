import React from "react";
import { ChevronLeft, ChevronRight } from "lucide-react";

import Button from "@/_components/Button/Button";
import Select, { type SelectOption } from "@/_components/Select/Select";

import styles from "./PaginationStyle.module.scss";

const DOTS = "…";
const DEFAULT_PAGE_SIZES = [10, 20, 30, 40, 50] as const;

type PaginationItem = number | typeof DOTS;

function getPaginationRange(params: {
  currentPage: number;
  totalPages: number;
  siblingCount?: number;
}): PaginationItem[] {
  const { currentPage, totalPages, siblingCount = 1 } = params;

  const totalPageNumbers = siblingCount * 2 + 5;

  if (totalPages <= totalPageNumbers) {
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }

  const leftSiblingIndex = Math.max(currentPage - siblingCount, 1);
  const rightSiblingIndex = Math.min(currentPage + siblingCount, totalPages);

  const shouldShowLeftDots = leftSiblingIndex > 2;
  const shouldShowRightDots = rightSiblingIndex < totalPages - 1;

  const firstPageIndex = 1;
  const lastPageIndex = totalPages;

  if (!shouldShowLeftDots && shouldShowRightDots) {
    const leftItemCount = 3 + siblingCount * 2;
    const leftRange = Array.from({ length: leftItemCount }, (_, i) => i + 1);
    return [...leftRange, DOTS, lastPageIndex];
  }

  if (shouldShowLeftDots && !shouldShowRightDots) {
    const rightItemCount = 3 + siblingCount * 2;
    const start = totalPages - rightItemCount + 1;
    const rightRange = Array.from(
      { length: rightItemCount },
      (_, i) => start + i,
    );
    return [firstPageIndex, DOTS, ...rightRange];
  }

  const middleRange = Array.from(
    { length: rightSiblingIndex - leftSiblingIndex + 1 },
    (_, i) => leftSiblingIndex + i,
  );

  return [firstPageIndex, DOTS, ...middleRange, DOTS, lastPageIndex];
}

export type PaginationProps = {
  page: number;
  totalPages: number;
  totalItems: number; // ✅ NOVO
  onPageChange: (page: number) => void;

  pageSize: number;
  onPageSizeChange: (pageSize: number) => void;
  pageSizeOptions?: readonly number[];

  disabled?: boolean;
  siblingCount?: number;
  className?: string;
};

export default function Pagination({
  page,
  totalPages,
  totalItems,
  onPageChange,
  pageSize,
  onPageSizeChange,
  pageSizeOptions = DEFAULT_PAGE_SIZES,
  disabled = false,
  siblingCount = 1,
  className,
}: PaginationProps) {
  const safeTotalPages = Math.max(1, totalPages);
  const safePage = Math.min(Math.max(1, page), safeTotalPages);

  const range = React.useMemo(
    () =>
      getPaginationRange({
        currentPage: safePage,
        totalPages: safeTotalPages,
        siblingCount,
      }),
    [safePage, safeTotalPages, siblingCount],
  );

  const canGoPrev = safePage > 1;
  const canGoNext = safePage < safeTotalPages;
  const showPager = safeTotalPages > 1;

  const options: SelectOption<number>[] = (
    pageSizeOptions ?? DEFAULT_PAGE_SIZES
  ).map((n) => ({
    value: n,
    label: String(n),
  }));

  const from = totalItems === 0 ? 0 : (safePage - 1) * pageSize + 1;
  const to = totalItems === 0 ? 0 : Math.min(safePage * pageSize, totalItems);

  return (
    <div className={[styles.pagination, className ?? ""].join(" ")}>
      <div className={styles.info}>
        <div className={styles.range}>
          Mostrando <strong>{from}</strong>–<strong>{to}</strong> de{" "}
          <strong>{totalItems} </strong>itens
        </div>
      </div>

      <div className={styles.pageSize}>
        <span className={styles.pageSizeLabel}>Itens por página</span>

        <Select<number>
          value={pageSize}
          options={options}
          onChange={(v) => onPageSizeChange(v)}
          disabled={disabled}
          placeholder=""
          className={styles.pageSizeSelect}
        />
      </div>

      {showPager ? (
        <div className={styles.pager}>
          <Button
            text="Página anterior"
            title="Página anterior"
            icon={<ChevronLeft size={18} />}
            iconOnly
            size="sm"
            variant="ghost"
            className={styles.iconButton}
            disabled={disabled || !canGoPrev}
            onClick={() => onPageChange(safePage - 1)}
          />

          <div className={styles.numbers}>
            {range.map((item, idx) => {
              if (item === DOTS) {
                return (
                  <span key={`dots-${idx}`} className={styles.dots}>
                    {DOTS}
                  </span>
                );
              }

              const isActive = item === safePage;

              return (
                <Button
                  key={item}
                  text={String(item)}
                  title={`Ir para página ${item}`}
                  size="sm"
                  variant={isActive ? "primary" : "ghost"}
                  className={styles.pageButton}
                  disabled={disabled}
                  onClick={() => onPageChange(item)}
                />
              );
            })}
          </div>

          <Button
            text="Próxima página"
            title="Próxima página"
            icon={<ChevronRight size={18} />}
            iconOnly
            size="sm"
            variant="ghost"
            className={styles.iconButton}
            disabled={disabled || !canGoNext}
            onClick={() => onPageChange(safePage + 1)}
          />
        </div>
      ) : null}
    </div>
  );
}
