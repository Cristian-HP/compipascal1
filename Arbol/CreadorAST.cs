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
                LinkedList<Funcion> funciones = new LinkedList<Funcion>();
                LinkedList<Metodo> metodos = new LinkedList<Metodo>();
                LinkedList<Instruccion> instrucciones = new LinkedList<Instruccion>();
                foreach(Instruccion inst in temp)
                {
                    if(inst is Funcion )
                    {
                        funciones.AddLast((Funcion)inst);
                    }else if(inst is Metodo)
                    {
                        metodos.AddLast((Metodo)inst);
                    }
                    else
                    {
                        instrucciones.AddLast(inst); 
                    }
                }
                return new AST(instrucciones,funciones,metodos);
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

            }
            else if (equalnode(current, "LISTA_PARTES"))
            {
                LinkedList<Group> partes = new LinkedList<Group>();
                foreach (ParseTreeNode hijo in current.ChildNodes)
                {
                    partes.AddLast((Group)analisisnodo(hijo));
                }
                return partes;
            }
            else if (equalnode(current, "PARTE"))
            {
                return analisisnodo(current.ChildNodes[0]);
            }
            else if (equalnode(current, "DECLARACIONES"))
            {
                return new Group((LinkedList < Instruccion >) analisisnodo(current.ChildNodes[1]), current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
            }
            else if(equalnode(current, "CONSTANTE_GLO"))
            {
                return new Group((LinkedList<Instruccion>)analisisnodo(current.ChildNodes[1]), current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
            }
            else if (equalnode(current, "LISTA_VARIABLE"))
            {
                LinkedList<Instruccion> lisdecla = new LinkedList<Instruccion>();
                foreach (ParseTreeNode hijo in current.ChildNodes)
                {
                    lisdecla.AddLast((Instruccion)analisisnodo(hijo));
                }
                return lisdecla;
            }
            else if (equalnode(current, "VARIABLE"))
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
            }
            else if (equalnode(current, "MAIN"))
            {
                return new Group((LinkedList<Instruccion>)analisisnodo(current.ChildNodes[1]),current.ChildNodes[0].Token.Location.Line,current.ChildNodes[0].Token.Location.Column);
            }
            else if (equalnode(current, "LISTA_INTS"))
            {
                LinkedList<Instruccion> intruc = new LinkedList<Instruccion>();
                foreach(ParseTreeNode hijo in current.ChildNodes)
                {
                    intruc.AddLast((Instruccion)analisisnodo(hijo));
                }
                return intruc;
            }
            else if (equalnode(current, "INST"))
            {
                return analisisnodo(current.ChildNodes[0]);
            }
            else if (equalnode(current, "WRT"))
            {
                bool jump = false;
                if (current.ChildNodes[0].Token.Text.Equals("writeln", StringComparison.InvariantCultureIgnoreCase))
                    jump = true;
                return new Writeln((LinkedList<Expresion>) analisisnodo(current.ChildNodes[2]),jump,current.ChildNodes[0].Token.Location.Line,current.ChildNodes[0].Token.Location.Column);
            }
            else if (equalnode(current, "L_EXP"))
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
            }
            else if (equalnode(current, "ERELA"))
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
            }
            else if (equalnode(current, "EARI"))
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
            }
            else if (equalnode(current, "LISTA_ID"))
            {
                LinkedList<Simbolos> variables = new LinkedList<Simbolos>();
                Simbolos aux1;
                foreach (ParseTreeNode hijo in current.ChildNodes)
                {
                    aux1 = new Simbolos(null, null, obtenerid(hijo).ToLower());
                    variables.AddLast(aux1);
                }
                return variables;
            }
            else if (equalnode(current, "TIPO_CONID"))
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
                    else if (aux2.Token.Text.Equals("real", StringComparison.InvariantCultureIgnoreCase))
                        return new Tipo(Tipos.REAL, "");
                    else if (aux2.Token.Text.Equals("boolean", StringComparison.InvariantCultureIgnoreCase))
                        return new Tipo(Tipos.BOOLEAN, "");
                }
            }
            else if (equalnode(current, "IF"))
            {
                if(current.ChildNodes.Count == 4)
                {
                    if (current.ChildNodes[3].Term.Name.Equals("INST",StringComparison.InvariantCultureIgnoreCase))
                    {
                        LinkedList<Instruccion> instrucciones = new LinkedList<Instruccion>();
                        Expresion valor = (Expresion)analisisnodo(current.ChildNodes[1]);
                        Instruccion instrucc = (Instruccion)analisisnodo(current.ChildNodes[3]);
                        instrucciones.AddLast(instrucc);
                        return new IF(valor, instrucciones, null, current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                    }
                    else
                    {
                        LinkedList<Instruccion> instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[3]);
                        Expresion valor = (Expresion)analisisnodo(current.ChildNodes[1]);
                        return new IF(valor, instrucciones, null, current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                    }
                }else if (current.ChildNodes.Count == 5)
                {
                    LinkedList<Instruccion> instrucciones = new LinkedList<Instruccion>();
                    Expresion valor = (Expresion)analisisnodo(current.ChildNodes[1]);
                    Instruccion instrucc = (Instruccion)analisisnodo(current.ChildNodes[3]);
                    instrucciones.AddLast(instrucc);
                    Instruccion inselse = (Instruccion)analisisnodo(current.ChildNodes[4]);
                    return new IF(valor, instrucciones, inselse, current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                }
                else
                {
                    LinkedList<Instruccion> instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[4]);
                    Expresion valor = (Expresion)analisisnodo(current.ChildNodes[1]);
                    Instruccion inselse = (Instruccion)analisisnodo(current.ChildNodes[6]);
                    return new IF(valor, instrucciones, inselse, current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                }
            }
            else if (equalnode(current, "ELSE"))
            {
                if (current.ChildNodes[1].Term.Name.Equals("INST", StringComparison.InvariantCulture))
                {
                    LinkedList<Instruccion> instrucciones = new LinkedList<Instruccion>();
                    Instruccion instrucc = (Instruccion)analisisnodo(current.ChildNodes[1]);
                    instrucciones.AddLast(instrucc);
                    return new ELSE(instrucciones, current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                }
                else
                {
                    LinkedList<Instruccion> instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[1]);
                    return new ELSE(instrucciones,current.ChildNodes[0].Token.Location.Line,current.ChildNodes[0].Token.Location.Column);
                }
            }
            else if (equalnode(current, "BEGIN"))
            {
                return analisisnodo(current.ChildNodes[1]);
            }
            else if (equalnode(current, "WL"))
            {
                if (current.ChildNodes[3].Term.Name.Equals("INST", StringComparison.InvariantCulture))
                {
                    LinkedList<Instruccion> instrucciones = new LinkedList<Instruccion>();
                    Instruccion instrucc = (Instruccion)analisisnodo(current.ChildNodes[3]);
                    Expresion valor = (Expresion)analisisnodo(current.ChildNodes[1]);
                    instrucciones.AddLast(instrucc);
                    return new WHILE(instrucciones,valor ,current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                }
                else
                {
                    LinkedList<Instruccion> instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[3]);
                    Expresion valor = (Expresion)analisisnodo(current.ChildNodes[1]);
                    return new WHILE(instrucciones,valor ,current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                }
            }
            else if (equalnode(current, "ASIGN"))
            {
                Expresion valor = (Expresion)analisisnodo(current.ChildNodes[2]);
                Id idtem = new Id(obtenerid(current.ChildNodes[0]), current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                return new Asignacion(valor, idtem, current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
            }
            else if (equalnode(current, "FOR"))
            {
                int linea = current.ChildNodes[0].Token.Location.Line;
                int columna = current.ChildNodes[0].Token.Location.Column;
                Id idini = new Id(obtenerid(current.ChildNodes[1]), linea, current.ChildNodes[1].Token.Location.Column);
                Expresion inicio = (Expresion)analisisnodo(current.ChildNodes[3]);
                Expresion tope =(Expresion) analisisnodo(current.ChildNodes[5]);
                string direccion = current.ChildNodes[4].ChildNodes[0].Token.Text;
                if (current.ChildNodes[7].Term.Name.Equals("INST", StringComparison.InvariantCultureIgnoreCase))
                {
                    LinkedList<Instruccion> instrucciones = new LinkedList<Instruccion>();
                    Instruccion instrucc = (Instruccion)analisisnodo(current.ChildNodes[7]);
                    instrucciones.AddLast(instrucc);
                    return new FOR(idini,instrucciones, tope, direccion, inicio, linea, columna);
                }
                else
                {
                    LinkedList<Instruccion> instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[7]);
                    return new FOR(idini, instrucciones, tope, direccion, inicio, linea, columna);
                }
            }
            else if (equalnode(current, "BKR"))
            {
                return new Break(current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
            }
            else if (equalnode(current, "COT"))
            {
                return new Continue(current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
            }
            else if (equalnode(current, "CASE"))
            {
                Id temid = (Id)analisisnodo(current.ChildNodes[1]);
                LinkedList<Case> opciones = (LinkedList<Case>) analisisnodo(current.ChildNodes[3]);
                if(current.ChildNodes.Count == 7)
                {
                    ELSE temelse = (ELSE)analisisnodo(current.ChildNodes[4]);
                    return new Switch(temid, opciones, temelse, current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                }
                else
                {
                    return new Switch(temid,opciones,null,current.ChildNodes[0].Token.Location.Line,current.ChildNodes[0].Token.Location.Column);
                }

            }
            else if (equalnode(current, "L_OPC"))
            {
                LinkedList<Case> opciones = new LinkedList<Case>();
                Case temcase;
                foreach(ParseTreeNode hijo in current.ChildNodes)
                {
                    temcase =(Case) analisisnodo(hijo);
                    opciones.AddLast(temcase);
                }
                return opciones;
            }
            else if(equalnode(current, "OPC"))
            {
                LinkedList<Expresion> etiq = (LinkedList<Expresion>)analisisnodo(current.ChildNodes[0]);
                LinkedList<Instruccion> instruciones;
                if (current.ChildNodes[2].Term.Name.Equals("INST", StringComparison.InvariantCultureIgnoreCase))
                {
                    instruciones = new LinkedList<Instruccion>();
                    Instruccion temp = (Instruccion)analisisnodo(current.ChildNodes[2]);
                }
                instruciones =(LinkedList<Instruccion>) analisisnodo(current.ChildNodes[2]);
                return new Case(etiq, instruciones, current.ChildNodes[1].Token.Location.Line, current.ChildNodes[1].Token.Location.Column);

            }
            else if(equalnode(current, "L_EBC"))
            {
                LinkedList<Expresion> listaeti = new LinkedList<Expresion>();
                Expresion temexp;
                foreach(ParseTreeNode hijo in current.ChildNodes)
                {
                    temexp = (Expresion)analisisnodo(hijo);
                    listaeti.AddLast(temexp);
                }
                return listaeti;
            }
            else if (equalnode(current, "EBC"))
            {
                if(current.ChildNodes.Count == 2)
                {
                    int temp = int.Parse(current.ChildNodes[1].Token.Text);
                    temp = temp * -1;
                    return new Literal(temp, current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
                }
                return analisisnodo(current.ChildNodes[0]);
            }
            else if (equalnode(current, "REP"))
            {
                Expresion condi = (Expresion)analisisnodo(current.ChildNodes[3]);
                LinkedList<Instruccion> instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[1]);
                return new Repeat(condi, instrucciones, current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column);
            }
            else if (equalnode(current, "CONSTANTE"))
            {
                Simbolos aux1 = new Simbolos(null, null, obtenerid(current.ChildNodes[0]).ToLower(),true);
                Expresion valor =(Expresion) analisisnodo(current.ChildNodes[2]);
                return new Constante(valor, aux1, current.ChildNodes[1].Token.Location.Line, current.ChildNodes[1].Token.Location.Column);
            }
            else if(equalnode(current, "LISTA_CONSTANTE"))
            {
                LinkedList<Instruccion> constantes = new LinkedList<Instruccion>();
                Constante auxconst;
                foreach(ParseTreeNode hijo in current.ChildNodes)
                {
                    auxconst = (Constante)analisisnodo(hijo);
                    constantes.AddLast(auxconst);
                }
                return constantes;
            }
            else if(equalnode(current, "LISTA_FUNPRO"))
            {
                LinkedList<Instruccion> funopro = new LinkedList<Instruccion>();
                LinkedList<Instruccion> temp1;
                foreach(ParseTreeNode hijo in current.ChildNodes)
                {
                    temp1 = (LinkedList<Instruccion>)analisisnodo(hijo);
                    foreach(Instruccion child in temp1)
                    {
                        funopro.AddLast(child);
                    }
                }
                return new Group(funopro, 0, 0);
            }
            else if (equalnode(current, "FUNPRO"))
            {
                return analisisnodo(current.ChildNodes[0]);
            }
            else if (equalnode(current, "PROCEDIMIENTO"))
            {
                string idfun = current.ChildNodes[1].Token.Text.ToLower();
                LinkedList<Instruccion> instrucciones;
                int linea = current.ChildNodes[0].Token.Location.Line;
                int columna = current.ChildNodes[0].Token.Location.Column;
                //apartado para la declaracion de los metodos en la tabla
                Simbolos aux1 = new Simbolos(null, null, idfun);
                LinkedList<Simbolos> simbolos = new LinkedList<Simbolos>();
                simbolos.AddLast(aux1);
                Tipo tipomet = new Tipo(Tipos.VOID, "");
                LinkedList<Instruccion> Totaldeclafun = new LinkedList<Instruccion>();
                //termino de la declaracion
                if (current.ChildNodes.Count == 7)
                {
                    LinkedList<Simbolos> parametros = (LinkedList<Simbolos>)analisisnodo(current.ChildNodes[3]);
                    instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[6]);
                    Metodo mymet= new Metodo(linea,columna,idfun,parametros,instrucciones);
                    Declaracion nombremet = new Declaracion(simbolos, tipomet, linea, columna);
                    Totaldeclafun.AddLast(nombremet);
                    Totaldeclafun.AddLast(mymet);
                    return Totaldeclafun;
                }
                else if (current.ChildNodes.Count == 6)
                {
                    instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[5]);
                    Metodo mymet = new Metodo(linea,columna,idfun,null,instrucciones);
                    Declaracion nombremet = new Declaracion(simbolos, tipomet, linea, columna);
                    Totaldeclafun.AddLast(nombremet);
                    Totaldeclafun.AddLast(mymet);
                    return Totaldeclafun;
                }
                else
                {
                    instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[3]);
                    Metodo mymet= new Metodo(linea,columna,idfun,null,instrucciones);
                    Declaracion nombremet = new Declaracion(simbolos, tipomet, linea, columna);
                    Totaldeclafun.AddLast(nombremet);
                    Totaldeclafun.AddLast(mymet);
                    return Totaldeclafun;
                }
            }
            else if(equalnode(current, "FUNCIONES"))
            {
                string idfun = current.ChildNodes[1].Token.Text.ToLower();
                Simbolos aux1 = new Simbolos(null, null, idfun);
                LinkedList<Simbolos> simbolos = new LinkedList<Simbolos>();
                simbolos.AddLast(aux1);
                Tipo tipofun;
                LinkedList<Instruccion> instrucciones;
                int linea = current.ChildNodes[0].Token.Location.Line;
                int columna = current.ChildNodes[0].Token.Location.Column;
                LinkedList<Instruccion> Totaldeclafun = new LinkedList<Instruccion>();
                if(current.ChildNodes.Count == 9)
                {
                    tipofun = (Tipo)analisisnodo(current.ChildNodes[6]);
                    LinkedList<Simbolos> parametros = (LinkedList<Simbolos>)analisisnodo(current.ChildNodes[3]);
                    instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[8]);
                    Declaracion nombrefun= new Declaracion(simbolos, tipofun, linea, columna);
                    Funcion myfun= new Funcion(idfun, parametros, instrucciones, tipofun, linea, columna);
                    Totaldeclafun.AddLast(nombrefun);
                    Totaldeclafun.AddLast(myfun);
                    return Totaldeclafun;
                }else if(current.ChildNodes.Count == 8)
                {
                    tipofun = (Tipo)analisisnodo(current.ChildNodes[5]);
                    instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[7]);
                    Declaracion nombrefun = new Declaracion(simbolos, tipofun, linea, columna);
                    Funcion myfun = new Funcion(idfun, null, instrucciones, tipofun, linea, columna);
                    Totaldeclafun.AddLast(nombrefun);
                    Totaldeclafun.AddLast(myfun);
                    return Totaldeclafun;
                }
                else
                {
                    tipofun = (Tipo)analisisnodo(current.ChildNodes[3]);
                    instrucciones = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[5]);
                    Declaracion nombrefun = new Declaracion(simbolos, tipofun, linea, columna);
                    Funcion myfun= new Funcion(idfun, null, instrucciones, tipofun, linea, columna);
                    Totaldeclafun.AddLast(nombrefun);
                    Totaldeclafun.AddLast(myfun);
                    return Totaldeclafun;

                }
            }
            else if(equalnode(current, "L_PARAM"))
            {
                LinkedList<Simbolos> listaparam = new LinkedList<Simbolos>();
                LinkedList<Simbolos> param;
                foreach(ParseTreeNode hijo in current.ChildNodes)
                {
                    param = (LinkedList<Simbolos>)analisisnodo(hijo);
                    foreach(Simbolos simbol in param)
                    {
                        listaparam.AddLast(simbol);
                    }
                }
                return listaparam;
            }
            else if(equalnode(current, "PARAM"))
            {
                if(current.ChildNodes.Count == 4)
                {
                    //por referencia
                    return null;
                }
                else
                {
                    LinkedList<Simbolos> varia = (LinkedList<Simbolos>)analisisnodo(current.ChildNodes[0]);
                    Tipo tipo = (Tipo)analisisnodo(current.ChildNodes[2]);
                    foreach(Simbolos auxtipo in varia)
                    {
                        auxtipo.tipo = tipo;
                    }
                    return varia;
                }
               
            }
            else if (equalnode(current, "BODY"))
            {
                if(current.ChildNodes.Count == 2)
                {
                    LinkedList<Instruccion> dec = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[0]);
                    LinkedList<Instruccion> insts = (LinkedList<Instruccion>)analisisnodo(current.ChildNodes[1]);
                    foreach(Instruccion inst in insts)
                    {
                        dec.AddLast(inst);
                    }
                    return dec;
                   
                }
                else
                {
                    return analisisnodo(current.ChildNodes[0]);
                }
            }
            else if(equalnode(current, "L_DEF"))
            {
                LinkedList<Instruccion> todas = new LinkedList<Instruccion>();
                Group temlis;
                foreach(ParseTreeNode hijo in current.ChildNodes)
                {
                    temlis = (Group)analisisnodo(hijo);
                    iterarbloque(todas, temlis);
                }
                return todas;
            }
            else if (equalnode(current,"DEF")){
                return analisisnodo(current.ChildNodes[0]);
            }
            else if(equalnode(current, "EXT"))
            {
                return new Exit(current.ChildNodes[0].Token.Location.Line, current.ChildNodes[0].Token.Location.Column, (Expresion)analisisnodo(current.ChildNodes[2]));
            }
            else if (equalnode(current, "LLAMA"))
            {
                int linea = current.ChildNodes[0].Token.Location.Line;
                int columna = current.ChildNodes[0].Token.Location.Column;
                string idfun = current.ChildNodes[0].Token.Text.ToLower();
                if(current.ChildNodes.Count == 5)
                {
                    LinkedList<Expresion> expresio = (LinkedList<Expresion>)analisisnodo(current.ChildNodes[2]);
                    return new Llama(idfun, expresio, linea, columna);
                }else if(current.ChildNodes.Count == 4)
                {
                    if (current.ChildNodes[3].Token.Text.Equals(")"))
                    {
                        LinkedList<Expresion> expresio = (LinkedList<Expresion>)analisisnodo(current.ChildNodes[2]);
                        return new Llama(idfun, expresio, linea, columna);
                    }else
                        return new Llama(idfun, null, linea, columna);
                }
                else
                {
                    return new Llama(idfun, null, linea, columna);
                }
            }
            else if(equalnode(current, "GTS"))
            {
                return new Graficarts();
            }
            else if (equaliteral(current))
            {
                return new Literal(obtenerliteral(current),current.Token.Location.Line,current.Token.Location.Column);

            }
            else if (equalid(current))
            {
                return new Id(obtenerid(current),current.Token.Location.Line,current.Token.Location.Column);
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
