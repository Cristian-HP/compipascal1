using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Exit : Instruccion,Expresion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        public Expresion valorR;

        public Exit(int linea, int columna, Expresion valorR)
        {
            Linea = linea;
            Columna = columna;
            this.valorR = valorR;
        }

        public object Ejecutar(Entorno ent, AST tree)
        {
            return valorR.resolver(ent, tree);
        }

        public Simbolos resolver(Entorno ent, AST tree)
        {
            return valorR.resolver(ent, tree);
        }
    }
}
