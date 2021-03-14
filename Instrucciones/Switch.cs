using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Switch : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        private Expresion condicion;
        private LinkedList<Case> lista_case;
        private Instruccion defaultcase;
        public object Ejecutar(Entorno ent, AST tree, Erroresglo herror)
        {
            try
            {
                Simbolos condi = condicion.resolver(ent, tree,herror);
                foreach(Case opcion in lista_case)
                {
                    opcion.initcondicion = condi;
                    object resul =opcion.Ejecutar(ent, tree,herror);
                    if(resul != null)
                    {
                        try
                        {
                            if (bool.Parse(resul.ToString()))
                                return null;
                        }
                        catch(Exception)
                        {
                            return resul;
                        }
                        
                    } 
                }
                if (defaultcase != null)
                    defaultcase.Ejecutar(ent, tree,herror);
            }
            catch(Errorp er)
            {
                herror.adderr(er);
                Form1.errorcon.AppendText(er.ToString() + "\n");
                //guardar el error
            }
            return null;
        }

        public Switch(Expresion condicion, LinkedList<Case> lista_case, Instruccion defaultcase,int linea,int columna)
        {
            this.condicion = condicion;
            this.lista_case = lista_case;
            this.defaultcase = defaultcase;
            Linea = linea;
            Columna = columna;
        }
    }
}
