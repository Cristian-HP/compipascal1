using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class IF : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        private Expresion valor;
        private LinkedList<Instruccion> instrucciones;
        private Instruccion _else;

        public IF(Expresion valor, LinkedList<Instruccion> instrucciones, Instruccion @else,int linea,int columna)
        {
            this.valor = valor;
            this.instrucciones = instrucciones;
            _else = @else;
            Linea = linea;
            Columna = columna;
        }

        public  object Ejecutar(Entorno ent, AST tree)
        {
            Simbolos temvalor = valor.resolver(ent, tree);
            if (temvalor.tipo.tipo != Tipos.BOOLEAN)
                throw new Errorp(Linea, Columna, "La expresion que tiene de condicion el 'IF' no es booleano sino de tipo "+temvalor.tipo.tipo.ToString(), "Semantico");
            if (bool.Parse(temvalor.valor.ToString()))
            {
                try
                {
                    foreach(Instruccion ints in instrucciones)
                    {
                        if (ints is Break || ints is Continue || ints is Exit)
                            return ints;
                        if (ints is Exit)
                            return ints;
                        Object resl= ints.Ejecutar(ent, tree);
                        if (resl is Exit)
                            return resl;
         
                    }
                    return null;
                }
                catch
                {
                    //agregar el error a alguna parte
                }
            }
            else
            {
                if (_else != null)
                    _else.Ejecutar(ent, tree);
            }

            return null;
        }
    }
}
