using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EjercicioBd4o
{
    public class Cita : Cliente
    {
        public string Id_Ct { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string No_Orden { get; set; }

        public Cita() { }

        public Cita(string id_Ct, string id_C, string fecha, string hora, string no_orden)
            : base(id_C, null, null, null, null) // Solo pasamos Id_C, otros parámetros como null
        {
            Id_Ct = id_Ct;
            Fecha = fecha;
            Hora = hora;
            No_Orden = no_orden;
        }
    }
}