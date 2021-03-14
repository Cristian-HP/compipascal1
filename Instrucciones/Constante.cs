using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Expresiones;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Constante : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        private Expresion valor;
        private Simbolos constante;
        public  object Ejecutar(Entorno ent, AST tree)
        {
            try
            {
                Simbolos valor = this.valor.resolver(ent, tree);
                constante.tipo = valor.tipo;
                constante.valor = valor.valor;
                ent.declararVariable(constante.id.ToLower(), constante,Linea,Columna);

            }
            catch
            {
                //agregar el error a algun lado
            }
            return null;
        }

        public Constante(Expresion valor, Simbolos conts, int linea, int columna)
        {
            this.valor = valor;
            this.constante = conts;
            Linea = linea;
            Columna = columna;
        }
    }
}
