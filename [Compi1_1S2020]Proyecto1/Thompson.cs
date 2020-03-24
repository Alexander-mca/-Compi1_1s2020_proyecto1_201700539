using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi1_1S2020_Proyecto1
{
    class Thompson
    {
        public class Transicion
        {
            public String dato;
            public Nodo siguiente;

            public Transicion(string dato, Nodo siguiente)
            {
                this.dato = dato;
                this.siguiente = null;
            }
        }
        public class Nodo
        {
            public List<Transicion> siguiente;
            //public int dato;
            public int estado;
            public Nodo(int estado)
            {
                //this.dato = dato;
                this.estado = estado;
                this.siguiente = new List<Transicion>();
            }
        }
        Nodo raiz;
        public Thompson()
        {
            this.raiz = null;
        }
        public Nodo Add(Nodo raiz, int estado, String dato)
        {
            if (this.raiz == null)
            {
                this.raiz = new Nodo(estado);
            }
            else
            {
                if (raiz != null)
                {
                    raiz.siguiente.Add(new Transicion(dato, null));
                    foreach (Transicion item in raiz.siguiente)
                    {
                        if (item.siguiente == null)
                        {
                            item.siguiente = Add(null, estado, dato);
                        }
                    }
                }
                else
                {
                    return new Nodo(estado);
                }
            }

            return null;

        }
    }
}
