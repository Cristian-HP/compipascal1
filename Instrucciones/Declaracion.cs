using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Declaracion : Instruccion
    {
        public Expresion valor { get; set; }
        public Tipo tipo { get; set; }
        public LinkedList<Simbolos> variables { get; set; }

        public Declaracion(LinkedList<Simbolos> variables, Tipo tipo, int linea, int columna)
        {
            this.variables = variables;
            this.tipo = tipo;
            this.Linea = linea;
            this.Columna = columna;
        }

        public Declaracion(LinkedList<Simbolos> variables, Tipo tipo, Expresion valor, int linea, int columna)
        {
            this.variables = variables;
            this.tipo = tipo;
            this.valor = valor;
            this.Linea = linea;
            this.Columna = columna;
        }

        public override object Ejecutar(Entorno ent, AST tree)
        {
            if(variables.Count >1 && valor != null)
            {
                throw new Exception();
            }else if (variables.Count == 1 && valor != null)
            {
                foreach (Simbolos temp1 in variables)
                {
                    temp1.valor = valor.resolver(ent, tree);
                    temp1.tipo = tipo;
                    ent.declararVariable(temp1.id, temp1);
                }

            }
            else
            {
                foreach (Simbolos aux1 in variables)
                {
                    aux1.tipo = tipo;
                    if(tipo.tipo == Tipos.INTEGER)
                    {
                        aux1.valor = 0;
                    }else if (tipo.tipo == Tipos.REAL)
                    {
                        aux1.valor = 0.0;
                    }else if(tipo.tipo == Tipos.STRING)
                    {
                        aux1.valor = "";
                    }
                    ent.declararVariable(aux1.id, aux1);
                }
            }
            return null;
            // resolver antes de agregar valor 

        }
    }
}
