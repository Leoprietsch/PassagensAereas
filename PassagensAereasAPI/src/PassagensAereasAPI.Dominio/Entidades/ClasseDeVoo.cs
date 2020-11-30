namespace PassagensAereasAPI.Dominio.Entidades
{
    public class ClasseDeVoo
    {
        private ClasseDeVoo() { }
        public ClasseDeVoo(string descricao, double valorFixoDoVoo, double valorPorMilha)
        {
            this.Descricao = descricao;
            this.ValorFixoDoVoo = valorFixoDoVoo;
            this.ValorPorMilha = valorPorMilha;
        }
        public string Descricao { get; private set; }
        public double ValorFixoDoVoo { get; private set; }
        public double ValorPorMilha { get; private set; }
        public int Id { get; private set; }

        public void Atualizar(ClasseDeVoo classeDeVoo)
        {
            this.Descricao = classeDeVoo.Descricao;
            this.ValorFixoDoVoo = classeDeVoo.ValorFixoDoVoo;
            this.ValorPorMilha = classeDeVoo.ValorPorMilha;
        }
    }
}