using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Case : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        public LinkedList<Expresion> condicion;
        private LinkedList<Instruccion> instrucciones;
        public Simbolos initcondicion { get; set; }
        public  object Ejecutar(Entorno ent, AST tree, Erroresglo herror)
        {
            try
            {
                foreach (Expresion exp in condicion)
                {
                    Simbolos mytem = exp.resolver(ent, tree,herror);
                    if (comparar(initcondicion, mytem,ent))
                    {
                        foreach(Instruccion inst in instrucciones)
                        {
                            if (inst is Break || inst is Continue || inst is Exit)
                                return inst;
                            Object resl=inst.Ejecutar(ent, tree,herror);
                            if (resl is  Exit || resl is Break || resl is Continue)
                                return resl;
                        }
                        return true;
                    }
                }
                return null;
            }
            catch(Errorp er)
            {
                herror.adderr(er);
                Form1.errorcon.AppendText(er.ToString() + "\n");
                return null;
            }
        }

        public Case(LinkedList<Expresion> condicion, LinkedList<Instruccion> instrucciones,int linea,int columna)
        {
            this.condicion = condicion;
            this.instrucciones = instrucciones;
            Linea = linea;
            Columna = columna;
        }

        private bool comparar(Simbolos initcondi,Simbolos mycondi,Entorno ent)
        {
            if(initcondi.tipo.tipo == mycondi.tipo.tipo)
            {
                if(initcondi.tipo.tipo == Tipos.INTEGER)
                {
                    int initemp = int.Parse(initcondi.valor.ToString());
                    int mytemp = int.Parse(mycondi.valor.ToString());
                    return initemp == mytemp ? true : false;
                }else if(initcondi.tipo.tipo == Tipos.BOOLEAN)
                {
                    bool initem = bool.Parse(initcondi.valor.ToString());
                    bool mytemp = bool.Parse(mycondi.valor.ToString());
                    return initem && mytemp;
                }
                else
                {
                    return String.Compare(initcondi.valor.ToString(), mycondi.valor.ToString()) == 0? true : false;
                }
            }
            else
            {
                throw new Errorp(Linea,Columna,"El tipo de la condicon Case y su opcion No Coninciden","Semantico", ent.nombre);
            }
        }
    }
}
