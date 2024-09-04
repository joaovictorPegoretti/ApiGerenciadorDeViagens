using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ViagensApi.Modelos;

namespace ApiGerenciadorDeViagens.Modelos
{
    public class Modelo_Passagens
    {
        [Key]
        public Guid NumeroPassagem { get; init; }


        public string Origem { get; set; }
        public string Destino { get; set; }
        public string DataIda { get; set; }
        public string DataVolta { get; set; }
        public string HoraIda {  get; set; }
        public string HoraVolta { get; set; }
        public string Companhia {  get; set; }
        public int assentos { get; set; }
        public string FormaDePagamento { get; set; }
        public string Cpf {  get; set; }

        public Modelo_Usuario Usuario { get; set; }
        public Modelo_Viagem  Viagens { get; set; } // Aqui é uma propriedade para criar uma coleção de viagens, onde ficaram as viagens escolhidas pelo usuário

       
    }
}
