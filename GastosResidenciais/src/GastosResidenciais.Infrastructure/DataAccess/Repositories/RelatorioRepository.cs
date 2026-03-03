using GastosResidenciais.Domain.DTOs;
using GastosResidenciais.Domain.Enums;
using GastosResidenciais.Domain.Repositories.Relatorios;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Infrastructure.DataAccess.Repositories
{
    internal class RelatorioRepository : IRelatorioReadOnlyRepository
    {
        private readonly GastosResidenciaisDbContext _dbContext;

        public RelatorioRepository(GastosResidenciaisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PessoaTotaisDTO>> GetPessoasTotaisAsync()
        {
            var transacoes = _dbContext.Transacoes
                .AsNoTracking()
                .GroupBy(t => t.PessoaId)
                .Select(g => new
                {
                    PessoaId = (long?)g.Key,

                    TotalReceitas = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Receita)
                                     .Sum(t => (decimal?)t.Valor),

                    TotalDespesas = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Despesa)
                                     .Sum(t => (decimal?)t.Valor),
                });

            var query =
                from pessoa in _dbContext.Pessoas.AsNoTracking()
                join t in transacoes on (long?)pessoa.Id equals t.PessoaId into pt
                from t in pt.DefaultIfEmpty()
                select new PessoaTotaisDTO
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    Idade = pessoa.Idade,
                    TotalReceitas = t == null ? 0m : (t.TotalReceitas ?? 0m),
                    TotalDespesas = t == null ? 0m : (t.TotalDespesas ?? 0m),
                };

            return await query
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<PageResultDTO<PessoaTotaisDTO>> GetPessoasTotaisPaginadoAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var transacoes = _dbContext.Transacoes
                .AsNoTracking()
                .GroupBy(t => t.PessoaId)
                .Select(g => new
                {
                    PessoaId = (long?)g.Key,

                    TotalReceitas = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Receita)
                                     .Sum(t => (decimal?)t.Valor),

                    TotalDespesas = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Despesa)
                                     .Sum(t => (decimal?)t.Valor),
                });

            var query =
                from pessoa in _dbContext.Pessoas.AsNoTracking()
                join t in transacoes on (long?)pessoa.Id equals t.PessoaId into pt
                from t in pt.DefaultIfEmpty()
                select new PessoaTotaisDTO
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    Idade = pessoa.Idade,
                    TotalReceitas = t == null ? 0m : (t.TotalReceitas ?? 0m),
                    TotalDespesas = t == null ? 0m : (t.TotalDespesas ?? 0m),
                };

            var totalItems = await _dbContext.Pessoas.AsNoTracking().CountAsync();

            var items = await query
                .OrderBy(x => x.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultDTO<PessoaTotaisDTO>
            {
                Page = page,
                PageSize = items.Count,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items
            };
        }

        public async Task<TotaisGeraisDTO> GetTotaisGeraisPessoasAsync()
        {
            var res = await _dbContext.Transacoes
                .AsNoTracking()
                .GroupBy(_ => 1)
                .Select(g => new TotaisGeraisDTO
                {
                    TotalReceitasGeral = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Receita)
                                          .Sum(t => (decimal?)t.Valor) ?? 0m,
                    TotalDespesasGeral = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Despesa)
                                          .Sum(t => (decimal?)t.Valor) ?? 0m,
                })
                .FirstOrDefaultAsync();

            return res ?? new TotaisGeraisDTO { TotalReceitasGeral = 0m, TotalDespesasGeral = 0m };
        }

        public async Task<List<CategoriaTotaisDTO>> GetCategoriasTotaisAsync()
        {
            var transacoes = _dbContext.Transacoes
                .AsNoTracking()
                .GroupBy(t => t.CategoriaId)
                .Select(g => new
                {
                    CategoriaId = (long?)g.Key,

                    TotalReceitas = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Receita)
                                     .Sum(t => (decimal?)t.Valor),

                    TotalDespesas = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Despesa)
                                     .Sum(t => (decimal?)t.Valor)
                });

            var query =
                from categoria in _dbContext.Categorias.AsNoTracking()
                join t in transacoes on (long?)categoria.Id equals t.CategoriaId into ct
                from t in ct.DefaultIfEmpty()
                select new CategoriaTotaisDTO
                {
                    Id = categoria.Id,
                    Descricao = categoria.Descricao,
                    Finalidade = categoria.Finalidade,

                    TotalReceitas =
                        categoria.Finalidade == FinalidadeCategoriaEnum.Despesa
                            ? 0m
                            : (t == null ? 0m : (t.TotalReceitas ?? 0m)),

                    TotalDespesas =
                        categoria.Finalidade == FinalidadeCategoriaEnum.Receita
                            ? 0m
                            : (t == null ? 0m : (t.TotalDespesas ?? 0m))
                };

            return await query
                .OrderBy(x => x.Descricao)
                .ToListAsync();
        }

        public async Task<PageResultDTO<CategoriaTotaisDTO>> GetCategoriasTotaisPaginadoAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var transacoes = _dbContext.Transacoes
                .AsNoTracking()
                .GroupBy(t => t.CategoriaId)
                .Select(g => new
                {
                    CategoriaId = (long?)g.Key,

                    TotalReceitas = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Receita)
                                     .Sum(t => (decimal?)t.Valor),

                    TotalDespesas = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Despesa)
                                     .Sum(t => (decimal?)t.Valor)
                });

            var query =
                from categoria in _dbContext.Categorias.AsNoTracking()
                join t in transacoes on (long?)categoria.Id equals t.CategoriaId into ct
                from t in ct.DefaultIfEmpty()
                select new CategoriaTotaisDTO
                {
                    Id = categoria.Id,
                    Descricao = categoria.Descricao,
                    Finalidade = categoria.Finalidade,

                    TotalReceitas =
                        categoria.Finalidade == FinalidadeCategoriaEnum.Despesa
                            ? 0m
                            : (t == null ? 0m : (t.TotalReceitas ?? 0m)),

                    TotalDespesas =
                        categoria.Finalidade == FinalidadeCategoriaEnum.Receita
                            ? 0m
                            : (t == null ? 0m : (t.TotalDespesas ?? 0m))
                };

            var totalItems = await _dbContext.Categorias
                .AsNoTracking()
                .CountAsync();

            var items = await query
                .OrderBy(x => x.Descricao)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultDTO<CategoriaTotaisDTO>
            {
                Page = page,
                PageSize = items.Count,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items
            };
        }

        public async Task<TotaisGeraisDTO> GetTotaisGeraisCategoriasAsync()
        {
            var res = await _dbContext.Transacoes
                .AsNoTracking()
                .GroupBy(_ => 1)
                .Select(g => new TotaisGeraisDTO
                {
                    TotalReceitasGeral = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Receita)
                                          .Sum(t => (decimal?)t.Valor) ?? 0m,
                    TotalDespesasGeral = g.Where(t => t.TipoTransacao == TipoTransacaoEnum.Despesa)
                                          .Sum(t => (decimal?)t.Valor) ?? 0m,
                })
                .FirstOrDefaultAsync();

            return res ?? new TotaisGeraisDTO { TotalReceitasGeral = 0m, TotalDespesasGeral = 0m };
        }
    }
}
