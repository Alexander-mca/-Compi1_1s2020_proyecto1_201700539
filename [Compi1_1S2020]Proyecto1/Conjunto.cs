using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi1_1S2020_Proyecto1
{
    class Conjunto
    {
        String id;
        Dictionary<String, Token> valores;

        public Conjunto(string id, Dictionary<String,Token> valores)
        {
            this.id = id;
            this.valores = valores;
        }
    }
}
