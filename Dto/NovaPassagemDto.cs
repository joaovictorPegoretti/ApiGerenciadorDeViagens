using ApiGerenciadorDeViagens.Modelos;

namespace ApiGerenciadorDeViagens.Dto
{
    public class NovaPassagemDto //Um DTO para criação de uma nova passagem
    {
        public Guid idViagem { get; set; }
        public int Assentos { get; set; }
        public string Forma_De_Pagamento { get; set; }
        public string cpf { get; set; }
       
    }
}
