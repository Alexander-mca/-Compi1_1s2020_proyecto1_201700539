using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Compi1_1S2020_Proyecto1
{
    class Pila
    {
        public class Nodo
        {
           public Nodo siguiente;
          public  Object valor;
            public Nodo(Object valor)
            {
                this.valor = valor;
                this.siguiente = null;
            }
        }
        public Nodo Inicio, Fin;

        public Pila()
        {
            this.Inicio = this.Fin = null;
        }
        public void Push(Object obj)
        {
            if (Inicio == null)
            {
                Inicio = new Nodo(obj);
                Fin = Inicio;
            }
            else
            {
                if (Inicio == Fin)
                {
                    Inicio.siguiente = new Nodo(obj);
                    Fin = Inicio.siguiente;
                }
                else
                {
                    Fin.siguiente = new Nodo(obj);
                    Fin = Fin.siguiente;
                }

            }
        }
        public int size()
        {
            int tam = 0;
            if (Inicio != null)
            {
                Nodo aux = Inicio;
                while (aux != null)
                {
                    tam++;
                    aux = aux.siguiente;
                }
            }
            return tam;
        }
        public Nodo Pop()
        {
            Nodo retorno = null;
            if (Inicio != null)
            {
                if (Inicio == Fin)
                {

                    retorno = Inicio;
                    Inicio = Fin = null;

                    return retorno;
                }
                else
                {
                    Nodo aux = Inicio;
                    while (aux.siguiente != null)
                    {
                        Nodo actual = aux.siguiente;
                        if (actual == Fin)
                        {
                            retorno = actual;
                            aux.siguiente = null;
                            Fin = aux;
                            return retorno;
                        }
                        aux = aux.siguiente;
                    }
                }

            }
            return retorno;
        }
    }
}
