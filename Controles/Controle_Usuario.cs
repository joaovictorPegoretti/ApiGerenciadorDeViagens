using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;
using ApiGerenciadorDeViagens.Servicos.Passagens;
using ApiGerenciadorDeViagens.Servicos.Usuario;
using ApiGerenciadorDeViagens.Servicos.Viagem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ViagensApi.Controles
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controle_Usuario : ControllerBase
    {
        private readonly IInterface_Usuario _interfaceUsuario;
        private readonly IInterface_Passagem _interfacePassagem;
        private readonly IInterface_Viagem _interfaceViagem;
        public Controle_Usuario(IInterface_Usuario interfaceUsuario, IInterface_Passagem interfacePassagem)
        {
            _interfaceUsuario = interfaceUsuario;
            _interfacePassagem = interfacePassagem;
        }

        [HttpPost("Cadastrar usuário")]
        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Usuario>>>> CadastrarUsuario(NovoUsuaripDto novoUsuaripDto)
        {
            var novoUsuario = await _interfaceUsuario.CadastrarUsuario(novoUsuaripDto);
            return Ok(novoUsuario);
        }

        [HttpPost("Criar nova viagem")]

        public async Task<ActionResult<Modelo_Resposta<Modelo_Viagem>>> NovaViagem(NovaViagemDto novaViagemDto)
        {
            var viagemnova = await _interfaceViagem.NovaViagem(novaViagemDto);
            return Ok(viagemnova);
        }

        [HttpPost("Comprar passagens")]
        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Passagens>>>> ComprarPassagem(NovaPassagemDto novaPassagemDto)
        {
            var novaPassagem = await _interfacePassagem.ComprarPassagem(novaPassagemDto);
            return Ok(novaPassagem);
        }

        [HttpGet("Listar todas as viagens")]

        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Viagem>>>> Listar_Todas_Viagens()
        {
            var Viagens = await _interfaceViagem.Listar_Todas_Viagens();
            return Ok(Viagens);
        }

        //[HttpDelete("Excluir Passagens")]


    }
}
