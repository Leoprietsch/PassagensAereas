using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Servicos
{
    public class ClasseDeVooService
    {
        public List<string> Validar(ClasseDeVoo classeDeVoo)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(classeDeVoo.Descricao?.Trim()))
                mensagens.Add("É necessário informar a descrição.");

            if (classeDeVoo.ValorFixoDoVoo <= default(double))
                mensagens.Add("É necessário informar o valor fixo do voo.");
            if (classeDeVoo.ValorPorMilha <= default(double))
                mensagens.Add("É necessário informar o valor por milha.");

            return mensagens;
        }
    }
}