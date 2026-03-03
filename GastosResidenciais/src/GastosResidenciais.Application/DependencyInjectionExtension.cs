using FluentValidation;
using GastosResidenciais.Application.AutoMapper;
using GastosResidenciais.Application.UseCases.Categorias.Count;
using GastosResidenciais.Application.UseCases.Categorias.GetAll;
using GastosResidenciais.Application.UseCases.Categorias.GetById;
using GastosResidenciais.Application.UseCases.Categorias.Register;
using GastosResidenciais.Application.UseCases.Pessoas.Count;
using GastosResidenciais.Application.UseCases.Pessoas.Delete;
using GastosResidenciais.Application.UseCases.Pessoas.GetAll;
using GastosResidenciais.Application.UseCases.Pessoas.GetById;
using GastosResidenciais.Application.UseCases.Pessoas.Register;
using GastosResidenciais.Application.UseCases.Pessoas.Update;
using GastosResidenciais.Application.UseCases.Relatorios.GetTotaisCategorias;
using GastosResidenciais.Application.UseCases.Relatorios.GetTotaisPessoas;
using GastosResidenciais.Application.UseCases.Relatorios.Pdfs.PdfTotaisCategorias;
using GastosResidenciais.Application.UseCases.Relatorios.Pdfs.PdfTotaisPessoas;
using GastosResidenciais.Application.UseCases.Transacoes;
using GastosResidenciais.Application.UseCases.Transacoes.Count;
using GastosResidenciais.Application.UseCases.Transacoes.GetAll;
using GastosResidenciais.Application.UseCases.Transacoes.GetById;
using GastosResidenciais.Application.UseCases.Transacoes.Register;
using GastosResidenciais.Communication.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace GastosResidenciais.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }

        private static void AddUseCases(IServiceCollection services)
        {
            // Pessoa
            services.AddScoped<IRegisterPessoaUseCase, RegisterPessoaUseCase>();
            services.AddScoped<IGetAllPessoasUseCase, GetAllPessoasUseCase>();
            services.AddScoped<IGetPessoaByIdUseCase, GetPessoaByIdUseCase>();
            services.AddScoped<IDeletePessoaUseCase, DeletePessoaUseCase>();
            services.AddScoped<IUpdatePessoaUseCase, UpdatePessoaUseCase>();
            services.AddScoped<IGetPessoasCountUseCase, GetPessoasCountUseCase>();

            // Categoria
            services.AddScoped<IRegisterCategoriaUseCase, RegisterCategoriaUseCase>();
            services.AddScoped<IGetAllCategoriasUseCase, GetAllCategoriasUseCase>();
            services.AddScoped<IGetCategoriaByIdUseCase, GetCategoriaByIdUseCase>();
            services.AddScoped<IGetCategoriasCountUseCase, GetCategoriasCountUseCase>();

            // Transacao
            services.AddScoped<IRegisterTransacaoUseCase, RegisterTransacaoUseCase>();
            services.AddScoped<IGetAllTransacoesUseCase, GetAllTransacoesUseCase>();
            services.AddScoped<IGetTransacaoByIdUseCase, GetTransacaoByIdUseCase>();
            services.AddScoped<IGetTransacoesCountUseCase, GetTransacoesCountUseCase>();
            services.AddScoped<IValidator<RequestTransacaoJson>, TransacaoValidator>();

            // Relatorio
            services.AddScoped<IGetTotaisPessoasRelatorioUseCase, GetTotaisPessoasRelatorioUseCase>();
            services.AddScoped<IGetTotaisCategoriasRelatorioUseCase, GetTotaisCategoriasRelatorioUseCase>();
            services.AddScoped<IPdfTotaisPessoasUseCase, PdfTotaisPessoasUseCase>();
            services.AddScoped<IPdfTotaisCategoriasUseCase, PdfTotaisCategoriasUseCase>();
        }
    }
}
