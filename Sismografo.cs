namespace RedSismicaWinForms
{
    public class Sismografo
    {
        private int idSismografo;
        private string nombreSismografo;
        private string descripcion;
        private string nroSerie;

        public Sismografo(int id, string nombre, string descripcion, string nroSerie)
        {
            this.idSismografo = id;
            this.nombreSismografo = nombre;
            this.descripcion = descripcion;
            this.nroSerie = nroSerie;
        }

        public string getNombreSismografo()
        {
            return nombreSismografo;
        }

        public bool esDeSerieTemporal()
        {
            // Esta implementaci�n es gen�rica. Ajustala a tu l�gica real si es m�s compleja.
            return true;
        }

        // Getters opcionales
        public int getIdSismografo() => idSismografo;
        public string getDescripcion() => descripcion;
        public string getNroSerie() => nroSerie;
    }
}
