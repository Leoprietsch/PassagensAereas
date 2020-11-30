using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PassagensAereasAPI.Dominio.Entidades
{
    public class Usuario
    {
        private Usuario() { }

        public Usuario(string primeiroNome, string ultimoNome, string cpf, Nullable<DateTime> dataDeNascimento, string email, string senha)
        {
            this.PrimeiroNome = primeiroNome;
            this.UltimoNome = ultimoNome;
            this.Cpf = cpf;
            this.DataDeNascimento = dataDeNascimento;
            this.Email = email;
            this.Senha = CriptografarSenha(senha);
            this.Admin = false;
        }

        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }
        public string Cpf { get; private set; }
        public Nullable<DateTime> DataDeNascimento { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public int Id { get; private set; }
        public Boolean Admin { get; private set; }
        public List<Reserva> Reservas { get; private set; }

        public void Atualizar(Usuario usuario)
        {
            this.PrimeiroNome = usuario.PrimeiroNome;
            this.UltimoNome = usuario.UltimoNome;
            this.Cpf = usuario.Cpf;
            this.DataDeNascimento = usuario.DataDeNascimento;
            this.Email = usuario.Email;
            this.Senha = CriptografarSenha(usuario.Senha);
        }

        private string CriptografarSenha(string senha)
        {
            var inputBytes = Encoding.UTF8.GetBytes(senha);

            var hashedBytes = new SHA256CryptoServiceProvider().ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}