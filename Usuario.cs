using System;

namespace RedSismicaWinForms
{
    public class Usuario
    {
        private string contrase�a;
        private string nombre;
        private DateTime fechaAlta;
        private AnalistaEnSismos analistaEnSismo;

        public Usuario(string nombre) { this.nombre = nombre; }
        public string getUsuario() => nombre;
        public string getAnalistaEnSismos() => analistaEnSismo.getNombre();
    }
}
