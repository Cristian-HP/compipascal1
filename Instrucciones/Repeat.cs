using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Repeat : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        private Expresion condicion;
        private LinkedList<Instruccion> instrucciones;
        public  object Ejecutar(Entorno ent, AST tree)
        {
            try
            {
                Simbolos temvalor = condicion.resolver(ent, tree);
                if (temvalor.tipo.tipo != Tipos.BOOLEAN)
                    throw new Errorp(Linea, Columna, "La expresion que tiene de condicion el 'WHILE' no es booleano sino de tipo " + temvalor.tipo.tipo.ToString(), "Semantico");
                do
                {
                    foreach (Instruccion inst in instrucciones)
                    {
                        if (inst is Break)
                            return null;
                        else if (inst is Continue)
                            break;
                        else if (inst is Exit)
                            return inst;
                        else
                        {
                            object temp = inst.Ejecutar(ent, tree);
                            if (temp is Break)
                                return null;
                            else if (temp is Continue)
                                break;
                            else if (temp is Exit)
                                return temp;
                        }
                    }
                    temvalor = condicion.resolver(ent, tree);
                }while (!bool.Parse(temvalor.valor.ToString())) ;
            }
            catch
            {
                //guardar valor;
            }
            return null;
        }

        public Repeat(Expresion condicion, LinkedList<Instruccion> instrucciones,int linea,int columna)
        {
            this.condicion = condicion;
            this.instrucciones = instrucciones;
            Linea = linea;
            Columna = columna;
        }
    }
}
