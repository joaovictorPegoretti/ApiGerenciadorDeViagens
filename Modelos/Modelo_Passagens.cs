﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiGerenciadorDeViagens.Modelos;

namespace ApiGerenciadorDeViagens.Modelos
{
    public class Modelo_Passagens //Padrão de modelo para as Passagens
    {
        [Key]
        public Guid NumeroPassagem { get; init; }
        public Guid IdViagem { get; set; }

        public int assentos { get; set; }
        public string FormaDePagamento { get; set; }
        public string Cpf {  get; set; }

        public bool Checkin { get; set; }
        public Modelo_Usuario Usuario { get; set; }// Aqui é uma propriedade para criar uma coleção de viagens, onde ficaram as viagens escolhidas pelo usuário
        public Modelo_Viagem Viagens { get; set; }



    }
}
