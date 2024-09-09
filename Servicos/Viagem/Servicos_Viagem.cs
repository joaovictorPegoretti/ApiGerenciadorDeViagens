using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ApiGerenciadorDeViagens.Data;
using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;

namespace ApiGerenciadorDeViagens.Servicos.Viagem
{
    public class Servicos_Viagem : IInterface_Viagem
    {
        private readonly AppDbContext _context;
        public Servicos_Viagem(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Modelo_Resposta<List<Modelo_Viagem>>> NovaViagem(NovaViagemDto novaViagemDto)
        {
            Modelo_Resposta<List<Modelo_Viagem>> Resposta = new Modelo_Resposta<List<Modelo_Viagem>>();

            try
            { 
                var viagem = new Modelo_Viagem() //Cadastro da nova viagem
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
                };

                _context.Add(viagem); //Adicionando a viagem ao banco
                await _context.SaveChangesAsync(); //Salvando as alterações

                Resposta.Dados = await _context.Tabela_Viagem.ToListAsync(); //Listando todas as viagens
                Resposta.Mensagem = "Viagem criada com sucesso";
                return Resposta;
            }
            catch (DbUpdateException ex)
            {

                Resposta.Mensagem = "Erro ao salvar as mudanças: " + ex.InnerException?.Message;
                Resposta.Status = false;
                return Resposta;

            }
        } //Cadastro de uma nova Viagem

        public async Task<Modelo_Resposta<List<Modelo_Viagem>>> ListarViagensPeloDestino(string origem, string destino, string ida, string volta)
        {
            Modelo_Resposta<List<Modelo_Viagem>> Resposta = new Modelo_Resposta<List<Modelo_Viagem>>();
            try
            {
                var Listar_Viagens_PorODIV = await _context.Tabela_Viagem
                    .Where(viagens => viagens.Origem == origem && viagens.Destino == destino && viagens.dataIda == ida && viagens.dataVolta == volta)
                    .ToListAsync();// Verificando os dados
             

                if (Listar_Viagens_PorODIV == null)
                {
                    Resposta.Mensagem = "Nenhuma viagem com esses dados foi encontrada";
                    return Resposta;
                }

                //Listar_Viagens_PorODIV - O nome ODIV é O = ORIGEM, D = DESTINO, I = IDA e V = VOLTA
                Resposta.Dados = Listar_Viagens_PorODIV; 
                Resposta.Mensagem = $"Essas são todas as viagens com origem em {origem} indo às {ida} com destino a {destino}";
                return Resposta;

            }
            catch (Exception ex)
            {
                Resposta.Mensagem = ex.Message;
                Resposta.Status = false;
                return Resposta;
            }
        } //Listando viagens com base em valores indicados pelo usuário

        public async Task<Modelo_Resposta<List<Modelo_Viagem>>> Listar_Todas_Viagens()
        {
            Modelo_Resposta<List<Modelo_Viagem>> Resposta = new Modelo_Resposta<List<Modelo_Viagem>>();
            try
            {
                var ListaViagens = await _context.Tabela_Viagem.ToListAsync(); //Pegando todas as viagens no banco

                if (ListaViagens == null)
                {
                    Resposta.Mensagem = "Nenhuma viagem foi encontrada";
                    return Resposta;
                }
                Resposta.Dados = ListaViagens; //Mostrando todas a viagens
                Resposta.Mensagem = "Essas são todas as viagens";
                return Resposta;

            }
            catch (Exception ex)
            {
                Resposta.Mensagem = ex.Message;
                Resposta.Status = false;
                return Resposta;
            }
        } //Listando todas as viagens

        public async Task<Modelo_Resposta<List<Modelo_Viagem>>> RelatorioOcupacao()
        {
            Modelo_Resposta<List<Modelo_Viagem>> Resposta = new Modelo_Resposta<List<Modelo_Viagem>>();

            try
            {
                var Assentos = _context.Tabela_Passagem.Select(Passagens => Passagens.assentos).Sum(); //Somando os assentos ocupados 

                StreamWriter Relatorio;

                var ocupacao = (Assentos / 150) * 100; //Calculando os valores dos assentos
                Relatorio = new StreamWriter(@"C:\Relátorio de Ocupação.txt"); //Criando o relatório

                Relatorio.WriteLine($"{Relatorio}%"); //Coloando os valores ao relatório
                Relatorio.Close();

                Resposta.Mensagem = @"Relatório de Ocupação criado com sucesso, irá encontralo em C:\";
                Resposta.Status = true;
                return Resposta;
            }

            catch (Exception ex)
            {
                Resposta.Mensagem = $"Ocorreu uma mensange {ex.Message}";
                Resposta.Status = false;
                return Resposta;
            }
        } //Realizando um relatorio de Ocupação

        public async Task<Modelo_Resposta<List<Modelo_Viagem>>> RelatorioVendas() //Realizando um relatóriio de Vendas
        {
            Modelo_Resposta<List<Modelo_Viagem>> Resposta = new Modelo_Resposta<List<Modelo_Viagem>>();
            try
            {
                var Assentos = await _context.Tabela_Passagem.Select(Passagens => Passagens.assentos).SumAsync(); //Pegando as cadeiras ocupadas 

                var valores = Assentos * Convert.ToDouble(_context.Tabela_Viagem.Select(Viagem => Viagem.Precos)); //Pegando o valor da viagem e fazendo o calculo com as cadeiras

                StreamWriter Relatorio;

                Relatorio = new StreamWriter(@"C:\Relatorio de Vendas.txt");//Criando o relatório

                Relatorio.WriteLine(valores);//Inserindo os valores ao relatório
                Relatorio.Close();

                Resposta.Mensagem = @"Relatório de vendas foi realizado com sucesso, você vai encontrar ele em C:\";
                Resposta.Status = true;
                return Resposta;
            }
            catch (Exception ex)
            {
                Resposta.Mensagem = $"Ocorreu uma mensagem: {ex.Message}";
                Resposta.Status = false;
                return Resposta;
            }

        }
    }
}
