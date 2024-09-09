namespace ApiGerenciadorDeViagens.Modelos
{
    public class Modelo_Resposta<T> //Padrão de modelo para resposta do sistema para o usuário
    {
        public T? Dados {get; set;}
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}
