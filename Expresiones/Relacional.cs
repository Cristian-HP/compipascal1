using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Expresiones
{
    class Relacional : Expresion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        public Expresion Izquierda { get; set; }
        public Expresion Derecha { get; set; }

        public string tipoope { get; set; }
        public Simbolos resolver(Entorno ent, AST tree)
        {
            Simbolos izq = Izquierda.resolver(ent, tree);
            Simbolos der = Derecha.resolver(ent, tree);
            Tipos tiporesul = Tablatipos.getTipo(izq.tipo, der.tipo, tipoope);
            if (tiporesul == Tipos.ERROR)
                throw new Errorp(Linea, Columna, "No se puede realizar la operacion " + palabraope() + " entre " + izq.tipo.ToString() + " y un " + der.tipo.ToString(), "Semantico");

            Tipo temptipo = new Tipo(tiporesul, null);
            Simbolos resul;
            switch (tipoope)
            {
                case "<":
                    if(izq.tipo.tipo == Tipos.INTEGER || izq.tipo.tipo == Tipos.REAL)
                    {
                        resul = new Simbolos(double.Parse(izq.valor.ToString()) < double.Parse(der.valor.ToString()), temptipo, "");
                        return resul;
                    }else if(izq.tipo.tipo == Tipos.BOOLEAN)
                    {
                        resul = new Simbolos(operbool(izq,der,tipoope), temptipo, "");
                        return resul;
                    }
                    else
                    {
                        resul = new Simbolos(operstring(izq,der,tipoope), temptipo, "");
                        return resul;
                    }
                case ">":
                    if (izq.tipo.tipo == Tipos.INTEGER || izq.tipo.tipo == Tipos.REAL)
                    {
                        resul = new Simbolos(double.Parse(izq.valor.ToString()) > double.Parse(der.valor.ToString()), temptipo, "");
                        return resul;
                    }
                    else if (izq.tipo.tipo == Tipos.BOOLEAN)
                    {
                        resul = new Simbolos(operbool(izq, der, tipoope), temptipo, "");
                        return resul;
                    }
                    else
                    {
                        resul = new Simbolos(operstring(izq, der, tipoope), temptipo, "");
                        return resul;
                    }
                case "<=":
                    if (izq.tipo.tipo == Tipos.INTEGER || izq.tipo.tipo == Tipos.REAL)
                    {
                        resul = new Simbolos(double.Parse(izq.valor.ToString()) <= double.Parse(der.valor.ToString()), temptipo, "");
                        return resul;
                    }
                    else if (izq.tipo.tipo == Tipos.BOOLEAN)
                    {
                        resul = new Simbolos(operbool(izq, der, tipoope), temptipo, "");
                        return resul;
                    }
                    else
                    {
                        resul = new Simbolos(operstring(izq, der, tipoope), temptipo, "");
                        return resul;
                    }
                case ">=":
                    if (izq.tipo.tipo == Tipos.INTEGER || izq.tipo.tipo == Tipos.REAL)
                    {
                        resul = new Simbolos(double.Parse(izq.valor.ToString()) >= double.Parse(der.valor.ToString()), temptipo, "");
                        return resul;
                    }
                    else if (izq.tipo.tipo == Tipos.BOOLEAN)
                    {
                        resul = new Simbolos(operbool(izq, der, tipoope), temptipo, "");
                        return resul;
                    }
                    else
                    {
                        resul = new Simbolos(operstring(izq, der, tipoope), temptipo, "");
                        return resul;
                    }
                case "=":
                    if (izq.tipo.tipo == Tipos.INTEGER || izq.tipo.tipo == Tipos.REAL)
                    {
                        resul = new Simbolos(double.Parse(izq.valor.ToString()) == double.Parse(der.valor.ToString()), temptipo, "");
                        return resul;
                    }
                    else if (izq.tipo.tipo == Tipos.BOOLEAN)
                    {
                        resul = new Simbolos(operbool(izq, der, tipoope), temptipo, "");
                        return resul;
                    }
                    else
                    {
                        resul = new Simbolos(operstring(izq, der, tipoope), temptipo, "");
                        return resul;
                    }
                default:
                    if (izq.tipo.tipo == Tipos.INTEGER || izq.tipo.tipo == Tipos.REAL)
                    {
                        resul = new Simbolos(double.Parse(izq.valor.ToString()) != double.Parse(der.valor.ToString()), temptipo, "");
                        return resul;
                    }
                    else if (izq.tipo.tipo == Tipos.BOOLEAN)
                    {
                        resul = new Simbolos(operbool(izq, der, tipoope), temptipo, "");
                        return resul;
                    }
                    else
                    {
                        resul = new Simbolos(operstring(izq, der, tipoope), temptipo, "");
                        return resul;
                    }
            }


        }

        public Relacional(Expresion izquierda, Expresion derecha, string tipo,int linea,int columna)
        {
            this.Izquierda = izquierda;
            this.Derecha = derecha;
            this.tipoope = tipo;
            this.Linea = linea;
            this.Columna = columna;
        }

        private string palabraope()
        {
            switch (tipoope)
            {
                case "<":
                    return "Menor ";
                case ">":
                    return "Mayor";
                case "<=":
                    return "Menor Igual";
                case ">=":
                    return "Mayor Igual";
                case "=":
                    return "Igual";
                default:
                    return "Desigual";

            }
        }

        private bool operbool(Simbolos izq1,Simbolos der1,string oper1)
        {
            int intizq = bool.Parse(izq1.valor.ToString()) ? 1 : 0;
            int intder = bool.Parse(der1.valor.ToString()) ? 1 : 0;
            switch (oper1)
            {
                case "<":
                    return intizq < intder;
                case ">":
                    return intizq > intder;
                case "<=":
                    return intizq <= intder;
                case ">=":
                    return intizq >= intder;
                case "=":
                    return intizq == intder;
                default:
                    return intizq != intder;
                    
            }
        }

        private bool operstring(Simbolos izq1,Simbolos der1,string oper1)
        {
            string stgizq = izq1.valor.ToString();
            string stgder = der1.valor.ToString();
            int resul;
            switch (oper1)
            {
                case "<":
                    resul = string.Compare(stgizq, stgder);
                    if (resul <= 0)
                        return false;
                    else
                        return true;
                case ">":
                    resul = string.Compare(stgizq, stgder);
                    if (resul >= 0)
                        return false;
                    else
                        return true;
                case "<=":
                    resul = string.Compare(stgizq, stgder);
                    if (resul < 0)
                        return false;
                    else
                        return true;
                case ">=":
                    resul = string.Compare(stgizq, stgder);
                    if (resul > 0)
                        return false;
                    else
                        return true;
                case "=":
                    resul = string.Compare(stgizq, stgder);
                    if (resul == 0)
                        return true;
                    else
                        return false;
                default:
                    resul = string.Compare(stgizq, stgder);
                    if (resul == 0)
                        return false;
                    else
                        return true;
            }
        }

    }
}
