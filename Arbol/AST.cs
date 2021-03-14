using compipascal1.abstracta;
using compipascal1.Instrucciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Arbol
{
    class AST
    {
        public LinkedList<Instruccion> instrucciones;
        public LinkedList<Funcion> funciones;
        public LinkedList<Metodo> metodos;
        //private LinkedList<Objecto> objetos;
        public AST(LinkedList<Instruccion> instrucciones,LinkedList<Funcion> funciones,LinkedList<Metodo> metodos)
        {
            this.instrucciones = instrucciones;
            this.funciones = funciones;
            this.metodos = metodos;
            //objetos = new LinkedList<Objeto>;
        }

        public bool existefun(String id)
        {
            foreach(Funcion fun in funciones)
            {
                if (fun.id.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public Funcion getfuncion(String id)
        {
            foreach (Funcion fun in funciones)
            {
                if (fun.id.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return fun;
                }
            }
            return null;
        }



        public bool existeMet(String id)
        {
            foreach (Metodo Met in metodos)
            {
                if (Met.namemet.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public Metodo getMetodo(String id)
        {
            foreach (Metodo Met in metodos)
            {
                if (Met.namemet.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return Met;
                }
            }
            return null;
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
