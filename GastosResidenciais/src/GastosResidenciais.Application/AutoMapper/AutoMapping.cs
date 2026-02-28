using AutoMapper;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses.Categorias;
using GastosResidenciais.Communication.Responses.Pessoas;
using GastosResidenciais.Communication.Responses.Relatorios;
using GastosResidenciais.Communication.Responses.Transacoes;
using GastosResidenciais.Domain.DTOs;
using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestPessoaJson, Pessoa>();
            CreateMap<RequestCategoriaJson, Categoria>();
            CreateMap<RequestTransacaoJson, Transacao>();
        }

        private void EntityToResponse()
        {
            CreateMap<Pessoa, ResponsePessoaJson>();
            CreateMap<Categoria, ResponseCategoriaJson>();
            CreateMap<Transacao, ResponseTransacaoJson>();
            CreateMap<PessoaTotaisDTO, ResponseTotalPessoaJson>()
               .ForMember(d => d.Saldo, opt => opt.MapFrom(s => s.TotalReceitas - s.TotalDespesas));
            CreateMap<CategoriaTotaisDTO, ResponseTotalCategoriaJson>()
                .ForMember(d => d.Saldo, opt => opt.MapFrom(s => s.TotalReceitas - s.TotalDespesas));
        }
    }
}
