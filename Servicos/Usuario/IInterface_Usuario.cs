using ApiGerenciadorDeViagens.Dto;
using ViagensApi.Modelos;

namespace ViagensApi.Servicos.Usuario
{
    public interface IInterface_Usuario
    {
        Task<Modelo_Resposta<List<Modelo_Usuario>>> CadastrarUsuario(NovoUsuaripDto novoUsuaripDto);
        Task<Modelo_Resposta<List<Modelo_Usuario>>> ListarUsarios();
       
    }
}
