using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Simbolo
{
    class Tablatipos
    {
        public static Tipos[,] Arisum = new Tipos[4, 4] {
            { Tipos.INTEGER,Tipos.REAL,Tipos.ERROR,Tipos.ERROR},
            { Tipos.REAL,Tipos.REAL,Tipos.ERROR,Tipos.ERROR},
            { Tipos.ERROR,Tipos.ERROR,Tipos.STRING,Tipos.ERROR},
            {Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR }
        };

        public static Tipos[,] AriRMD = new Tipos[4, 4] {
            { Tipos.INTEGER,Tipos.REAL,Tipos.ERROR,Tipos.ERROR},
            { Tipos.REAL,Tipos.REAL,Tipos.ERROR,Tipos.ERROR},
            { Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR},
            {Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR }
        };

        public static Tipos[,] Arimod = new Tipos[4, 4] {
            { Tipos.INTEGER,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR},
            { Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR},
            { Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR},
            {Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR }
        };

        public static Tipos[,] Rel = new Tipos[4, 4] {
            { Tipos.BOOLEAN,Tipos.BOOLEAN,Tipos.ERROR,Tipos.ERROR},
            { Tipos.BOOLEAN,Tipos.BOOLEAN,Tipos.ERROR,Tipos.ERROR},
            { Tipos.ERROR,Tipos.ERROR,Tipos.BOOLEAN,Tipos.ERROR},
            {Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.BOOLEAN }
        };
        public static Tipos[,] Log = new Tipos[4, 4] {
             { Tipos.INTEGER,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR},
            { Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR},
            { Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR},
            {Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.BOOLEAN }
        };

        public static Tipos getTipo(Tipo izquerda,Tipo derecha,string operacion)
        {
            if (operacion.Equals("+"))
            {
                return Arisum[(int)izquerda.tipo, (int)derecha.tipo];
            }else if (operacion.Equals("-") || operacion.Equals("*") || operacion.Equals("/"))
            {
                return AriRMD[(int)izquerda.tipo, (int)derecha.tipo];
            }
            else if (operacion.Equals("%"))
            {
                return Arimod[(int)izquerda.tipo, (int)derecha.tipo];
            }
            else if (operacion.Equals(">") || operacion.Equals("<") || operacion.Equals("<=") || operacion.Equals(">=") || operacion.Equals("=") || operacion.Equals("<>"))
            {
                return Rel[(int)izquerda.tipo,(int)derecha.tipo];
            }
            else
            {
                return Log[(int)izquerda.tipo, (int)derecha.tipo];
            }
        }
    }
}
