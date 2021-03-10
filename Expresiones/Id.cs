using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Expresiones
{
    class Id : Expresion
    {
        public string name { get; set; }
        public override Simbolos resolver(Entorno ent, AST tree)
        {
            Simbolos temp = ent.obtenerVariable(name);
            if (temp != null)
            {
                return temp;
            }
            else
            {
                //error semantico   poner errore nueva clase error
                throw new Exception();
            }
        }

        public Id(string name,int linea,int columna)
        {
            this.name = name;
            this.Columna = columna;
            this.Linea = linea;
        }
    }
}
