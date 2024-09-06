using ApiGerenciadorDeViagens.Modelos;

namespace ApiGerenciadorDeViagens.Dto
{
    public class NovaPassagemDto
    {
        public string idViagem { get; set; }
        public int Assentos { get; set; }
        public string Forma_De_Pagamento { get; set; }
        public string cpf { get; set; }
    }
}
