using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.abstracta
{
    interface Instruccion
    {
        public abstract object Ejecutar(Entorno ent, AST tree);

        public int Linea { get; set; }
        public int Columna { get; set; }
    }
}
