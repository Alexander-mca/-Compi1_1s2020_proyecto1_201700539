using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi1_1S2020_Proyecto1
{
    class ABB
    { public class Nodo
        {
            public Nodo izq, der;
            public Object dato;

            public Nodo(Object dato)
            {
                this.dato = dato;
                this.izq = this.der = null;
            }
        }
      public  Nodo raiz;
        public ABB()
        {
            this.raiz = null;
        }
        public Nodo Add(Nodo raiz,Object dato)
        {
            if (this.raiz == null)
            {
                this.raiz = new Nodo(dato);
            }
            else
            {
                if (raiz != null)
                {


                    if (raiz.izq!= null)
                    {
                        raiz.der = Add(raiz.der, dato);
                    }
                    else
                    {
                        raiz.izq = Add(raiz.izq, dato);
                    }
                }
                else
                {
                    Nodo r;
                    if (dato is Nodo){
                        r = (Nodo)dato;
                    }else{
                         r= new Nodo(dato);
                    }
                    return r;
                }
            }
            return null;
        }
    }
}
