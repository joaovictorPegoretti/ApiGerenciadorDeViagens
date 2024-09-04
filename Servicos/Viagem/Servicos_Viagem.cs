using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ViagensApi.Data;
using ViagensApi.Dto;
using ViagensApi.Modelos;

namespace ViagensApi.Servicos.Viagem
{
    public class Servicos_Viagem : IInterface_Viagem
    {
        private readonly AppDbContext _context;
        public Servicos_Viagem(AppDbContext context)
        {
            _context = context;
        }

        public Task<Modelo_Resposta<Modelo_Viagem>> CancelarReserva()
        {
            throw new NotImplementedException();
        }

        public Task<Modelo_Resposta<Modelo_Viagem>> ComprarViagem()
        {
            throw new NotImplementedException();
        }

        public async Task<Modelo_Resposta<List<Modelo_Viagem>>> NovaViagem(NovaViagemDto novaViagemDto)
        {
            Modelo_Resposta<List<Modelo_Viagem>> Resposta = new Modelo_Resposta<List<Modelo_Viagem>>();

            try
            {
                var viagem = new Modelo_Viagem()
                {
                    Origem = novaViagemDto.Origem,
                    Destino = novaViagemDto.Destino,
                    dataIda = novaViagemDto.dataIda,
                    dataVolta = novaViagemDto.dataVolta,
                    horaIda = novaViagemDto.horaIda,
                    horaVolta = novaViagemDto.horaVolta,
                    Companhia = novaViagemDto.Companhia,
                    Precos = novaViagemDto.Precos,
                    Cadeiras = 150,
                    CadeirasOcupadas = novaViagemDto.CadeirasOcupadas,
                };

                _context.Add(viagem); //Adiciona a vaigem ao banco
                await _context.SaveChangesAsync();

                Resposta.Dados = await _context.Tabela_Viagem.ToListAsync();
                Resposta.Mensagem = "Viagem criada com sucesso";
                return Resposta;
            }
            catch (DbUpdateException ex)
            {

                Resposta.Mensagem = "Erro ao salvar as mudanças: " + ex.InnerException?.Message;
                Resposta.Status = false;
                return Resposta;

            }
        }

        public async Task<Modelo_Resposta<List<Modelo_Viagem>>> ListarViagensPeloDestino(string origem, string destino, string ida, string volta)
        {
            Modelo_Resposta<List<Modelo_Viagem>> Resposta = new Modelo_Resposta<List<Modelo_Viagem>>();
            try
            {
                var Listar_Viagens_PorODIV = await _context.Tabela_Viagem
                    .Where(viagens => viagens.Origem == origem && viagens.Destino == destino && viagens.dataIda == ida && viagens.dataVolta == volta)
                    .ToListAsync();
               

                if (Listar_Viagens_PorODIV == null)
                {
                    Resposta.Mensagem = "Nenhuma viagem com esses dados foi encontrada";
                    return Resposta;
                }
                Resposta.Dados = Listar_Viagens_PorODIV; //O nome ODIV é O = ORIGEM, D = DESTINO, I = IDA e V = VOLTA
                Resposta.Mensagem = $"Essas são todas as viagens com origem em {origem} indo às {ida} com destino a {destino}";
                return Resposta;

            }
            catch (Exception ex)
            {
                Resposta.Mensagem = ex.Message;
                Resposta.Status = false;
                return Resposta;
            }
        }

        public async Task<Modelo_Resposta<List<Modelo_Viagem>>> Listar_Todas_Viagens()
        {
            Modelo_Resposta<List<Modelo_Viagem>> Resposta = new Modelo_Resposta<List<Modelo_Viagem>>();
            try
            {
                var ListaViagens = await _context.Tabela_Viagem.ToListAsync();

                if (ListaViagens == null)
                {
                    Resposta.Mensagem = "Nenhuma viagem foi encontrada";
                    return Resposta;
                }
                Resposta.Dados = ListaViagens; //O nome ODIV é O = ORIGEM, D = DESTINO, I = IDA e V = VOLTA
                Resposta.Mensagem = "Essas são todas as viagens";
                return Resposta;

            }
            catch (Exception ex)
            {
                Resposta.Mensagem = ex.Message;
                Resposta.Status = false;
                return Resposta;
            }
        }
    }
}
