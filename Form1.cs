using compipascal1.abstracta;
using compipascal1.Analisis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compipascal1
{
    public partial class Form1 : Form
    {
        public static RichTextBox consola2;
        public static RichTextBox errorcon;
        public static LinkedList<TablasimbolosRep> Tablasim;
        Analizador analizador = new Analizador();
        private int caracter;
        public Form1()
        {
            InitializeComponent();
            consola2 = consola;
            errorcon = outerror;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tablasim  = new LinkedList<TablasimbolosRep>();
            consola2.Text = "";
            errorcon.Text = "";
            String txt = richTextBox1.Text;
            analizador.analizar(txt);
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            analizador.graficaerrores();
            analizador.generarGrafoF();
            graficarTabla();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            caracter = 0;
            int altura = richTextBox1.GetPositionFromCharIndex(0).Y;
            if(richTextBox1.Lines.Length > 0)
            {
                for(int i=0; i < richTextBox1.Lines.Length; i++)
                {
                    e.Graphics.DrawString((i + 1).ToString(), richTextBox1.Font, Brushes.Blue, pictureBox1.Width - (e.Graphics.MeasureString((i + 1).ToString(), richTextBox1.Font).Width + 10), altura);
                    caracter += richTextBox1.Lines[i].Length + 1;
                    altura = richTextBox1.GetPositionFromCharIndex(caracter).Y;
                }
            }
            else
            {
                e.Graphics.DrawString((1).ToString(), richTextBox1.Font, Brushes.Blue, pictureBox1.Width - (e.Graphics.MeasureString((1).ToString(), richTextBox1.Font).Width + 10), altura);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.Title = "Seleccionar fichero";
            oFD.Filter = "Archivos Pascal (*.pas)|*.pas" +"|Todos los archivos (*.*)|*.*";
            oFD.FileName = this.richTextBox1.Text;
            if (oFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.richTextBox1.Text = System.IO.File.ReadAllText(oFD.FileName);
            }
        }

        private void graficarTabla()
        {
            string dottex = "digraph { \n  tbl [ \n    shape = plaintext \n    label =< \n ";
            dottex += "<table border ='0' cellborder ='1' color ='RED' cellspacing ='1' >";
            dottex += "<tr><td> Nombre </td><td> Tipo </td><td> Ambito </td><td> Fila </td><td> Columna </td></tr> ";
            for(int i = 0; i < Tablasim.Count; i++)
            {
                dottex += "<tr> \n ";
                dottex += "<td color ='blue' >" + Tablasim.ElementAt(i).nombre + "</td><td color ='blue'>" + Tablasim.ElementAt(i).tipo + "</td><td color ='blue'>" + Tablasim.ElementAt(i).Ambito;
                dottex += "</td><td color ='blue'>" + Tablasim.ElementAt(i).Linea + "</td><td color ='blue'>" + Tablasim.ElementAt(i).Columna + "</td>\n</tr>\n";
            }


            dottex += " </table> \n>]; \n}";
            string path = "C:\\compiladores2\\ReporteTablaSim.dot";
            try
            {
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(dottex);
                    fs.Write(info, 0, info.Length);
                }
                Thread.Sleep(2000);
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dot.exe",
                    Arguments = "-Tpng C:\\compiladores2\\ReporteTablaSim.dot -o C:\\compiladores2\\ReporteTablaSim.png",
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
