using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class WHILE : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        private LinkedList<Instruccion> instrucciones;
        private Expresion valor;
        public  object Ejecutar(Entorno ent, AST tree, Erroresglo herror)
        {
            try
            {
                Simbolos temvalor = valor.resolver(ent, tree,herror);
                if (temvalor.tipo.tipo != Tipos.BOOLEAN)
                    throw new Errorp(Linea, Columna, "La expresion que tiene de condicion el 'WHILE' no es booleano sino de tipo " + temvalor.tipo.tipo.ToString(), "Semantico", ent.nombre);
                while (bool.Parse(temvalor.valor.ToString()))
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
                            object temp = inst.Ejecutar(ent, tree,herror);
                            if (temp is Break)
                                return null;
                            else if (temp is Continue)
                                break;
                            else if (temp is Exit)
                                return temp;
                        }
                    }
                    temvalor = valor.resolver(ent, tree,herror);
                }
            }
            catch(Errorp er)
            {
                herror.adderr(er);
                Form1.errorcon.AppendText(er.ToString() + "\n");
                //guardar el error
            }
           
            return null;
        }

        public WHILE(LinkedList<Instruccion> instrucciones, Expresion valor,int linea,int columna)
        {
            this.instrucciones = instrucciones;
            this.valor = valor;
            Linea = linea;
            Columna = columna;
        }
    }
}
