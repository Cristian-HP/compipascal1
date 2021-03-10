using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.abstracta
{
   abstract class Expresion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }

        public abstract Simbolos resolver(Entorno ent, AST tree);

    }
}
