using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;
using ApiGerenciadorDeViagens.Servicos.Passagens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViagensApi.Modelos;
using ViagensApi.Servicos.Usuario;

namespace ViagensApi.Controles
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controle_Usuario : ControllerBase
    {
        private readonly IInterface_Usuario _interfaceUsuario;
        private readonly IInterface_Passagem _interfacePassagem;
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

        [HttpGet("Listar Usuarios")]
        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Usuario>>>> ListarUsuario()
        {
            var usuarios = await _interfaceUsuario.ListarUsarios();
            return Ok(usuarios);
        }

        [HttpPost("Comprar passagens")]
        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Passagens>>>> ComprarPassagem(NovaPassagemDto novaPassagemDto)
        {
            var novaPassagem = await _interfacePassagem.ComprarPassagem(novaPassagemDto);
            return Ok(novaPassagem);
        }

        [HttpDelete("Excluir Passagens")]

    }
}
