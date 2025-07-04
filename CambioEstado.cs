using System;

namespace RedSismicaWinForms
{
    public class CambioEstado
    {
        private DateTime fechaHoraInicio;
        private DateTime? fechaHoraFin;
        private Estado estado;
        private Usuario usuario;

        public CambioEstado(DateTime inicio, DateTime? fin, Estado estado, Usuario usuario)
        {
            this.fechaHoraInicio = inicio;
            this.fechaHoraFin = fin;
            this.estado = estado;
            this.usuario = usuario;
        }

        public void setFechaHoraFin(DateTime fecha) { fechaHoraFin = fecha; }

        public CambioEstado sosActual(List<CambioEstado> cambiosDeEstado)
        {
            foreach (var cambio in cambiosDeEstado)
            {
                if (cambio == cambiosDeEstado.Last())
                {
                    return cambio;
                }
                
            }
            return null;
        }
            
    }
}
