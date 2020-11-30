using System;
using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Servicos
{
    public class ReservaService
    {
        public List<string> Validar(Usuario usuario, Reserva reserva)
        {
            List<string> mensagens = new List<string>();

            var hoje = DateTime.Today;
            var idade = hoje.Year - usuario.DataDeNascimento.Value.Year;
            if (usuario.DataDeNascimento > hoje.AddYears(-idade))
            {
                idade--;
            }

            if (idade < 18)
            {
                mensagens.Add("É necessário ser ter 18 anos para realizar uma reserva.");
            }

            if (reserva.Trecho == null)
                mensagens.Add("É necessário informar um trecho");

            if (reserva.ClasseDeVoo == null)
                mensagens.Add("É necessário informar a classe");

            return mensagens;
        }
    }
}
