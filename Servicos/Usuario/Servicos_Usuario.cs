using ApiGerenciadorDeViagens.Dto;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication;
using ApiGerenciadorDeViagens.Data;
using ApiGerenciadorDeViagens.Modelos;

namespace ApiGerenciadorDeViagens.Servicos.Usuario
{
    public class Servicos_Usuario : IInterface_Usuario
    {
        private readonly AppDbContext _context;

        public Servicos_Usuario(AppDbContext context) // Acessando o banco
        {
            _context = context;
        }

        public async Task<Modelo_Resposta<List<Modelo_Usuario>>> CadastrarUsuario(NovoUsuaripDto novoUsuaripDto)
        {
            Modelo_Resposta<List<Modelo_Usuario>> resposta = new Modelo_Resposta<List<Modelo_Usuario>>();


                try
            {
                var Cadastro = new Modelo_Usuario
                {
                    CPF = novoUsuaripDto.Cpf,
                    Nome = novoUsuaripDto.Nome,
                    Endereco_Rua = novoUsuaripDto.Endereco_Rua,
                    Endereco_Bairro = novoUsuaripDto.Endereco_Bairro,
                    Endereco_Numero = novoUsuaripDto.Endereco_Numero,
                    Telefone_Celular = novoUsuaripDto.Telefone_Celular,
                    Telefone_Fixo = novoUsuaripDto.Telefone_Fixo,
                    Email = novoUsuaripDto.Email,
                };

                _context.Add(Cadastro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Tabela_Usuario.ToListAsync();
                resposta.Mensagem = "Cadastro realizado com sucesso";
                return resposta;


            }
            catch (Exception ex) {
                resposta.Mensagem = $"Ocorreu um erro: {ex.Message}";
                resposta.Status = false;
                return resposta;
            }

        }

        public async Task<Modelo_Resposta<List<Modelo_Usuario>>> ListarUsarios()
        {
            Modelo_Resposta<List<Modelo_Usuario>> resposta = new Modelo_Resposta<List<Modelo_Usuario>>();
            try
            {
                var Lista_Usuarios = await _context.Tabela_Usuario.ToListAsync();

                resposta.Dados = Lista_Usuarios;
                resposta.Mensagem = "Todos os usuários foram mostrados";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
           
        }
    }
}
