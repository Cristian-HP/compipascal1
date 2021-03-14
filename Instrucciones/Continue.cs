using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Continue : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        public object Ejecutar(Entorno ent, AST tree)
        {
            throw new NotImplementedException();
        }

        public Continue(int linea,int columna)
        {
            Linea = linea;
            Columna = columna;
        }
    }
}
