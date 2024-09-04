using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using ViagensApi.Data;
using ViagensApi.Dto;
using ViagensApi.Modelos;

namespace ApiGerenciadorDeViagens.Servicos.Passagens
{
    public class Servicos_Passagens : IInterface_Passagem
    {
        private readonly AppDbContext _context;

        public Servicos_Passagens(AppDbContext context) // Acessando o banco
        {
            _context = context;
        }

        public async Task<Modelo_Resposta<List<Modelo_Passagens>>> ComprarPassagem(NovaPassagemDto novaPassagemDto)
        {
            Modelo_Resposta<List<Modelo_Passagens>> resposta = new Modelo_Resposta<List<Modelo_Passagens>>();
            try
            {
                var Passagem = new Modelo_Passagens()
                {
                    Origem = novaPassagemDto.Origem,
                    Destino = novaPassagemDto.Destino,
                    DataIda = novaPassagemDto.DataIda,
                    DataVolta = novaPassagemDto.DataVolta,
                    HoraIda = novaPassagemDto.HoraIda,
                    HoraVolta = novaPassagemDto.HoraVolta,
                    Companhia = novaPassagemDto.Companhia,
                    assentos = novaPassagemDto.Assentos,
                    FormaDePagamento = novaPassagemDto.Forma_De_Pagamento,
                    Cpf = novaPassagemDto.cpf,

                };

                var pegarcpf = await _context.Tabela_Usuario.Include(cliente => cliente.Passagem).FirstOrDefaultAsync(usuarioBanco => usuarioBanco.CPF == novaPassagemDto.cpf);
                var pegarviagem = await _context.Tabela_Viagem.Include(viagem => viagem.Passagens).FirstOrDefaultAsync(viagemBanco => viagemBanco.Origem == novaPassagemDto.Origem && viagemBanco.Destino == novaPassagemDto.Destino && viagemBanco.dataIda == novaPassagemDto.DataIda && viagemBanco.dataVolta == novaPassagemDto.DataVolta && viagemBanco.horaIda == novaPassagemDto.HoraIda && viagemBanco.horaVolta == novaPassagemDto.HoraVolta);
                

                
                if(pegarcpf == null)
                {
                    resposta.Mensagem = "CPF informado não está registrado no sistema, por favor realize o seu cadastro e tente comprar a passagem novamente";
                        return resposta;
                }

                

                _context.Add(Passagem);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Tabela_Passagem.ToListAsync();
                resposta.Mensagem = "Passagem Comprada com sucesso";
                return resposta;


            }
            catch (Exception ex) 
            { 
            
                resposta.Mensagem = $"Ocorreu um erro: {ex.Message}";
                return resposta;
            }
        }

    
    }
}
