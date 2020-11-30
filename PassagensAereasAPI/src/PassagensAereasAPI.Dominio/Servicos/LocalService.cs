using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Servicos
{
    public class LocalService
    {
        public List<string> Validar(Local local)
        {
            List<string> mensagens = new List<string>();

            if (local.Latitude < -90 || local.Latitude > 90)
                mensagens.Add("Latitude deve estar entre -90 e 90.");

            if (local.Longitude < -180 || local.Longitude > 180)
                mensagens.Add("Longitude deve estar entre -180 e 180.");

            if (string.IsNullOrEmpty(local.Nome?.Trim()))
                mensagens.Add("É necessário informar o nome do local.");

            return mensagens;
        }
    }
}