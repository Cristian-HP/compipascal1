using compipascal1.Analisis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compipascal1
{
    public partial class Form1 : Form
    {
        public static RichTextBox consola2;
        public Form1()
        {
            InitializeComponent();
            consola2 = consola;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            consola2.Text = "";
            Analizador analizador = new Analizador();
            String txt = richTextBox1.Text;
            analizador.analizar(txt);
        }
        public  void  wrtconsola(string txt)
        {
            consola.Text += txt;
        }

    }
}
