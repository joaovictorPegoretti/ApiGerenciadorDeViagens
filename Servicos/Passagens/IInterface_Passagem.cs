using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;
using ViagensApi.Modelos;

namespace ApiGerenciadorDeViagens.Servicos.Passagens
{
    public interface IInterface_Passagem
    {

        Task<Modelo_Resposta<List<Modelo_Passagens>>> ComprarPassagem(NovaPassagemDto novaPassagemDto);

    }
}
