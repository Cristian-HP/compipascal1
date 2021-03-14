using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Simbolo
{
    class Errorp : Exception
    {
        public int Linea {get;set;}
        public int Columna { get; set; }
        public string msg { get; set; }
        public string tipo { get; set; }
        public string ambito { get; set; }
        public Errorp(int linea, int columna, string msg, string tipo,string ambito)
        {
            Linea = linea+1;
            Columna = columna;
            this.msg = msg;
            this.tipo = tipo;
            this.ambito = ambito;
        }

        public override string ToString()
        {
            return "Se encontro un error de tipo: " + this.tipo+" En la linea: "+this.Linea+" y columna: "+this.Columna +"En el ambito de"+this.ambito+" Descripcion: "+this.msg;
        }
    }
}
