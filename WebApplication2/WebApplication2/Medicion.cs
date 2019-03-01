using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class Medicion
    {
        private int id_medicion,id_tipo, carnet, valor_medicion;
        private string fecha, hora, latitud, longitud,unidad;

        public Medicion(int id_medicion, int id_tipo, int carnet, int valor_medicion, string fecha, string hora, string latitud, string longitud, string unidad)
        {
            this.id_medicion = id_medicion;
            this.id_tipo = id_tipo;
            this.carnet = carnet;
            this.valor_medicion = valor_medicion;
            this.fecha = fecha;
            this.hora = hora;
            this.latitud = latitud;
            this.longitud = longitud;
            this.unidad = unidad;
        }

        public int Id_tipo { get => id_tipo; set => id_tipo = value; }
        public int Carnet { get => carnet; set => carnet = value; }
        public int Valor_medicion { get => valor_medicion; set => valor_medicion = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Hora { get => hora; set => hora = value; }
        public string Latitud { get => latitud; set => latitud = value; }
        public string Longitud { get => longitud; set => longitud = value; }
        public string Unidad { get => unidad; set => unidad = value; }
        public int Id_medicion { get => id_medicion; set => id_medicion = value; }
    }
}
