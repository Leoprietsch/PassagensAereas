using Geolocation;

namespace PassagensAereasAPI.Dominio.Entidades
{
    public class Local
    {
        private Local() { }
        public Local(double latitude, double longitude, string nome)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Nome = nome;
        }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string Nome { get; private set; }
        public int Id { get; private set; }

        public void Atualizar(Local local)
        {
            this.Latitude = local.Latitude;
            this.Longitude = local.Longitude;
            this.Nome = local.Nome;
        }
    }
}