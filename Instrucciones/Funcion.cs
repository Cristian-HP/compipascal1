using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Expresiones;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace compipascal1.Instrucciones
{
    class Funcion : Instruccion
    {
        public string id { get; set; }
        public int Linea { get; set; }
        public int Columna { get; set; }

        private LinkedList<Simbolos> parametos = new LinkedList<Simbolos>();
        private LinkedList<Expresion> valorparam;
        private LinkedList<Instruccion> instrucciones = new LinkedList<Instruccion>();
        public Tipo tipo;

        public  object Ejecutar(Entorno ent, AST tree)
        {
            try
            {
                Entorno localfun = new Entorno(ent, id.ToLower());
                if (valorparam == null)
                    valorparam = new LinkedList<Expresion>();

                LinkedList<Expresion> obtenidos = new LinkedList<Expresion>();

                foreach(Expresion temp in valorparam)
                {
                    obtenidos.AddLast(new Literal(temp.resolver(ent,tree).valor,0,0));
                }

                if (parametos == null)
                    parametos = new LinkedList<Simbolos>();

                if(parametos.Count == obtenidos.Count)
                {
                    /*for(int index = 0; index < obtenidos.Count; index++)
                    {
                        Simbolos parr = parametos.ElementAt(index);
                        Expresion valexp = obtenidos.ElementAt(index);
                        LinkedList<Simbolos> simlss = new LinkedList<Simbolos>();
                        simlss.AddLast(parr);
                        Declaracion decla = new Declaracion(simlss,parr.tipo,valexp,valexp.Linea,valexp.Columna);
                        decla.Ejecutar(localfun, tree);
                    }*/

                    for (int index = 0; index < obtenidos.Count; index++)
                    {
                        Simbolos parr = parametos.ElementAt(index);
                        Expresion valexp = obtenidos.ElementAt(index);
                        LinkedList<Simbolos> simlss = new LinkedList<Simbolos>();
                        Simbolos temp3 = new Simbolos(parr.valor,parr.tipo,parr.id);
                        simlss.AddLast(temp3);
                        Declaracion decla = new Declaracion(simlss, parr.tipo, valexp, valexp.Linea, valexp.Columna);
                        decla.Ejecutar(localfun, tree);
                    }

                    foreach (Instruccion inst in instrucciones)
                    {
                        if(inst is Exit)
                        {
                            return inst.Ejecutar(localfun, tree);
                        }
                        Object resul = inst.Ejecutar(localfun, tree);

                        if (resul is Exit)
                        {
                            Exit temp = (Exit)resul;
                            return temp.Ejecutar(localfun, tree);
                        }
                            
                    }
                    return ent.obtenerVariable(id.ToLower());
                }
                else
                {
                    throw new Errorp(Linea, Columna, "La Funcion no acepta esta cantidad "+obtenidos.Count+" de parametros", "Semantico");
                }

            }catch(Exception e)
            {
                // guardar error
            }
            return null;
        }

        public Funcion(string id, LinkedList<Simbolos> parametos, LinkedList<Instruccion> instrucciones, Tipo tipo,int linea,int columna)
        {
            this.id = id;
            this.parametos = parametos;
            this.instrucciones = instrucciones;
            this.tipo = tipo;
            Linea = linea;
            Columna = columna;
        }

        public void setValParam(LinkedList<Expresion> valores)
        {
            valorparam = valores;
        }
    }
}
