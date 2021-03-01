using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace compipascal1.Analisis
{
    class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: false)
        {
            #region ER
            //primitivos
            NumberLiteral entero = new NumberLiteral("INT");
            StringLiteral cadena = new StringLiteral("CADENA","'");
            RegexBasedTerminal doble = new RegexBasedTerminal("DOUBLE","[0-9]+'.'[0-9]+");
            RegexBasedTerminal bolean = new RegexBasedTerminal("BOOLEAN", "true|false");
            IdentifierTerminal id = new IdentifierTerminal("ID");

            //Comentarios
            CommentTerminal comentariolinea = new CommentTerminal("COMENTARIOLINEA", "//", "\n", "\r\n");
            CommentTerminal comentariomulti1 = new CommentTerminal("COMENTARIOMULTI1", "(*", "*)");
            CommentTerminal comentariomulti2 = new CommentTerminal("COMENTARIOMULTI2", "{", "}");

            #endregion

            #region Terminales
            //signos
            var coma = ToTerm(",");
            var parabre = ToTerm("(");
            var parcierra = ToTerm(")");
            var corabre = ToTerm("[");
            var corcierra = ToTerm("]");
            var ptocoma = ToTerm(";");
            var punto = ToTerm(".");
            var mayor = ToTerm(">");
            var menor = ToTerm("<");
            var menorigual = ToTerm("<=");
            var mayorigual = ToTerm(">=");
            var igual = ToTerm("=");
            var mas = ToTerm("+");
            var menos = ToTerm("-");
            var multi = ToTerm("*");
            var div = ToTerm("/");
            var mod = ToTerm("%");
            var desigual = ToTerm("<>");
            var dospto = ToTerm(":");
            var asig = ToTerm(":=");

            //palabras reservadas
            var robject = ToTerm("object");
            var rarray = ToTerm("array");
            var rinteger = ToTerm("integer");
            var rbreak = ToTerm("break");
            var rcotinue = ToTerm("continue");
            var rreal = ToTerm("real");
            var rwrite = ToTerm("write");
            var rstring = ToTerm("string");
            var rboolean = ToTerm("boolean");
            var writeln = ToTerm("writeln");


            #endregion

        }
    }
}
