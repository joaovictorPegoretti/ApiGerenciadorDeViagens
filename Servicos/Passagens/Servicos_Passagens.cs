using ApiGerenciadorDeViagens.Data;
using ApiGerenciadorDeViagens.Dto;
using ApiGerenciadorDeViagens.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using ApiGerenciadorDeViagens.Migrations;


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
                
                var pegardados = _context.Tabela_Passagem.Include(Acesso => Acesso.Usuario).Include(Acesso => Acesso.Viagens).Where(Quando => novaPassagemDto.cpf == Quando.Usuario.CPF).FirstOrDefaultAsync(Quando => Quando.Viagens.Id == novaPassagemDto.idViagem); //Fazendo a verificação dos dados informados
                

                if (pegardados == null)
                {
                    resposta.Mensagem = "O CPF ou o ID da viagem fornecido não são validos, informe novamente";
                    return resposta;
                }

                var Passagem = new Modelo_Passagens() //Criando uma nova passagem
                {
                    IdViagem = novaPassagemDto.idViagem,
                    assentos = novaPassagemDto.Assentos,
                    FormaDePagamento = novaPassagemDto.Forma_De_Pagamento,
                    Cpf = novaPassagemDto.cpf,
                    Checkin = false,
                 
                };

                _context.Tabela_Passagem.Add(Passagem);//Adicionando ao banco
                await _context.SaveChangesAsync();//Salvando as alterações

                resposta.Dados = await _context.Tabela_Passagem.Where(Passagem => Passagem.Usuario.CPF == Passagem.Cpf).ToListAsync();//Listagem das passagens
                resposta.Mensagem = "Sua passagem foi comprada com sucesso";
                return resposta;


            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message;
                resposta.Mensagem = $"Ocorreu um erro: {innerException}";
                return resposta;
            }
        } //Criando uma nova passagem

        public async Task<Modelo_Resposta<List<Modelo_Passagens>>> ListarPassagens(string IdUsuario)
        {
            Modelo_Resposta<List<Modelo_Passagens>> Resposta = new Modelo_Resposta<List<Modelo_Passagens>>();

            try
            {

                var Passagens = await _context.Tabela_Passagem.Include(Acesso => Acesso.Usuario).Include(Acesso => Acesso.Viagens).Where(passagem => passagem.Usuario.CPF == IdUsuario).ToListAsync(); //Verifica os dados informados

                if (Passagens != null)
                {
                    Resposta.Mensagem = "CPF inválido, digite novamente. Por favor";
                    return Resposta;
                }

                
                Resposta.Dados = Passagens; //Mostra todas as passagens
                Resposta.Mensagem = $"Todas as passagens com o CPF {IdUsuario} foram listadas";
                return Resposta;

            }
            catch (Exception ex)
            {

                Resposta.Mensagem = $"Ocorreu uma mensagem: {ex.Message}";
                return Resposta;

            }
        } //Listar todas as passagens

        public async Task<Modelo_Resposta<Modelo_Passagens>> CancelarPassagem(Guid IdPassagem) //Cancelado a passagem selecioanda
        {
            Modelo_Resposta<Modelo_Passagens> Resposta = new Modelo_Resposta<Modelo_Passagens>();
            try
            {
                //Acertar essa bosta

                var PassagemDeletada = await _context.Tabela_Passagem.FirstOrDefaultAsync(Passagem => Passagem.NumeroPassagem == IdPassagem);//Verificando as informações
               

                    if(PassagemDeletada == null){
                    Resposta.Mensagem = "Nenhuma passagem foi encontrada";
                        return Resposta;
                    }

                    _context.Remove(PassagemDeletada); //Apagando a passagem
                    await _context.SaveChangesAsync();//Salvando as alterações

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

        public async Task<Modelo_Resposta<Modelo_Passagens>> RealizarCheckin(Guid IdPassagem) // Efetuando Checkin
        {

            Modelo_Resposta<Modelo_Passagens> Resposta = new Modelo_Resposta<Modelo_Passagens>();
            try
            {
                var confirmarcheckin = await _context.Tabela_Passagem.FirstOrDefaultAsync(Resposta => Resposta.NumeroPassagem == IdPassagem); //Confirmado chekin
                var ModeloPassagemNovo = new PassagemCheckinDto
                {
                    Checkin = true
                };
                Console.WriteLine(confirmarcheckin);

                

                if (confirmarcheckin == null)
                {
                    Resposta.Mensagem = "Id da passagem fornecido não existe, favor tente novamente";
                    return Resposta;
                }

                _context.Add(ModeloPassagemNovo); //Adicionando o chekin
                _context.SaveChangesAsync();// Salvando as alterações

                Resposta.Dados = confirmarcheckin;
                Resposta.Mensagem = "Seu Checkin foi realizado com sucesso";
                Resposta.Status = true;

                return Resposta;

            }
            catch (Exception ex)
            {

                Resposta.Mensagem = $"Ocorreu uma mensagem: {ex.Message}";
                return Resposta;

            }

        }

        public async Task<Modelo_Resposta<Modelo_Passagens>> EmitirBilhete(Guid IdPassagem) //Emissão de bilhetes
        {
            Modelo_Resposta<Modelo_Passagens> resposta = new Modelo_Resposta<Modelo_Passagens>();
            try
            {
                var verificarid = _context.Tabela_Passagem.FirstOrDefaultAsync(Passagem => Passagem.NumeroPassagem == IdPassagem); //Verificando os dados

                if (verificarid == null)
                {
                    resposta.Mensagem = "Id de passagem informado não existe";
                    resposta.Status = false;
                }

                var Dadosbilhete = _context.Tabela_Passagem.Include(Acesso => Acesso.Viagens).Where(Passagem => Passagem.IdViagem == Passagem.Viagens.Id).Select(Pegar => Pegar.Viagens); //Pegando os dados da passagem

                StreamWriter Bilhete;

                Bilhete = new StreamWriter(@"C:\Bilhete.txt");//Criando o arquivo do bilhete

                Bilhete.WriteLine(Dadosbilhete); //Colocando as informações no arquivo
                Bilhete.Close();

                var emailcliente = _context.Tabela_Passagem.Include(Acesso => Acesso.Usuario).Where(Passagem => Passagem.Cpf == Passagem.Usuario.CPF).Select(Pegar => Pegar.Usuario.Email);//Pegando o email do cliente



                var gmail = new Email("smtp.gmail.com", "Email Do proprietario para envio", "Senha da email"); //Dados do proprietario para o envio

                gmail.Enviaremail(
                 emailsTo: new List<string>
                 {
                       $"{emailcliente}"
                 }, //Enviando o email

                 subject: "Sua Passagem Chegou",
                 body: "Olá, sua passagem acabou de chegar, verifique logo a baixo o anexo", //Textos do email

                 attachments: new List<string>
                 {
                   $"{Bilhete}"
                 });//Arquivo enviado para o email

                resposta.Mensagem = "Bilhete emitido e enviado com sucesso para o seu email";
                resposta.Status = true;
                return resposta;

            }

            catch (Exception ex) 
            {
                resposta.Mensagem = $"Ocorreu uma mensagem: {ex.Message}";
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
