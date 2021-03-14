using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Expresiones;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Asignacion : Instruccion
    {
        private Expresion valor;
        private Id id;

        public int Linea { get; set; }
        public int Columna { get; set; }

        public  object Ejecutar(Entorno ent, AST tree)
        {
            try
            {
                Simbolos valor = this.valor.resolver(ent, tree);
                Simbolos idtem = this.id.resolver(ent, tree);
                if (idtem.constante)
                    throw new Errorp(Linea, Columna, "No es posible reasignar el valor a una constante", "Semantico");
                if (valor.tipo.tipo == idtem.tipo.tipo)
                {
                    idtem.valor = valor.valor;
                    ent.asignavariable(idtem.id.ToString().ToLower(), idtem);
                }
                else
                    throw new Errorp(Linea, Columna, "No es posible asignar un valor de tipo: "+valor.tipo.tipo.ToString()+" A una variable de  Tipo "+idtem.tipo.tipo.ToString(), "Semantico");
            }
            catch
            {
                //agregar el error a algun lado
            }
            return null;
        }

        public Asignacion(Expresion valor, Id id,int linea,int columna)
        {
            this.valor = valor;
            this.id = id;
            Linea = linea;
            Columna = Columna;
        }
    }
}
