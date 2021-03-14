using compipascal1.Simbolo;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace compipascal1.abstracta
{
    class Erroresglo
    {

        public LinkedList<Errorp> errores { get; set; } 
        public Erroresglo()
        {
            errores = new LinkedList<Errorp>();
        }

        public void adderr(Errorp er)
        {
            errores.AddLast(er);
        }
        public void graficar()
        {
            string errorestext = "<html>\n <body> <h2>Errores proyecto 1</h2> <table style=\"width:100%\" border=\"1\"> <tr> <th>Tipo</th><th>Ambito</th> <th>Descripcion del error</th><th>Linea</th> <th>Columna</th></tr> \n";
            foreach (Errorp elerror in errores)
            {
                errorestext += "<tr>" +
                   "<td>" + elerror.tipo +
                   "</td>" +
                   "<td>"+elerror.ambito+
                   "</td>"+
                   "<td>" + elerror.msg +
                   "</td>" +
                   "<td>" + elerror.Linea +
                   "</td>" +
                   "<td>" + elerror.Columna +
                   "</td>" +
                   "</tr>";
            }
            errorestext += "</table> </body> </html>";
            using (StreamWriter outputFile = new StreamWriter("C:\\compiladores2\\reporteErrores.html"))
            {
                outputFile.WriteLine(errorestext);
            }
        }
    }
}
