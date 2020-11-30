using System;
using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Servicos
{
    public class UsuarioService
    {
        public List<string> Validar(Usuario usuario)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(usuario.PrimeiroNome?.Trim()))
                mensagens.Add("É necessário informar o primeiro nome.");

            if (string.IsNullOrEmpty(usuario.UltimoNome?.Trim()))
                mensagens.Add("É necessário informar o ultimo nome.");

            if (string.IsNullOrEmpty(usuario.Cpf?.Trim()))
                mensagens.Add("É necessário informar o CPF.");

            if (usuario.DataDeNascimento == null)
                mensagens.Add("É necessário informar a data de nascimento.");

            if (usuario.DataDeNascimento.Value > DateTime.Now)
                mensagens.Add("A data informada é do futuro.");

            if (string.IsNullOrEmpty(usuario.Email?.Trim()))
                mensagens.Add("É necessário informar o e-mail.");

            if (string.IsNullOrEmpty(usuario.Senha?.Trim()))
                mensagens.Add("É necessário informar a senha.");

            return mensagens;
        }
    }
}