using ApiGerenciadorDeViagens.Modelos;

namespace ApiGerenciadorDeViagens.Dto
{
    public class NovaViagemDto //Um DTO para criação de uma nova viagem
    {
        public string Origem { get; set; }
        public string Destino { get; set; }

        private DateOnly DataIda;
        private DateOnly DataVolta;
        public string dataIda 
        {
            get;
            set;
        }

        public string dataVolta
        {
            get;
            set;
        }

        private TimeOnly HoraIda;
        private TimeOnly HoraVolta;

        public string horaIda
        {
            get;
            set;
        }

        public string horaVolta
        {
            get;
            set;

        }
        public string Companhia { get; init; }
        public double Precos { get; set; }
    }

}
