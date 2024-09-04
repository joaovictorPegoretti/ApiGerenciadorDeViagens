using ApiGerenciadorDeViagens.Modelos;
using Microsoft.EntityFrameworkCore;
using ViagensApi.Modelos;


namespace ViagensApi.Data
{
    public class AppDbContext : DbContext // Classe para conectar o banco e o código
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) 
        {
            
        }

        //Criando as tabelas do banco  
        public DbSet<Modelo_Usuario> Tabela_Usuario { get; set; }

        public DbSet<Modelo_Viagem> Tabela_Viagem { get; set; }
        public DbSet<Modelo_Passagens> Tabela_Passagem { get; set; }
    }
}
