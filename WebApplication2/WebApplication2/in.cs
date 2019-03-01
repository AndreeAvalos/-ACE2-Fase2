using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class @in
    {
        private int tipo, carnet, valor_medicion;
        private string latitud, longitud, unidad;

        public @in(int id_tipo, int carnet, string latitud, string longitud, int valor_medicion, string unidad)
        {
            this.Tipo = id_tipo;
            this.Carnet = carnet;
            this.Latitud = latitud;
            this.Longitud = longitud;
            this.Valor_medicion = valor_medicion;
            this.Unidad = unidad;
        }

        public int Carnet { get => carnet; set => carnet = value; }
        public int Valor_medicion { get => valor_medicion; set => valor_medicion = value; }
        public string Latitud { get => latitud; set => latitud = value; }
        public string Longitud { get => longitud; set => longitud = value; }
        public string Unidad { get => unidad; set => unidad = value; }
        public int Tipo { get => tipo; set => tipo = value; }
    }
}
