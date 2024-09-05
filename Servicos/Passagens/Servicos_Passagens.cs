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

                //var pegarviagem = await _context.Tabela_Passagem.Include(Passagem => Passagem.Usuario).Where(Id => Id.Usuario.CPF == novaPassagemDto.cpf);

                //if (pegarcpf == null)
                //{
                //    resposta.Mensagem = "CPF informado não está registrado no sistema, por favor realize o seu cadastro e tente comprar a passagem novamente";
                //    return resposta;
                //}

                else if ()
                {

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

                var Passagens = await _context.Tabela_Passagem.Include(Acesso => Acesso.Usuario).Where(passagem => passagem.Usuario.CPF == IdUsuario).ToListAsync();

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

        //public async Task<Modelo_Resposta<Modelo_Passagens>> CancelarPassagem(Guid IdPassagem)
        //{
        //    //Modelo_Resposta<Modelo_Passagens> Resposta = new Modelo_Resposta<Modelo_Passagens>();
        //    //try
        //    //{
        //    //    //Acertar essa bosta
        //    //    var Passagens = await _context.Tabela_Passagem
        //    //        .Include(Acesso => Acesso.Viagens).Sum(Passagem => Passagem.assentos - Passagem.Viagens.CadeirasOcupadas);
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //}
        //}
    }
}
