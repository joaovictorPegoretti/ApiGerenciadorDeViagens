using ApiGerenciadorDeViagens.Data;
using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


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



                if (pegardados == null)
                {
                    if (novaPassagemDto.idViagem == null)
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

        public async Task<Modelo_Resposta<List<Modelo_Passagens>>> ListarPassagens(string IdUsuario)
        {
            Modelo_Resposta<List<Modelo_Passagens>> Resposta = new Modelo_Resposta<List<Modelo_Passagens>>();

            try
            {

                var Passagens = await _context.Tabela_Passagem.Include(Acesso => Acesso.Usuario).Include(Acesso => Acesso.Viagens).Where(passagem => passagem.Usuario.CPF == IdUsuario).ToListAsync();

                if (Passagens != null)
                {
                    Resposta.Mensagem = "CPF inválido, digite novamente. Por favor";
                    return Resposta;
                }

                Resposta.Dados = Passagens;
                Resposta.Mensagem = $"Todas as passagens com o CPF {IdUsuario} foram listadas";
                return Resposta;

            }
            catch (Exception ex)
            {

                Resposta.Mensagem = $"Ocorreu uma mensagem: {ex.Message}";
                return Resposta;

            }
        }

        public async Task<Modelo_Resposta<Modelo_Passagens>> CancelarPassagem(Guid IdPassagem)
        {
            Modelo_Resposta<Modelo_Passagens> Resposta = new Modelo_Resposta<Modelo_Passagens>();
            try
            {
                //Acertar essa bosta

                var PassagemDeletada = await _context.Tabela_Passagem.FirstOrDefaultAsync(Passagem => Passagem.NumeroPassagem == IdPassagem);
                var PassagensSum =  _context.Tabela_Passagem 
                    .Include(Acesso => Acesso.Viagens)
                    .Where(Passagem => Passagem.NumeroPassagem == IdPassagem)
                    .Sum(Passagem => Passagem.Viagens.Cadeiras - Passagem.assentos);

                    if(PassagensSum == 0){
                    Resposta.Mensagem = "Nenhuma passagem foi encontrada";
                        return Resposta;
                    }

                    _context.Remove(PassagemDeletada);
                    await _context.SaveChangesAsync();

                Resposta.Mensagem = "Passagem foi excluida, com sucesso";
                Resposta.Status = true;
                return Resposta;
            }
            catch (Exception ex)
            {
                Resposta.Mensagem = $"Ocorreu uma mensagem: {ex.Message}";
                return Resposta;
               
            }
        }
    }
}
