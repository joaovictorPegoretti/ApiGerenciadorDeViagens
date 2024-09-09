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
    public class Controles : ControllerBase // Central de controle, onde fica todas as funções que o sistema mostra para o usuário
    {
        private readonly IInterface_Usuario _interfaceUsuario;
        private readonly IInterface_Passagem _interfacePassagem;
        private readonly IInterface_Viagem _interfaceViagem;
        public Controles(IInterface_Usuario interfaceUsuario, IInterface_Passagem interfacePassagem, IInterface_Viagem interfaceViagem) // Construtor dos controles
        {
            _interfaceUsuario = interfaceUsuario;
            _interfacePassagem = interfacePassagem;
            _interfaceViagem = interfaceViagem;
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

        [HttpGet("Listar todas as viagens")]
        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Viagem>>>> Listar_Todas_Viagens()
        {
            var ListarTodasViagens = await _interfaceViagem.Listar_Todas_Viagens();
            return Ok(ListarTodasViagens);
        }

        [HttpGet("Listar pela Origem, Destino, Data de Ida e Data de Volta")]
        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Viagem>>>> ListarViagensPeloDestino(string origem, string destino, string ida, string volta)
        {
            var ListarViagensPorODIV = await _interfaceViagem.ListarViagensPeloDestino(origem, destino, ida, volta);
            return Ok(ListarViagensPorODIV);
        }

        [HttpPost("Comprar passagens")]
        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Passagens>>>> ComprarPassagem(NovaPassagemDto novaPassagemDto)
        {
            var novaPassagem = await _interfacePassagem.ComprarPassagem(novaPassagemDto);
            return Ok(novaPassagem);
        }

        [HttpDelete("Cancelar passagens")]
        public async Task<ActionResult<Modelo_Resposta<List<Modelo_Passagens>>>> CancelarPassagem(Guid IdPassagem)
        {
            var apagarPassagem = await _interfacePassagem.CancelarPassagem(IdPassagem);
            return Ok(apagarPassagem);
        }

        [HttpPut("Realizar Checkin")]
        public async Task<ActionResult<Modelo_Resposta<Modelo_Passagens>>> RealizarCheckin(Guid IdPassagem)
        {
            var FazerChekin = await _interfacePassagem.RealizarCheckin(IdPassagem);
            return Ok(FazerChekin);
        }
        [HttpGet("Fazer um relatório de Ocupação")]
        public async Task<ActionResult<Modelo_Resposta<Modelo_Viagem>>> RelatorioOcupacao()
        {
            var FazerRelatorio = await _interfaceViagem.RelatorioOcupacao();
            return Ok(FazerRelatorio);
        }

        [HttpGet("Fazer um relatório de Vendas")]
        public async Task<ActionResult<Modelo_Resposta<Modelo_Viagem>>> RelatorioVenda()
        {
            var FazerRelatorio = await _interfaceViagem.RelatorioVendas();
            return Ok(FazerRelatorio);
        }

    }
}
