using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Simbolo
{
    class Errorp : Exception
    {
        private int Linea, Columna;
        private string msg;
        private string tipo;

        public Errorp(int linea, int columna, string msg, string tipo)
        {
            Linea = linea;
            Columna = columna;
            this.msg = msg;
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return "Se encontro un error de tipo: " + this.tipo+" En la linea: "+this.Linea+" y columna: "+this.Columna +" Descripcion: "+this.msg;
        }
    }
}
