import React from "react";
import "./CrudTableStyle.scss";
import type { PaginationProps } from "../Pagination/Pagination";
import Pagination from "../Pagination/Pagination";

export type CrudColumn<T> = {
  header: string;
  accessor?: keyof T;
  render?: (item: T) => React.ReactNode;

  className?: string;
  mobileLabel?: string;
  hideOnMobile?: boolean;
};

type Props<T> = {
  items: T[];
  loading?: boolean;
  totalLabel?: React.ReactNode;
  headerRight?: React.ReactNode;
  columns: CrudColumn<T>[];
  actionsHeader?: string;
  renderActions?: (item: T) => React.ReactNode;
  emptyTitle?: string;
  emptyText?: string;
  emptyAction?: React.ReactNode;
  getRowKey: (item: T) => string | number;
  pagination?: PaginationProps;
};

function renderCell<T>(col: CrudColumn<T>, item: T) {
  if (col.render) return col.render(item);
  if (col.accessor) return String(item[col.accessor] ?? "");
  return "";
}

export default function CrudTable<T>({
  items,
  loading = false,
  totalLabel,
  headerRight,
  columns,
  actionsHeader = "Ações",
  renderActions,
  emptyTitle = "Nenhum registro encontrado",
  emptyText = "Não há dados para exibir.",
  emptyAction,
  getRowKey,
  pagination,
}: Props<T>) {
  const hasActions = typeof renderActions === "function";
  const mobileColumns = columns.filter((c) => !c.hideOnMobile);

  return (
    <div className="crudCard">
      <div className="crudCardHeader">
        <div className="crudHeaderLeft">{totalLabel}</div>
        <div className="crudHeaderRight">{headerRight}</div>
      </div>

      {loading ? (
        <div className="crudLoadingState">
          <span className="crudSpinner" />
          <span>Carregando...</span>
        </div>
      ) : items.length === 0 ? (
        <div className="crudEmptyState">
          <div className="crudEmptyTitle">{emptyTitle}</div>
          <div className="crudEmptyText">{emptyText}</div>
          {emptyAction ? (
            <div className="crudEmptyAction">{emptyAction}</div>
          ) : null}
        </div>
      ) : (
        <>
          {/* DESKTOP: tabela */}
          <div className="crudTableWrap">
            <table className="crudTable">
              <thead>
                <tr>
                  {columns.map((col, i) => (
                    <th key={i} className={col.className}>
                      {col.header}
                    </th>
                  ))}
                  {hasActions ? (
                    <th className="crudColActions">{actionsHeader}</th>
                  ) : null}
                </tr>
              </thead>

              <tbody>
                {items.map((item) => (
                  <tr key={getRowKey(item)}>
                    {columns.map((col, i) => (
                      <td key={i} className={col.className}>
                        {renderCell(col, item)}
                      </td>
                    ))}

                    {hasActions ? (
                      <td className="crudColActions">
                        <div className="crudRowActions">
                          {renderActions!(item)}
                        </div>
                      </td>
                    ) : null}
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          {/* MOBILE: cards */}
          <div className="crudMobileList">
            {items.map((item) => (
              <div key={getRowKey(item)} className="crudMobileCard">
                <div className="crudMobileGrid">
                  {mobileColumns.map((col, i) => (
                    <div key={i} className="crudMobileRow">
                      <div className="crudMobileLabel">
                        {col.mobileLabel ?? col.header}
                      </div>
                      <div className="crudMobileValue">
                        {renderCell(col, item)}
                      </div>
                    </div>
                  ))}
                </div>

                {hasActions ? (
                  <div className="crudMobileActions">
                    {renderActions!(item)}
                  </div>
                ) : null}
              </div>
            ))}
          </div>
        </>
      )}

      {/* ✅ Rodapé: paginação */}
      {pagination ? (
        <div className="crudPagination">
          <Pagination
            {...pagination}
            disabled={pagination.disabled || loading}
          />
        </div>
      ) : null}
    </div>
  );
}
