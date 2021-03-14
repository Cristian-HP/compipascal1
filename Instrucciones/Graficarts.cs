using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Graficarts : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }

        public object Ejecutar(Entorno ent, AST tree, Erroresglo herror)
        {
            ent.graficartabla();
            return null;
        }

        public Graficarts()
        {

        }
    }
}
