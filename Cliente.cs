using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EjercicioBd4o
{
    public class Cliente
    {
        public string Id_C { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public Cliente() { }

        public Cliente(string id_C, string nombre, string correo, string telefono, string direccion)
        {
            Id_C = id_C;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
            Direccion = direccion;
        }
    }
}
