using ApiGerenciadorDeViagens.Modelos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiGerenciadorDeViagens.Modelos
{
    public class Modelo_Usuario //Padrão de modelo para usuario
    {
        [Key] 
        public string CPF { get; init; }


        public string Nome { get; set; }
        public string Endereco_Rua {  get; set; } 
        public string Endereco_Bairro { get; set; }
        public string Endereco_Numero { get; set; }
        public string Telefone_Fixo { get; set; }
        public string Telefone_Celular { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public ICollection<Modelo_Passagens> Passagem { get;}

        

        
    }
}
