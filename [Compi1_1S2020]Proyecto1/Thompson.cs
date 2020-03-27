using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _Compi1_1S2020_Proyecto1.Token;

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
                this.siguiente =siguiente;
            }
        }
        public class Nodo
        {
            public List<Transicion> siguiente;
            //public String dato;
            public int estado;
            public Nodo(int estado)
            {
                //this.dato = dato;
                this.estado = estado;
                this.siguiente = new List<Transicion>();
            }
        }
        public Nodo Inicio;
        ABB arbol;
        public Thompson(ABB arbol)
        {
            this.arbol = arbol;
            this.Inicio = null;
        }
        //private Nodo Add(int estado, int estado2, String dato)
        //{
        //    if (Inicio == null)
        //    {
        //        this.Inicio = new Nodo(estado);               
        //        this.Inicio.siguiente.Add(new Transicion(dato, new Nodo(estado2)));
        //        return Inicio.siguiente[0].siguiente;
                
        //    }
        //    else
        //    {

        //    }
            

        //    return null;
        //}
        private Nodo Agregar(Tipo tipo,Object dato,Object dato2)
        {
            char c = (char)949;
            Nodo nd = new Nodo(0);
            Nodo nd2 = new Nodo(1);
            Nodo nd3 = new Nodo(2);
            Nodo nd4 = new Nodo(3);
            Nodo nd5 = new Nodo(4);
            Nodo nd6 = new Nodo(5);
           
            if (dato is Nodo )
            {
                if (dato2 is Nodo)
                {
                    Nodo aux = (Nodo)dato;
                    Nodo aux2 = (Nodo)dato2;

                    switch (tipo)
                    {

                        case Tipo.punto:

                            Nodo first = aux;
                            while (aux.siguiente[0].siguiente.siguiente.Count != 0)
                            {
                                Nodo actual = aux.siguiente[0].siguiente;
                                if (actual.siguiente.Count == 0)
                                {
                                    actual = aux2;
                                    aux.siguiente[0].siguiente = actual;
                                    break;

                                }
                                aux = aux.siguiente[0].siguiente;
                            }

                            return first;

                        case Tipo.or:
                            nd.siguiente.Add(new Transicion(String.Concat(c), aux));
                            nd.siguiente.Add(new Transicion(String.Concat(c), aux2));
                            while (aux.siguiente.Count != 0)
                            {
                                aux = aux.siguiente[0].siguiente;
                            }
                            while (aux2.siguiente.Count != 0)
                            {
                                aux2 = aux2.siguiente[0].siguiente;
                            }
                            aux.siguiente.Add(new Transicion(String.Concat(c), nd2));
                            aux2.siguiente.Add(new Transicion(String.Concat(c), nd2));
                            break;





                    }
                }
                else
                {
                    
                    Nodo aux = (Nodo)dato;
                    switch (tipo)
                    {
                        case Tipo.asterisco:
                            nd2 = aux;

                            nd.siguiente.Add(new Transicion(String.Concat(c), nd2));

                            while (aux.siguiente.Count != 0)
                            {
                                aux = aux.siguiente[0].siguiente;
                            }
                            nd3 = aux;
                            nd3.siguiente.Add(new Transicion(String.Concat(c), nd4));
                            nd3.siguiente.Add(new Transicion(String.Concat(c), nd2));
                            nd.siguiente.Add(new Transicion(String.Concat(c), nd4));
                            break;
                        case Tipo.punto:
                            String val2 = ((Token)((ABB.Nodo)dato2).dato).lexema;
                            nd = aux;
                            while (aux.siguiente[0].siguiente.siguiente.Count != 0)
                            {
                                Nodo actual = aux.siguiente[0].siguiente;
                                if (actual.siguiente.Count == 0)
                                {
                                    nd2 = actual;
                                    aux.siguiente[0].siguiente = nd2;
                                    break;

                                }
                                aux = aux.siguiente[0].siguiente;
                            }

                            nd2.siguiente.Add(new Transicion(val2, nd3));
                            break;
                        case Tipo.interrogacion:
                        case Tipo.or:
                            String val = ((Token)((ABB.Nodo)dato2).dato).lexema;
                            nd.siguiente.Add(new Transicion(String.Concat(c), nd2));
                            nd2 = aux;
                            while (aux.siguiente.Count != 0)
                            {
                                aux = aux.siguiente[0].siguiente;
                            }
                            aux.siguiente.Add(new Transicion(String.Concat(c), nd6));
                            nd.siguiente.Add(new Transicion(String.Concat(c), nd4));
                            nd4.siguiente.Add(new Transicion(val, nd5));
                            nd5.siguiente.Add(new Transicion(String.Concat(c), nd6));

                            break;
                        case Tipo.mas:
                            nd = aux;
                            while (aux.siguiente[0].siguiente.siguiente.Count != 0)
                            {
                                Nodo actual = aux.siguiente[0].siguiente;
                                if (actual.siguiente.Count == 0)
                                {
                                    nd4 = nd2 = actual;
                                    aux.siguiente[0].siguiente = nd2;
                                    aux.siguiente[0].siguiente = nd4;
                                    break;
                                }
                                aux = aux.siguiente[0].siguiente;
                            }
                            nd2.siguiente.Add(new Transicion(String.Concat(c), nd3));
                            nd3 = (Nodo)dato;
                            nd4.siguiente.Add(new Transicion(String.Concat(c), nd5));
                            nd4.siguiente.Add(new Transicion(String.Concat(c), nd3));
                            nd2.siguiente.Add(new Transicion(String.Concat(c), nd5));
                            break;
                    }
                }
            }
            else
            {
                if(dato2 is Nodo)
                {
                    String val1 = ((Token)((ABB.Nodo)dato).dato).lexema;
                    Nodo aux2 = (Nodo)dato2;
                    switch (tipo)
                    {

                        case Tipo.punto:
                            nd2 = aux2;
                            nd.siguiente.Add(new Transicion(val1, nd2));


                            break;

                        case Tipo.or:
                            nd4 = aux2;
                            nd.siguiente.Add(new Transicion(String.Concat(c), nd2));
                            nd2.siguiente.Add(new Transicion(val1, nd3));
                            nd3.siguiente.Add(new Transicion(String.Concat(c), nd6));

                            nd.siguiente.Add(new Transicion(String.Concat(c), nd4));
                            while (aux2.siguiente.Count != 0)
                            {
                                aux2 = aux2.siguiente[0].siguiente;
                            }
                            aux2.siguiente.Add(new Transicion(String.Concat(c), nd6));

                            break;





                    }
                }
                else
                {
                    String val1 = ((Token)((ABB.Nodo)dato).dato).lexema;
                    String val2= ((Token)((ABB.Nodo)dato2).dato).lexema;
                    switch (tipo)
                    {
                        case Tipo.asterisco:

                            nd.siguiente.Add(new Transicion(String.Concat(c), nd2));
                            nd2.siguiente.Add(new Transicion(val1, nd3));
                            nd3.siguiente.Add(new Transicion(String.Concat(c), nd4));
                            nd3.siguiente.Add(new Transicion(String.Concat(c), nd2));
                            nd.siguiente.Add(new Transicion(String.Concat(c), nd4));
                            break;
                        case Tipo.punto:
                            nd.siguiente.Add(new Transicion(val1, nd2));
                            nd2.siguiente.Add(new Transicion(val2, nd3));
                            break;
                        case Tipo.interrogacion:
                        case Tipo.or:
                            nd.siguiente.Add(new Transicion(String.Concat(c), nd2));
                            nd2.siguiente.Add(new Transicion(val1, nd3));
                            nd.siguiente.Add(new Transicion(String.Concat(c), nd4));
                            nd4.siguiente.Add(new Transicion(val2, nd5));
                            nd5.siguiente.Add(new Transicion(String.Concat(c), nd6));
                            nd3.siguiente.Add(new Transicion(String.Concat(c), nd6));
                            break;
                        case Tipo.mas:
                            nd.siguiente.Add(new Transicion(val1, nd2));
                            nd2.siguiente.Add(new Transicion(String.Concat(c), nd3));
                            nd3.siguiente.Add(new Transicion(val1, nd4));
                            nd4.siguiente.Add(new Transicion(String.Concat(c), nd5));
                            nd2.siguiente.Add(new Transicion(String.Concat(c), nd5));
                            nd4.siguiente.Add(new Transicion(String.Concat(c), nd3));
                            break;




                    }
                }
            }
           
                
            return nd;
        }

        public  Object GenerarThompson(ABB.Nodo raiz)
        {
            if (raiz != null)
            {   if (raiz.izq != null && raiz.der != null)
                {
                    Object izq = GenerarThompson(raiz.izq);
                    Object der = GenerarThompson(raiz.der);
                    Tipo tipo = ((Token)raiz.dato).tipo;
                   return Agregar(tipo, izq, der);
                }
                else
                {
                    if(raiz.izq==null && raiz.der == null)
                    {
                        return raiz;
                    }else if (raiz.izq == null)
                    {

                    }
                    else
                    {
                        Object izq = GenerarThompson(raiz.izq);
                        Object der = GenerarThompson(raiz.der);
                        Tipo tipo = ((Token)raiz.dato).tipo;
                        return Agregar(tipo, izq, der);
                    }

                }
               

            }
           

            return null;
        }
        public Nodo GetArbol()
        {
            Object obj = GenerarThompson(arbol.raiz);
            if(obj is Nodo)
            {
                Nodo a = (Nodo)obj;
                Nodo nuevo = renombrar(a);
                this.Inicio = a;
                return this.Inicio;
            }
            return null;
        }
        private Nodo renombrar(Nodo first)
        {
            if (first != null)
            {
                int cont = 0;
                Nodo aux = first;
                while (aux.siguiente.Count != 0)
                {
                    for (int i = 0; i < aux.siguiente.Count; i++)
                    {
                        aux.estado = cont;
                        cont++;
                        aux = aux.siguiente[i].siguiente;
                    }
                }
            }
            return null;
        }
        public String Graficar(String name)
        {
            String contenido= "digraph s1{ rankdir=LR; graph[fontname = \"Helvetica - Oblique\",fontsize = 36,label =\""+name+ "\",size = \"6,6\" ];";
            String relaciones="";
            String nodos = "";
            Nodo aux = Inicio;
            int conteo;
           
               
                for (int i = 0; i <aux.siguiente.Count; i++)
                {
                //int dif = aux.siguiente[i].siguiente.estado+aux.estado;


                if (aux.siguiente[i].siguiente.estado < aux.estado)
                {
                    relaciones += "rel" + aux.estado + "->rel" + aux.siguiente[i].siguiente.estado + "[label=\"" + aux.siguiente[i].dato + "\"];\n";
                }
                else

                {
                    while (aux.siguiente[i].siguiente.siguiente.Count != 0)
                    {
                        Nodo actual = aux.siguiente[i].siguiente;
                       
                        relaciones += "rel" + aux.estado + "->rel" + actual.estado + "[label=\"" + aux.siguiente[i].dato + "\"];\n";
                        aux = aux.siguiente[i].siguiente;
                        if (actual.siguiente.Count == 0)
                        {
                            nodos += "node [shape=doublecircle];rel" + actual.estado + ";\n";
                            nodos += "node" + "[shape=circle];\n";
                        }
                    }
                   
                }
               
                }

            contenido += nodos+relaciones+"}";

            return contenido;
        }
        public void executeCommand(String commandR)
        {
            try
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c " + commandR,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                updateStatusExecution("***COMMAND RECEIVED: " + commandR);
                updateStatusExecution("***ANSWER:");
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    updateStatusExecution(line);
                }
            }
            catch (Exception e)
            {
                updateStatusExecution("***Error while executing '" + commandR + "'");
                updateStatusExecution("***Exception: '" + e.ToString());
                updateStatusExecution("***Stack Trace: '" + e.StackTrace.ToString());
            }
        }
        private void updateStatusExecution(String textR)
        {
            String currentDateTime = DateTime.Now.ToString(); ;
            Console.WriteLine(currentDateTime + " - " + textR);
        }

    }
}
