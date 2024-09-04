using ViagensApi.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using ViagensApi.Servicos.Usuario;
using ViagensApi.Servicos.Viagem;
using Microsoft.EntityFrameworkCore;
using ApiGerenciadorDeViagens.Servicos.Passagens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInterface_Usuario, Servicos_Usuario>(); //Eu estou informado que os métodos da IInterfaceUsuario estão sendo implementados por ServicosUsuario
builder.Services.AddScoped<IInterface_Viagem, Servicos_Viagem>();
builder.Services.AddScoped<IInterface_Passagem, Servicos_Passagens>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
