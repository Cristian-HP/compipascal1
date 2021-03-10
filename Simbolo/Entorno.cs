using System;
using System.Collections.Generic;
using System.Text;

namespace compipascal1.Simbolo
{
    class Entorno
    {
        Dictionary<string, Simbolos> variables;
        Entorno padre;

        public Entorno(Entorno padre)
        {
            this.padre = padre;
            this.variables = new Dictionary<string, Simbolos>();
        }

        public void declararVariable(string id, Simbolos variable)
        {
            if (this.variables.ContainsKey(id))
            {
                throw new Exception("La variable " + id + "ya existe en este ambito");
            }
            else
            {    
                this.variables.Add(id, variable);
            }
        }

        public Simbolos obtenerVariable(string id)
        {
            Entorno actual = this;
            while(actual != null)
            {
                if(actual.variables[id] != null)
                {
                    return actual.variables[id];
                }
                actual = actual.padre;
            }

            return null;
        }
    }
}
