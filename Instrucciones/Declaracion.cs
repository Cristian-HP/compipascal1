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
        public int Linea { get; set; }
        public int Columna { get; set; }

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

        public  object Ejecutar(Entorno ent, AST tree, Erroresglo herror)
        {
            if(variables.Count >1 && valor != null)
            {
                throw new Errorp(Linea,Columna,"No es posible declarar una lista de variables con valor explicito","Semantico",ent.nombre);
            }else if (variables.Count == 1 && valor != null)
            {
                foreach (Simbolos temp1 in variables)
                {
                    temp1.valor = valor.resolver(ent, tree,herror);
                    temp1.tipo = tipo;
                    ent.declararVariable(temp1.id, temp1,Linea,Columna);
                    Form1.Tablasim.AddLast(new TablasimbolosRep(temp1.id,Linea,Columna,ent.nombre,temp1.tipo.tipo.ToString()));
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
                    ent.declararVariable(aux1.id, aux1,Linea,Columna);
                    Form1.Tablasim.AddLast(new TablasimbolosRep(aux1.id, Linea, Columna, ent.nombre, aux1.tipo.tipo.ToString()));
                }
            }
            return null;
            // resolver antes de agregar valor 

        }
    }
}
