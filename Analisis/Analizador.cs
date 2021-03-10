using compipascal1.abstracta;
using compipascal1.Arbol;
using compipascal1.Simbolo;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace compipascal1.Analisis
{
    class Analizador
    {

        public void analizar(string cadena)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            foreach (var item in lenguaje.Errors)
            {
                Console.WriteLine(item);
            }

            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;
            if (raiz == null)
            {
                Console.WriteLine(arbol.ParserMessages[0].Message);
                return;
            }
            Console.WriteLine("pasola peuba2");
            generarGrafo(raiz);
            Console.WriteLine("paso la prueba");

            ejecucion(arbol);

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void ejecucion(ParseTree tree)
        {
            CreadorAST arbolgenerado = new CreadorAST(tree);
            AST ast = arbolgenerado.mytree;
            Entorno ent = new Entorno(null);
            if(ast != null)
            {
                foreach(Instruccion inst in ast.instrucciones)
                {
                    inst.Ejecutar(ent, ast);
                }
            }

        }
    }
}
