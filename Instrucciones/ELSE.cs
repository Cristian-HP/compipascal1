using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class ELSE : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        private LinkedList<Instruccion> instrucciones;
        public  object Ejecutar(Entorno ent, AST tree)
        {
            foreach (Instruccion inst in instrucciones)
            {
                if (inst is Break || inst is Continue)
                    return inst;
                if (inst is Exit)
                    return inst;
                object resl= inst.Ejecutar(ent, tree);
                if (resl is Exit)
                    return resl;
            }
            return null;
        }

        public ELSE(LinkedList<Instruccion> instrucciones,int linea,int columna)
        {
            this.instrucciones = instrucciones;
            Linea = linea;
            Columna = columna;
        }
    }
}
