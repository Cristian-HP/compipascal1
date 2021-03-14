using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Expresiones
{
    class Arimetica : Expresion
    {
        public int Linea { get; set; }
        public int Columna { get; set; }
        public Expresion Izquierda { get; set; }
        public Expresion Derecha { get; set; }

        public string tipoope { get; set; }

        private bool unario;
        public  Simbolos resolver(Entorno ent, AST tree)
        {
            if (unario)
            {
                Simbolos unariotemp = Izquierda.resolver(ent, tree);
                if(unariotemp.tipo.tipo == Tipos.INTEGER)
                {
                    return new Simbolos(int.Parse(unariotemp.ToString())*-1, new Tipo(unariotemp.tipo.tipo,""),"");
                }else if (unariotemp.tipo.tipo == Tipos.REAL)
                {
                    return new Simbolos(double.Parse(unariotemp.ToString()) * -1, new Tipo(unariotemp.tipo.tipo, ""), "");
                }
                else
                {
                    throw new Errorp(Linea, Columna, "No se puede realizar la operacion unaria " + palabraope()+" con el valor de tipo "+unariotemp.tipo.ToString(), "Semantico");
                }
            }
            else
            {
                Simbolos izq = Izquierda.resolver(ent, tree);
                Simbolos der = Derecha.resolver(ent, tree);
                Tipos tiporesul = Tablatipos.getTipo(izq.tipo,der.tipo,tipoope);
                if (tiporesul == Tipos.ERROR)
                    throw new Errorp(Linea,Columna,"No se puede realizar la operacion "+palabraope()+" entre "+izq.tipo.ToString()+" y un "+der.tipo.ToString() ,"Semantico");

                Tipo temptipo = new Tipo(tiporesul, null);
                Simbolos resul;

         
                switch (tipoope)
                {
                    case "+":
                        if(tiporesul == Tipos.INTEGER)
                        {
                            resul = new Simbolos(int.Parse(izq.valor.ToString()) + int.Parse(der.valor.ToString()),temptipo,"" );
                            return resul;
                        }else if(tiporesul == Tipos.REAL)
                        {
                            resul = new Simbolos(double.Parse(izq.valor.ToString()) + double.Parse(der.valor.ToString()), temptipo, "");
                            return resul;
                        }
                        else
                        {
                            resul = new Simbolos(izq.valor.ToString() + der.valor.ToString(), temptipo, "");
                            return resul;
                        }
                    case "-":
                        if (tiporesul == Tipos.INTEGER)
                        {
                            resul = new Simbolos(int.Parse(izq.valor.ToString()) - int.Parse(der.valor.ToString()), temptipo, "");
                            return resul;
                        }
                        else
                        {
                            resul = new Simbolos(double.Parse(izq.valor.ToString()) - double.Parse(der.valor.ToString()), temptipo, "");
                            return resul;
                        }
                    case "/":
                        if (der.valor.ToString().Equals("0"))
                        {
                            throw  new Errorp(Linea,Columna,"Indefinicion, No es posible la division entre '0'","Semantico");
                        }
                        else if (tiporesul == Tipos.INTEGER)
                        {
                            resul = new Simbolos(int.Parse(izq.valor.ToString()) / int.Parse(der.valor.ToString()), temptipo, "");
                            return resul;
                        }
                        else
                        {
                            resul = new Simbolos(double.Parse(izq.valor.ToString()) / double.Parse(der.valor.ToString()), temptipo, "");
                            return resul;
                        }
                    case "%":
                        if (der.valor.ToString().Equals("0"))
                        {
                            throw new Errorp(Linea, Columna, "Indefinicion, No es posible la division entre '0'", "Semantico");
                        }
                        resul = new Simbolos(int.Parse(izq.valor.ToString()) % int.Parse(der.valor.ToString()), temptipo, "");
                        return resul;
                    default:
                        if (tiporesul == Tipos.INTEGER)
                        {
                            resul = new Simbolos(int.Parse(izq.valor.ToString()) * int.Parse(der.valor.ToString()), temptipo, "");
                            return resul;
                        }
                        else
                        {
                            resul = new Simbolos(double.Parse(izq.valor.ToString()) * double.Parse(der.valor.ToString()), temptipo, "");
                            return resul;
                        }
                }
            }
        }

        public Arimetica(Expresion izquierda, Expresion derecha, string tipo, int linea, int columna)
        {
            this.Izquierda = izquierda;
            this.Derecha = derecha;
            this.tipoope = tipo;
            this.Linea = linea;
            this.Columna = columna;
            this.unario = false;
        }

        public Arimetica(Expresion izquierda, string tipo, int linea, int columna)
        {
            this.Izquierda = izquierda;
            this.tipoope = tipo;
            this.Linea = linea;
            this.Columna = columna;
            this.unario = true;
        }

        private string palabraope()
        {
            switch (tipoope)
            {
                case "+":
                    return "Suma";
                case "-":
                    return "Resta";
                case "*":
                    return "Multiplicacion";
                case "/":
                    return "Division";
                case "%":
                    return "Modulo";
                default:
                    return "Negacion";
       
            }
        }
    }
}
