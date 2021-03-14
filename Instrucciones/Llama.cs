using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Expresiones;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Llama : Instruccion,Expresion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        private string tempid;
        private LinkedList<Expresion> expresiones;
        public  object Ejecutar(Entorno ent, AST tree, Erroresglo herror)
        {
            return resolver(ent, tree,herror);
        }

        public Simbolos resolver(Entorno ent, AST tree, Erroresglo herror)
        {
            try
            {
                Funcion myfun = tree.getfuncion(tempid.ToLower());
                Metodo mymet = tree.getMetodo(tempid.ToLower());
                if (myfun == null && mymet == null)
                    throw new Errorp(Linea, Columna, "La Funcion/Procedimieto " + tempid + " no existen", "Semantico", ent.nombre);
                if (myfun != null)
                {
                    myfun.setValParam(expresiones);
                    Object resul = myfun.Ejecutar(ent, tree, herror);
                    if (resul is Exit)
                        return null;
                    else
                        return (Simbolos)resul;
                }
                else
                {
                    mymet.setValParam(expresiones);
                    mymet.Ejecutar(ent, tree, herror);
                    return null;
                }
            }catch(Errorp er)
            {
                herror.adderr(er);
                Form1.errorcon.AppendText(er.ToString() + "\n");
            }
            
           
            return null;
        }

        public Llama(string tempid, LinkedList<Expresion> expresiones,int linea,int columna)
        {
            this.tempid = tempid;
            this.expresiones = expresiones;
            Linea = linea;
            Columna = columna;
        }
    }
}
