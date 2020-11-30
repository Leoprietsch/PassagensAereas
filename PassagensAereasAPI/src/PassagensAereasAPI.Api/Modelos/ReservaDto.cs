using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Api.Modelos
{
    public class ReservaDto
    {
        public int IdTrecho { get; set; }
        public int IdClasseDeVoo { get; set; }
        public List<int> IdsOpcionais { get; set; }
    }
}