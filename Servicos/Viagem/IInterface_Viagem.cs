using ViagensApi.Dto;
using ViagensApi.Modelos;

namespace ViagensApi.Servicos.Viagem
{
    public interface IInterface_Viagem
    {
        Task<Modelo_Resposta<List<Modelo_Viagem>>> NovaViagem(NovaViagemDto novaViagemDto);
        Task<Modelo_Resposta<List<Modelo_Viagem>>> Listar_Todas_Viagens();
        Task<Modelo_Resposta<List<Modelo_Viagem>>> ListarViagensPeloDestino(string origem, string destino, string ida, string volta);
        Task<Modelo_Resposta<Modelo_Viagem>> ComprarViagem(); 
        Task<Modelo_Resposta<Modelo_Viagem>> CancelarReserva();
    }
}
