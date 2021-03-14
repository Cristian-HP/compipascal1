using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace compipascal1.Simbolo
{
    class Entorno
    {
        Dictionary<string, Simbolos> variables;
        Entorno padre;
        LinkedList<int> lineas = new LinkedList<int>();
        LinkedList<int> columnas = new LinkedList<int>();
        public string nombre { get; set; }
        public Entorno(Entorno padre,String nombre)
        {
            this.padre = padre;
            this.nombre = nombre;
            this.variables = new Dictionary<string, Simbolos>();

        }

        public void declararVariable(string id, Simbolos variable,int linea,int columna)
        {
            if (this.variables.ContainsKey(id))
            {
                throw new Exception("La variable " + id + "ya existe en este ambito");
            }
            else
            {    
                this.variables.Add(id, variable);
                this.lineas.AddLast(linea);
                this.columnas.AddLast(columna);
            }
        }

        public Simbolos obtenerVariable(string id)
        {
            id = id.ToLower();
            Entorno actual = this;
            while(actual != null)
            {
               
                if (actual.variables.ContainsKey(id))
                {
                    return actual.variables[id];
                }
                actual = actual.padre;
            }

            return null;
        }

        public void asignavariable(string id,Simbolos variable)
        {
            Entorno actual = this;
            while (actual != null)
            {
                if (actual.variables.ContainsKey(id))
                {
                    actual.variables[id] = variable;
                    break;
                }
                actual = actual.padre;
            }
        }

        public void graficartabla()
        {
            string dottex = "digraph { \n  tbl [ \n    shape = plaintext \n    label =< \n ";
            dottex += "<table border ='0' cellborder ='1' color ='RED' cellspacing ='1' >";
            dottex += "<tr><td> Nombre </td><td> Tipo </td><td> Ambito </td><td> Fila </td><td> Columna </td></tr> ";
            for(int i=0;i<variables.Count;i++)
            {
                dottex += "<tr> \n ";
                dottex += "<td color ='blue' >" + variables.ElementAt(i).Key + "</td><td color ='blue'>"+variables.ElementAt(i).Value.tipo.tipo.ToString()+"</td><td color ='blue'>"+nombre;
                dottex += "</td><td color ='blue'>" + (lineas.ElementAt(i)+1)+"</td><td color ='blue'>" + columnas.ElementAt(i)+"</td>\n</tr>\n";
            }
            dottex += " </table> \n>]; \n}";

            string path = "C:\\compiladores2\\";
            string name = nombre;
            string temp = Path.Combine(path, name);
            temp = Path.ChangeExtension(temp, ".dot");
            string name2 = nombre + ".png";
            string temp2 = Path.Combine(path, name2);
            try
            {
                using (FileStream fs = File.Create(temp))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(dottex);
                    fs.Write(info, 0, info.Length);
                }
                Thread.Sleep(2000);
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dot.exe",
                    Arguments = "-Tpng "+ temp+ " -o "+temp2,
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
    }
}
