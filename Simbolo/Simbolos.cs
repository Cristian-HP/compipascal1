using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Simbolo
{
    class Simbolos
    {
        public object valor { get; set; }
        public string id { get; set; }
        public Tipo tipo { get; set; }

        public Simbolos(object valor,Tipo tipo,string id)
        {
            this.valor = valor;
            this.id = id;
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return this.valor.ToString();
        }
    }
}
