using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;

namespace ApiGerenciadorDeViagens.Servicos.Usuario
{
    public interface IInterface_Usuario //Interface de modelo para as funções na classe de Servicos_Usuario
    {
        Task<Modelo_Resposta<List<Modelo_Usuario>>> CadastrarUsuario(NovoUsuaripDto novoUsuaripDto); 
       
    }
}
