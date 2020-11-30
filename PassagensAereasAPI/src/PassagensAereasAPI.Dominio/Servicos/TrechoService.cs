using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Servicos
{
    public class TrechoService
    {
        public List<string> Validar(Trecho trecho)
        {
            List<string> mensagens = new List<string>();

            if (trecho.LocalOrigem == null)
                mensagens.Add("É necessário informar a origem.");

            if (trecho.LocalDestino == null)
                mensagens.Add("É necessário informar o destino.");

            if (trecho.LocalOrigem == trecho.LocalDestino)
            {
                mensagens.Add("A origem e destino não podem ser iguais.");
            }

            return mensagens;
        }
    }
}