using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Expresiones
{
    class Logica : Expresion
    {
        public Expresion izquierda { get; set; }
        public Expresion derecha { get; set; }

        public string tipo { get; set; }

        private bool unario;
        public override Simbolos resolver(Entorno ent, AST tree)
        {
            throw new NotImplementedException();
        }

        public Logica(Expresion izquierda, Expresion derecha, string tipo,int linea,int columna)
        {
            this.izquierda = izquierda;
            this.derecha = derecha;
            this.tipo = tipo;
            this.Linea = linea;
            this.Columna = columna;
            this.unario = false;
        }

        public Logica(Expresion izquierda, string tipo,int linea,int columna)
        {
            this.izquierda = izquierda;
            this.tipo = tipo;
            this.Linea = linea;
            this.Columna = columna;
            this.unario = true;
        }
    }
}
