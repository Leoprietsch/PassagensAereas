namespace PassagensAereasAPI.Dominio.Entidades
{
    public class ReservaOpcional
    {
        private ReservaOpcional() { }
        public ReservaOpcional(Opcional opcional)
        {
            this.Opcional = opcional;
        }
        public Opcional Opcional { get; private set; }
        public int Id { get; private set; }

    }
}