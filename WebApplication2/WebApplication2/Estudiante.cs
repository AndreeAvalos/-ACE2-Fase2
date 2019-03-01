using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class Estudiante
    {
        private int carnet;
        private String nombre;

        public Estudiante(int carnet, string nombre)
        {
            this.carnet = carnet;
            this.nombre = nombre;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Carnet { get => carnet; set => carnet = value; }
    }
}
