using System.Collections.Generic;
using System.Linq;

namespace PassagensAereasAPI.Dominio.Entidades
{
    public class Reserva
    {
        private Reserva() { }
        public Reserva(Trecho trecho, ClasseDeVoo classeDeVoo, List<ReservaOpcional> reservaOpcional)
        {
            this.Trecho = trecho;
            this.ClasseDeVoo = classeDeVoo;
            this.ReservaOpcional = reservaOpcional;
            this.Valor = CalcularValor();
        }

        public double Valor { get; private set; }
        public Trecho Trecho { get; private set; }
        public ClasseDeVoo ClasseDeVoo { get; private set; }
        public List<ReservaOpcional> ReservaOpcional { get; private set; }
        public int Id { get; private set; }
        public void Atualizar(Reserva reserva)
        {
            this.Trecho = reserva.Trecho;
            this.ClasseDeVoo = reserva.ClasseDeVoo;
            this.ReservaOpcional = reserva.ReservaOpcional;
            this.Valor = CalcularValor();
        }
        private double CalcularValor()
        {
            var somaOpcionais = this.ReservaOpcional.Select(r => r.Opcional.Valor).Sum();

            return (
                ClasseDeVoo.ValorFixoDoVoo +
                (ClasseDeVoo.ValorPorMilha * Trecho.Distancia) +
                (ClasseDeVoo.ValorFixoDoVoo * (somaOpcionais / 100))
                );
        }
    }
}