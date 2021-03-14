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
        public  object Ejecutar(Entorno ent, AST tree)
        {
            return resolver(ent, tree);
        }

        public Simbolos resolver(Entorno ent, AST tree)
        {
            Funcion myfun = tree.getfuncion(tempid.ToLower());
            Metodo mymet = tree.getMetodo(tempid.ToLower());
            if(myfun == null && mymet == null)
                throw new Errorp(Linea, Columna, "La Funcion/Procedimieto " + tempid + " no existen", "Semantico");
            if (myfun != null)
            {
                try
                {
                    myfun.setValParam(expresiones);
                    Object resul = myfun.Ejecutar(ent, tree);
                    if (resul is Exit)
                        return null;
                    else
                        return (Simbolos)resul;
                }
                catch (Exception e)
                {
                    //guardar el error
                }
            }
            else
            {
                try
                {
                    mymet.setValParam(expresiones);
                    mymet.Ejecutar(ent, tree);
                    return null;
                }catch(Exception e)
                {

                }
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
