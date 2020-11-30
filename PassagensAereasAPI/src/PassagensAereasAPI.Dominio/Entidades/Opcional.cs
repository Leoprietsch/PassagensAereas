namespace PassagensAereasAPI.Dominio.Entidades
{
    public class Opcional
    {
        private Opcional() { }

        public Opcional(string nome, string descricao, double valor)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Valor = valor;
        }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double Valor { get; private set; }
        public int Id { get; private set; }

        public void Atualizar(Opcional opcional)
        {
            this.Nome = opcional.Nome;
            this.Descricao = opcional.Descricao;
            this.Valor = opcional.Valor;
        }
    }
}