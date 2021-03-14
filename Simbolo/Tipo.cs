using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Simbolo
{
    public enum Tipos
    {
        INTEGER = 0,
        BOOLEAN = 3,
        REAL = 1,
        STRING = 2,
        OBJECT = 4,
        VOID = 5,
        ERROR = 9
    }
    class Tipo
    {
        public Tipos tipo;
        public string tipoaux;

        public Tipo(Tipos tipo, string tipoaxu)
        {
            this.tipo = tipo;
            this.tipoaux = tipoaxu;
        }

        public override string ToString()
        {
            return tipo.ToString();
        }
    }
}
