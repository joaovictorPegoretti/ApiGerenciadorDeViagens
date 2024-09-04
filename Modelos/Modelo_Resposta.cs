namespace ViagensApi.Modelos
{
    public class Modelo_Resposta<T>
    {
        public T? Dados {get; set;}
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}
