using System;
using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Api.Modelos
{
    public class UsuarioResponseDto
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Cpf { get; set; }
        public Nullable<DateTime> DataDeNascimento { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
    }
}