using ApiGerenciadorDeViagens.Modelos;
using Microsoft.EntityFrameworkCore;



namespace ApiGerenciadorDeViagens.Data
{
    public class AppDbContext : DbContext // Essa classe é a responsavel pela conexão entre o banco e a aplicação
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) 
        {
            
        }

        //Aqui estão sendo referenciada quais classes serão tabelas no banco de dados
        public DbSet<Modelo_Usuario> Tabela_Usuario { get; set; }

        public DbSet<Modelo_Viagem> Tabela_Viagem { get; set; }
        public DbSet<Modelo_Passagens> Tabela_Passagem { get; set; }
    }
}
