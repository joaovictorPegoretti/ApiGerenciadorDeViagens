using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;

namespace ApiGerenciadorDeViagens.Servicos.Usuario
{
    public interface IInterface_Usuario
    {
        Task<Modelo_Resposta<List<Modelo_Usuario>>> CadastrarUsuario(NovoUsuaripDto novoUsuaripDto);
        Task<Modelo_Resposta<List<Modelo_Usuario>>> ListarUsarios();
       
    }
}
