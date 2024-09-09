using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;

namespace ApiGerenciadorDeViagens.Servicos.Viagem
{
    public interface IInterface_Viagem //Interface de modelo para as funções na classe de Servicos_Viagem
    {
        Task<Modelo_Resposta<List<Modelo_Viagem>>> NovaViagem(NovaViagemDto novaViagemDto);
        Task<Modelo_Resposta<List<Modelo_Viagem>>> Listar_Todas_Viagens();
        Task<Modelo_Resposta<List<Modelo_Viagem>>> ListarViagensPeloDestino(string origem, string destino, string ida, string volta);

        Task<Modelo_Resposta<List<Modelo_Viagem>>> RelatorioOcupacao();

        Task<Modelo_Resposta<List<Modelo_Viagem>>> RelatorioVendas();
    }
}
