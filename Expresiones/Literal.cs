using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Expresiones
{
    class Literal : Expresion
    {
        public object valor { get; set; }

        public Literal(object valor,int linea,int columna)
        {
            this.valor = valor;
            Columna = columna;

        }
        public override Simbolos resolver(Entorno ent, AST tree)
        {
            Tipos temp2 = Obtenertipo(valor);
            Tipo tipo = new Tipo(temp2, null);
            return new Simbolos(valor, tipo, "");
        }

        private  Tipos Obtenertipo (object valor)
        {
            if (valor is int) return Tipos.INTEGER;
            else if (valor is double) return Tipos.REAL;
            else if (valor is decimal) return Tipos.REAL;
            else if (valor is bool) return Tipos.BOOLEAN;
            else if (valor is string) return Tipos.STRING;
            else return Tipos.ERROR;
        }
    }
}
