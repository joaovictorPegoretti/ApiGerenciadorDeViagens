using ApiGerenciadorDeViagens.Modelos;
using System.Text.Json.Serialization;

namespace ViagensApi.Modelos
{
    public class Modelo_Viagem
    {
        public Guid Id { get; init; }
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
        public int Cadeiras { get; init; }
        public int CadeirasOcupadas { get; set; }
        [JsonIgnore]
        public ICollection<Modelo_Passagens> Passagens { get; set; }
        

    }
}
