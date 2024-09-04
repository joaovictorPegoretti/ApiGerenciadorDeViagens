using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViagensApi.Dto;
using ViagensApi.Modelos;
using ViagensApi.Servicos.Usuario;
using ViagensApi.Servicos.Viagem;

namespace ViagensApi.Controles
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controle_Viagem : ControllerBase
    {
        private readonly IInterface_Viagem _interfaceViagem;
        public Controle_Viagem(IInterface_Viagem interfaceViagem)
        {
            _interfaceViagem = interfaceViagem;
        }

        [HttpPost("Criar nova viagem")]

        public async Task<ActionResult<Modelo_Resposta<Modelo_Viagem>>> NovaViagem(NovaViagemDto novaViagemDto)
        {
            var viagemnova = await _interfaceViagem.NovaViagem(novaViagemDto);
            return Ok(viagemnova);
        }

        [HttpGet("Listar todas as viagens")]

        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Viagem>>>> Listar_Todas_Viagens()
        {
            var Viagens = await _interfaceViagem.Listar_Todas_Viagens();
            return Ok(Viagens);
        }

        [HttpGet("Listar Viagens por origem e destino")] //Implementando a lista de viagens de origem e destino
        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Viagem>>>> ListarViagemPeloDestino(string origem, string destino, string ida, string volta)
        {
            var viagens = await _interfaceViagem.ListarViagensPeloDestino(origem, destino, ida, volta);
            return Ok(viagens);
        }

    }
}
