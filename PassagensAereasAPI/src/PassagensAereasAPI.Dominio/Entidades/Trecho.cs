using Geolocation;

namespace PassagensAereasAPI.Dominio.Entidades
{
    public class Trecho
    {

        private Trecho() { }
        public Trecho(Local localOrigem, Local localDestino)
        {
            this.LocalOrigem = localOrigem;
            this.LocalDestino = localDestino;
            this.Distancia = GeoCalculator.GetDistance(localOrigem.Latitude, localOrigem.Longitude, localDestino.Latitude, localDestino.Longitude, 1);
        }

        public Local LocalOrigem { get; private set; }

        public Local LocalDestino { get; private set; }

        public double Distancia { get; private set; }

        public int Id { get; private set; }

        public void Atualizar(Trecho trecho)
        {
            this.LocalOrigem = trecho.LocalOrigem;
            this.LocalDestino = trecho.LocalDestino;
            this.Distancia = GeoCalculator.GetDistance(
            trecho.LocalOrigem.Latitude,
            trecho.LocalOrigem.Longitude,
            trecho.LocalDestino.Latitude,
            trecho.LocalDestino.Longitude,
            1
            );
        }
    }
}