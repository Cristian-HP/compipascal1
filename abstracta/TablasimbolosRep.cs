using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.abstracta
{
    public class TablasimbolosRep
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        public string Ambito { get; set; }

        public string tipo { get; set; }
        public string nombre { get; set; }
        public TablasimbolosRep(string nombre,int linea, int columna, string ambito, string tipo)
        {
            Linea = linea+1;
            Columna = columna;
            Ambito = ambito;
            this.tipo = tipo;
            this.nombre = nombre;
        }
    }
}
