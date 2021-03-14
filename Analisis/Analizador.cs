using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace compipascal1.Analisis
{
    class Analizador
    {
        Erroresglo miserrores = new Erroresglo();
        ParseTreeNode raiz;
        public void analizar(string cadena)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            raiz = arbol.Root;

            bool vefica = verificarerrores(arbol, raiz);
            if (vefica)
            {    
                ejecucion(arbol,miserrores);
            }
        }
        public void generarGrafo(ParseTreeNode raiz)
        {
            string grafoDot = Graficador.getDot(raiz);
            string path = "C:\\compiladores2\\ast.dot";
            try
            {
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(grafoDot);
                    fs.Write(info, 0, info.Length);
                }
                Thread.Sleep(2000);
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dot.exe",
                    Arguments = "-Tpng C:\\compiladores2\\ast.dot -o C:\\compiladores2\\AST.png",
                    UseShellExecute = false
                };
                Process.Start(startInfo);

                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void generarGrafoF()
        {
            generarGrafo(raiz);
        }
        public static void ejecucion(ParseTree tree,Erroresglo elherror)
        {
            CreadorAST arbolgenerado = new CreadorAST(tree);
            AST ast = arbolgenerado.mytree;
            Entorno ent = new Entorno(null,"GLOBAL");
            if (ast != null)
            {
                foreach(Instruccion inst in ast.instrucciones)
                {
                    inst.Ejecutar(ent, ast, elherror);
                }
            }

        }

        public void graficaerrores()
        {
            miserrores.graficar();
        }
        private bool verificarerrores(ParseTree arbol,ParseTreeNode raiz)
        {
            if(raiz == null)
            {
                Errorp mi = new Errorp(0, 0, "ERROR Fatal No se Pudo Recuperar El analizador", "Analizador","Gramatica");
                miserrores.adderr(mi);
                Form1.errorcon.AppendText(mi.ToString() + "\n");
                return false;
            }else if(arbol.ParserMessages.Count > 0)
            {
                string tipoerro;
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {
                    if (arbol.ParserMessages[i].Message.Contains("Syntax"))
                        tipoerro = "Sintactico";
                    else
                        tipoerro = "Lexico";
                    Errorp mier = new Errorp(arbol.ParserMessages[i].Location.Line,arbol.ParserMessages[i].Location.Column,arbol.ParserMessages[i].Message,tipoerro,"Gramatica");
                    miserrores.adderr(mier);
                    Form1.errorcon.AppendText(mier.ToString() + "\n");
                }
                return false;
            }else if(raiz != null)
            {
                return true;
            }
            return false;
        }
    }
}
