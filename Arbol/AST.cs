using compipascal1.abstracta;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Arbol
{
    class AST
    {
        public LinkedList<Instruccion> instrucciones;
        //private LinkedList<Objecto> objetos;
        public AST(LinkedList<Instruccion> instrucciones)
        {
            this.instrucciones = instrucciones;
            //objetos = new LinkedList<Objeto>;
        }

        /*public void addObjeto(Objeto ob)
        {
            objetos.AddLast(ob)
        }


        public bool exiteObjeto(string nombre)
        {
            foreach(Objeto obj in objetos)
            {
                if (obj.identificador.Equals(nombre,StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }*/


    }
}
