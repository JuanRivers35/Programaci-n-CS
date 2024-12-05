using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioBd4o
{
    public class VentanaOrden
    {
        public string Id_VO { get; set; } 
        public string Tipo_Ventana { get; set; } 
        public string Largo_v { get; set; } 
        public string Ancho_v { get; set; } 
        public string Costo { get; set; } 
        public VentanaOrden() { }

        public VentanaOrden(string id_VO, string tipo_Ventana, string largo_v, string ancho_v, string costo)
        {
            Id_VO = id_VO;
            Tipo_Ventana = tipo_Ventana;
            Largo_v = largo_v;
            Ancho_v = ancho_v;
            Costo = costo;
            
        }
    }
}
