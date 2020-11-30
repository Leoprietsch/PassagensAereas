using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Servicos
{
    public class OpcionalService
    {
        public List<string> Validar(Opcional opcional)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(opcional.Nome?.Trim()))
                mensagens.Add("É necessário informar o nome.");

            if (string.IsNullOrEmpty(opcional.Descricao?.Trim()))
                mensagens.Add("É necessário informar a descrição.");

            if (opcional.Valor <= default(double))
                mensagens.Add("É necessário informar o valor.");

            return mensagens;
        }
    }
}