using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Instrucciones
{
    class FOR : Instruccion
    {
        private LinkedList<Instruccion> instrucciones;
        private Expresion tope;
        private string direcion;
        private Expresion inicio;
        private Expresion id;
        private Simbolos idt;
        private int topeint;
        private int pivote;

        public int Linea { get; set; }
        public int Columna { get; set; }

        public  object Ejecutar(Entorno ent, AST tree)
        {
            
            try
            {
                Simbolos ini = inicio.resolver(ent, tree);
                Simbolos top = tope.resolver(ent, tree);
                idt = id.resolver(ent, tree);
                if (ini.tipo.tipo != Tipos.INTEGER || ini.tipo.tipo != top.tipo.tipo)
                    throw new Errorp(Linea, Columna, "No es posible ejecutar la instruccion for dado que el tipo de dato no concuendan deben ser solo enteros", "Semantico");
                if (ini.tipo.tipo == idt.tipo.tipo)
                {
                    idt.valor = ini.valor;
                    ent.asignavariable(idt.id.ToString().ToLower(), idt);
                }
                else
                    throw new Errorp(Linea, Columna, "No es posible asignar un valor de tipo: " + ini.tipo.tipo.ToString() + " A una variable de  Tipo " + idt.tipo.tipo.ToString(), "Semantico");

                if (direcion.Equals("to", StringComparison.InvariantCultureIgnoreCase))
                {
                    topeint =int.Parse(top.valor.ToString());
                    pivote = int.Parse(ini.valor.ToString());
                    while (topeint >= pivote)
                    {
                        foreach (Instruccion inst in instrucciones)
                        {
                            if (inst is Break)
                            {
                                return null;
                            }else if(inst is Continue)
                            {
                                break;
                            }else if(inst is Exit)
                            {
                                return inst;
                            }
                            else
                            {
                                object resul = inst.Ejecutar(ent, tree);
                                if(resul is Break)
                                {
                                    return null;
                                }else if (resul is Continue)
                                {
                                    break;
                                }else if (resul is Exit)
                                {
                                    return resul;
                                }
                            }
                        }
                        if(pivote == topeint)
                        {
                            break;
                        }
                        else
                        {
                            updateid(ent);
                        }
                           
                    }
                    
                }
                else
                {

                    topeint = int.Parse(top.valor.ToString());
                    pivote = int.Parse(ini.valor.ToString());
                    while (topeint <= pivote)
                    {
                        foreach (Instruccion inst in instrucciones)
                        {
                            if (inst is Break)
                            {
                                return null;
                            }
                            else if (inst is Continue)
                            {
                                break;
                            }
                            else
                            {
                                object resul = inst.Ejecutar(ent, tree);
                                if (resul is Break)
                                {
                                    return null;
                                }
                                else if (resul is Continue)
                                {
                                    break;
                                }
                            }
                        }
                        if (pivote == topeint)
                        {
                            break;
                        }
                        else
                        {
                            updateid(ent);
                        }

                    }

                }
            }
            catch
            {
                //guardar el error
            }
            return null;
        }

        public FOR(Expresion id, LinkedList<Instruccion> instrucciones, Expresion tope, string direcion, Expresion inicio,int linea,int columna)
        {
            this.instrucciones = instrucciones;
            this.tope = tope;
            this.direcion = direcion;
            this.inicio = inicio;
            this.id = id;
            Linea = linea;
            Columna = columna;
        }

        private void updateid(Entorno ent)
        {
            if (direcion.Equals("to", StringComparison.InvariantCultureIgnoreCase))
            {
                idt = ent.obtenerVariable(idt.id.ToLower());
                pivote = int.Parse(idt.valor.ToString());
                pivote = pivote + 1;
                idt.valor = pivote;
                ent.asignavariable(idt.id.ToString().ToLower(), idt);
            }
            else
            {
                idt = ent.obtenerVariable(idt.id.ToLower());
                pivote = int.Parse(idt.valor.ToString());
                pivote = pivote - 1;
                idt.valor = pivote;
                ent.asignavariable(idt.id.ToString().ToLower(), idt);
            }
        }

    }
}
