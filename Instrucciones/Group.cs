using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Group : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        public LinkedList<Instruccion> instrucciones { get; set; }

        public Group(LinkedList<Instruccion> instrucciones,int linea,int columna)
        {
            this.instrucciones = instrucciones;
            this.Linea = linea;
            this.Columna = columna;
        }
        public object Ejecutar(Entorno ent, AST tree, Erroresglo herror)
        {
            throw new NotImplementedException();
        }
    }
}
