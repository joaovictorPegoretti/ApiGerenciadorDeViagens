using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;
namespace ApiGerenciadorDeViagens.Servicos.Passagens
{
    public interface IInterface_Passagem
    {

        Task<Modelo_Resposta<List<Modelo_Passagens>>> ComprarPassagem(NovaPassagemDto novaPassagemDto);

        Task<Modelo_Resposta<List<Modelo_Passagens>>> ListarPassagens(string IdUsuario);

        //Task<Modelo_Resposta<Modelo_Passagens>> CancelarPassagem(Guid IdPassagem);

    }
}
