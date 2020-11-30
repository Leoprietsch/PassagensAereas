using System;

namespace PassagensAereasAPI.Api.Modelos
{
    public class UsuarioDto
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Cpf { get; set; }
        public Nullable<DateTime> DataDeNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}