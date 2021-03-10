using System;
using System.Collections.Generic;
using System.Text;
using compipascal1.abstracta;
using compipascal1.Expresiones;
using compipascal1.Instrucciones;
using compipascal1.Simbolo;
using Irony.Parsing;

namespace compipascal1.Arbol
{
    class CreadorAST
    {
        private ParseTree treeirony;

        public AST mytree { get; set; }

        public CreadorAST(ParseTree tree)
        {
            treeirony = tree;
            creador(treeirony.Root);
        }

        private void creador(ParseTreeNode root)
        {
            mytree = (AST)analisisnodo(root);
        }


        private object analisisnodo(ParseTreeNode current)
        {
            if (equalnode(current, "INICIO"))
            {
                LinkedList<Instruccion> temp = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[0]);
                return new AST(temp);
            }
            else if (equalnode(current, "PROGRAM"))
            {
                LinkedList<Instruccion> instrucc = new LinkedList<Instruccion>();

                if(current.ChildNodes.Count == 4)
                {
                    Group bloq = (Group)analisisnodo(current.ChildNodes[3]);
                    iterarbloque(instrucc, bloq);
                }else if (current.ChildNodes.Count == 5)
                {
                    LinkedList<Group> bloques =(LinkedList<Group>) analisisnodo(current.ChildNodes[3]);

                    foreach(Group bloque in bloques)
                    {
                        iterarbloque(instrucc, bloque);
                    }
                    iterarbloque(instrucc, (Group)analisisnodo(current.ChildNodes[4]));
                }

                return instrucc;

            }else if (equalnode(current, "LISTA_PARTES"))
            {
                LinkedList<Group> partes = new LinkedList<Group>();
                foreach (ParseTreeNode hijo in current.ChildNodes)
                {
                    partes.AddLast((Group)analisisnodo(hijo));
                }
                return partes;
            }else if (equalnode(current, "PARTE"))
            {
                return analisisnodo(current.ChildNodes[0]);
            }else if (equalnode(current, "DECLARACIONES"))
            {
                return new Group((LinkedList < Instruccion >) analisisnodo(current.ChildNodes[1]), current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
            }else if (equalnode(current, "LISTA_VARIABLE"))
            {
                LinkedList<Instruccion> lisdecla = new LinkedList<Instruccion>();
                foreach (ParseTreeNode hijo in current.ChildNodes)
                {
                    lisdecla.AddLast((Instruccion)analisisnodo(hijo));
                }
                return lisdecla;
            }else if (equalnode(current, "VARIABLE"))
            {
                LinkedList<Simbolos> variables;
                Tipo tipo;
                if (current.ChildNodes.Count == 4)
                {
                    variables =(LinkedList<Simbolos>) analisisnodo(current.ChildNodes[0]);
                    tipo = (Tipo)analisisnodo(current.ChildNodes[2]);
                    return new Declaracion(variables,tipo,current.ChildNodes[1].Token.Location.Line,current.ChildNodes[1].Token.Location.Column);
                }else if (current.ChildNodes.Count == 6)
                {
                    variables = (LinkedList<Simbolos>)analisisnodo(current.ChildNodes[0]);
                    tipo = (Tipo)analisisnodo(current.ChildNodes[2]);
                    Expresion valor = (Expresion)analisisnodo(current.ChildNodes[4]);
                    return new Declaracion(variables,tipo,valor,current.ChildNodes[1].Token.Location.Line,current.ChildNodes[1].Token.Location.Column);
                }
            }else if (equalnode(current, "MAIN"))
            {
                return new Group((LinkedList<Instruccion>)analisisnodo(current.ChildNodes[1]),current.ChildNodes[0].Token.Location.Line,current.ChildNodes[0].Token.Location.Column);
            }else if (equalnode(current, "LISTA_INTS"))
            {
                LinkedList<Instruccion> intruc = new LinkedList<Instruccion>();
                foreach(ParseTreeNode hijo in current.ChildNodes)
                {
                    intruc.AddLast((Instruccion)analisisnodo(hijo));
                }
                return intruc;
            }else if (equalnode(current, "INST"))
            {
                return analisisnodo(current.ChildNodes[0]);
            }else if (equalnode(current, "WRT"))
            {
                return new Writeln((LinkedList<Expresion>) analisisnodo(current.ChildNodes[2]),true,current.ChildNodes[0].Token.Location.Line,current.ChildNodes[0].Token.Location.Column);
            }else if (equalnode(current, "L_EXP"))
            {
                LinkedList<Expresion> expresioness = new LinkedList<Expresion>();
                foreach (ParseTreeNode hijo in current.ChildNodes)
                {
                    expresioness.AddLast((Expresion)analisisnodo(hijo));
                }
                return expresioness;
            }
            else if (equalnode(current, "ELOG"))
            {
                if(current.ChildNodes.Count == 3)
                {
                    if (current.ChildNodes[0].Term.Name.Equals("("))
                        return analisisnodo(current.ChildNodes[1]);
                    else
                    {
                        Expresion izq = (Expresion)analisisnodo(current.ChildNodes[0]);
                        string tip = current.ChildNodes[1].Token.Text;
                        Expresion der = (Expresion)analisisnodo(current.ChildNodes[2]);
                        return new Logica(izq, der, tip, current.ChildNodes[1].Token.Location.Line, current.ChildNodes[1].Token.Location.Column);
                    }
                }
                else if (current.ChildNodes.Count == 2)
                {
                    Expresion izq = (Expresion)analisisnodo(current.ChildNodes[1]);
                    return new Logica(izq, "n", current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                }
                else
                {
                    return analisisnodo(current.ChildNodes[0]);
                }
            }else if (equalnode(current, "ERELA"))
            {
                if(current.ChildNodes.Count == 3)
                {
                    if (current.ChildNodes[0].Term.Name.Equals("("))
                        return analisisnodo(current.ChildNodes[1]);
                    else
                    {
                        Expresion izq = (Expresion)analisisnodo(current.ChildNodes[0]);
                        string tip = current.ChildNodes[1].Token.Text;
                        Expresion der = (Expresion)analisisnodo(current.ChildNodes[2]);
                        return new Relacional(izq,der,tip,current.ChildNodes[1].Token.Location.Line,current.ChildNodes[1].Token.Location.Column);
                    }
                }
                else
                {
                    return analisisnodo(current.ChildNodes[0]);
                }
            }else if (equalnode(current, "EARI"))
            {
                if(current.ChildNodes.Count == 3)
                {
                    if (current.ChildNodes[0].Term.Name.Equals("("))
                        return analisisnodo(current.ChildNodes[1]);
                    else
                    {
                        Expresion izq = (Expresion)analisisnodo(current.ChildNodes[0]);
                        string tip = current.ChildNodes[1].Token.Text;
                        Expresion der = (Expresion)analisisnodo(current.ChildNodes[2]);
                        return new Arimetica(izq, der, tip, current.ChildNodes[1].Token.Location.Line, current.ChildNodes[1].Token.Location.Column);
                    }
                }
                else if(current.ChildNodes.Count == 2)
                {
                    Expresion izq = (Expresion)analisisnodo(current.ChildNodes[1]);
                    //string tip = current.ChildNodes[1].Token.Text;
                    return new Arimetica(izq, "-", current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                }
                else
                {
                    return analisisnodo(current.ChildNodes[0]);
                }
            }else if (equalnode(current, "LISTA_ID"))
            {
                LinkedList<Simbolos> variables = new LinkedList<Simbolos>();
                Simbolos aux1;
                foreach (ParseTreeNode hijo in current.ChildNodes)
                {
                    aux1 = new Simbolos(null, null, obtenerid(hijo).ToLower());
                    variables.AddLast(aux1);
                }
                return variables;
            }else if (equalnode(current, "TIPO_CONID"))
            {
                if (equalid(current.ChildNodes[0]))
                    return new Tipo(Tipos.OBJECT, obtenerid(current.ChildNodes[0]).ToLower());
                else
                {
                    ParseTreeNode aux2 = current.ChildNodes[0].ChildNodes[0];
                    if (aux2.Token.Text.Equals("integer", StringComparison.InvariantCultureIgnoreCase))
                        return new Tipo(Tipos.INTEGER, "");
                    else if (aux2.Token.Text.Equals("string", StringComparison.InvariantCultureIgnoreCase))
                        return new Tipo(Tipos.STRING, "");
                }
            }else if (equalnode(current, "WR"))
            {

            }
            else if (equaliteral(current))
            {
                return new Literal(obtenerliteral(current),current.Token.Location.Line,current.Token.Location.Column);

            }
            return null;
        }


        private bool equalnode(ParseTreeNode node , string name)
        {
            return node.Term.Name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase);
        }

        private void iterarbloque(LinkedList<Instruccion> temp, Group bloque )
        {
            LinkedList<Instruccion> instrucciones = bloque.instrucciones;
            foreach (Instruccion inst in instrucciones)
            {
                if (inst is Group)
                    iterarbloque(temp, (Group)inst);
                else
                    temp.AddLast(inst);
            }
        }

        private bool equaliteral(ParseTreeNode node)
        {
            if (node.ToString().EndsWith("(CADENA)") || node.ToString().EndsWith("(NUMBER)") || node.ToString().EndsWith("(BOOLEAN)"))
                return true;
            return false;
        }

        private object obtenerliteral(ParseTreeNode node)
        {
            // case de opciones 
            if (node.ToString().EndsWith("(CADENA)"))
            {
                return node.Token.Text.Replace("'", "");
            }else if (node.ToString().EndsWith("(NUMBER)"))
            {
                try
                {
                    return int.Parse(node.Token.Text);
                }catch
                {
                    return double.Parse(node.Token.Text);
                }
            }else
            {
                if (node.Token.Text.Equals("true", StringComparison.InvariantCultureIgnoreCase)) return true;
                else return false;
            }
        }


        private string obtenerid(ParseTreeNode node)
        {
            return node.Token.Text.ToString();
        }

        private bool equalid(ParseTreeNode node)
        {
            return node.ToString().EndsWith("(ID)");
        }
    }
}
