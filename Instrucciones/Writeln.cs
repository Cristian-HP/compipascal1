using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class Writeln : Instruccion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        public LinkedList<Expresion> expresion { get; set; }

        private readonly bool jump;
        public Writeln(LinkedList<Expresion> exp,bool salto,int linea,int columna)
        {
            this.expresion = exp;
            this.Linea = linea;
            this.Columna = columna;
            this.jump = salto;
        }

        public  object Ejecutar(Entorno ent, AST tree )
        {
            string valor = "";
            try
            {
                Simbolos simb;
                foreach(Expresion expre in expresion)
                {
                    simb = expre.resolver(ent, tree);
                    valor += simb.ToString();
                }
                if (jump)
                    Form1.consola2.AppendText(valor+"\n");
                else
                    Form1.consola2.AppendText(valor);
            }
            catch
            {
                //agregar errores
            }

            return null;
        }
    }
}
