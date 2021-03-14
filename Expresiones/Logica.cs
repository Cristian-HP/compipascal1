using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Expresiones
{
    class Logica : Expresion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        public Expresion Izquierda { get; set; }
        public Expresion Derecha { get; set; }

        public string tipoope { get; set; }

        private bool unario;
        public  Simbolos resolver(Entorno ent, AST tree, Erroresglo herror)
        {
            if (unario)
            {
                Simbolos unariotemp = Izquierda.resolver(ent, tree,herror);
                if (unariotemp.tipo.tipo == Tipos.INTEGER)
                {
                    return new Simbolos(~int.Parse(unariotemp.ToString()), new Tipo(unariotemp.tipo.tipo, ""), "");
                }
                else if (unariotemp.tipo.tipo == Tipos.BOOLEAN)
                {
                    return new Simbolos( !bool.Parse(unariotemp.ToString()), new Tipo(unariotemp.tipo.tipo, ""), "");
                }
                else
                {
                    throw new Errorp(Linea, Columna, "No se puede realizar la operacion unaria " + palabraope() + " con el valor de tipo " + unariotemp.tipo.ToString(), "Semantico", ent.nombre);
                }
            }
            else
            {
                Simbolos izq = Izquierda.resolver(ent, tree,herror);
                Simbolos der = Derecha.resolver(ent, tree,herror);
                Tipos tiporesul = Tablatipos.getTipo(izq.tipo, der.tipo, tipoope);
                if (tiporesul == Tipos.ERROR)
                    throw new Errorp(Linea, Columna, "No se puede realizar la operacion " + palabraope() + " entre " + izq.tipo.ToString() + " y un " + der.tipo.ToString(), "Semantico", ent.nombre);

                Tipo temptipo = new Tipo(tiporesul, null);
                Simbolos resul;

                switch (tipoope.ToLower())
                {
                    case "and":
                        if (tiporesul == Tipos.INTEGER)
                        {
                            resul = new Simbolos(int.Parse(izq.valor.ToString()) & int.Parse(der.valor.ToString()), temptipo,"");
                            return resul;
                        }
                        else
                        {
                            resul = new Simbolos(bool.Parse(izq.valor.ToString()) && bool.Parse(der.valor.ToString()), temptipo, "");
                            return resul;
                        }
                    default:
                        if (tiporesul == Tipos.INTEGER)
                        {
                            resul = new Simbolos(int.Parse(izq.valor.ToString()) | int.Parse(der.valor.ToString()), temptipo, "");
                            return resul;
                        }
                        else
                        {
                            resul = new Simbolos(bool.Parse(izq.valor.ToString()) || bool.Parse(der.valor.ToString()), temptipo, "");
                            return resul;
                        }
                }
            }
        }

        public Logica(Expresion izquierda, Expresion derecha, string tipo,int linea,int columna)
        {
            this.Izquierda = izquierda;
            this.Derecha = derecha;
            this.tipoope = tipo;
            this.Linea = linea;
            this.Columna = columna;
            this.unario = false;
        }

        public Logica(Expresion izquierda, string tipo,int linea,int columna)
        {
            this.Izquierda = izquierda;
            this.tipoope = tipo;
            this.Linea = linea;
            this.Columna = columna;
            this.unario = true;
        }


        private string palabraope()
        {
            switch (tipoope.ToLower())
            {
                case "not":
                    return "Negacion logica";
                case "and":
                    return "AND logico";
                default:
                    return "OR logico";
            }
        }
    }
}
