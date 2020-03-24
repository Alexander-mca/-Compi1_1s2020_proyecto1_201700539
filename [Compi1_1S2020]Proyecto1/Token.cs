using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi1_1S2020_Proyecto1
{
    class Token
    {   public Tipo tipo;
        public String lexema;
        public int linea, columna;
        public Token(Tipo tipo, String lexema, int linea, int columna)
        {
            this.tipo = tipo;
            this.lexema = lexema;
            this.linea= linea;
            this.columna = columna;
        }

        public enum Tipo
        {
            error=1, cadena=2, numero=3,puntoycoma=4,dospuntos=5,punto=6,virgulilla=7,asterisco=8,admiracion=9,or=10,coma=11, mas=12,menor=13, mayor=14,guion=15,interrogacion=16, llaveAbre=17,llaveCierra=18,dolar=19,
            modulo=20,id=21,conjunto=22,comentarioMult=23,ComentarioSimple=24,numeral=25,ampersand=26,saltolinea=27,tabulacion=28,comillaSimple=29,comillaDoble=30,ConjTodo=31,reservada=32
               
        };
    }
    
}
