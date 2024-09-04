namespace ApiGerenciadorDeViagens.Dto
{
    public class NovaPassagemDto
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public string DataIda {  get; set; }
        public string DataVolta { get; set; }
        public string HoraIda {  get; set; }
        public string HoraVolta {  get; set; }
        public string Companhia {  get; set; }
        public int Assentos { get; set; }
        public string Forma_De_Pagamento { get; set; }
        public string cpf { get; set; }
    }
}
