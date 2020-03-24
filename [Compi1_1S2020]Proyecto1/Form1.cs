using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _Compi1_1S2020_Proyecto1.Token;

namespace _Compi1_1S2020_Proyecto1
{
    public partial class Form1 : Form
    {
        List<Token> TablaSimbolos = new List<Token>();
        List<Token> Errores = new List<Token>();
        OpenFileDialog abrir;
        SaveFileDialog save;
        //Dictionary<String, Object> Expresiones=new Dictionary<String, Object>();
        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void Analizar(String texto)
        {
            char c;
            int linea=1, columna=1;
            int estado=0;
            String lexema = "";
            for (int i = 0; i < texto.Length; i++)
            {
                c = texto[i];
                switch (estado)
                {
                    case 0:
                        if (Char.IsDigit(c))
                        {
                            estado = 1;
                            lexema += c;
                        } else if (Char.IsLetter(c))
                        {
                            estado = 5;
                            lexema += c;

                        } else if (c == ' ' || c == '\b' || c == '\r' || c == '\t')
                        {

                        } else if (c == '\n')
                        {
                            linea++;
                            columna = 1;
                        }
                        else
                        {

                            switch (c)
                            {
                                case ';':
                                    TablaSimbolos.Add(new Token(Token.Tipo.puntoycoma, String.Concat(c), linea, columna));
                                    break;
                                case ':':
                                    TablaSimbolos.Add(new Token(Token.Tipo.dospuntos, String.Concat(c), linea, columna));
                                    break;
                                case '.':
                                    TablaSimbolos.Add(new Token(Token.Tipo.punto, String.Concat(c), linea, columna));
                                    break;
                                case '"':
                                    lexema += c;
                                    estado = 4;
                                    break;
                                case '~':
                                    TablaSimbolos.Add(new Token(Token.Tipo.virgulilla, String.Concat(c), linea, columna));
                                    break;
                                case '*':
                                    TablaSimbolos.Add(new Token(Token.Tipo.asterisco, String.Concat(c), linea, columna));
                                    break;
                                case '!':
                                    TablaSimbolos.Add(new Token(Token.Tipo.admiracion, String.Concat(c), linea, columna));
                                    break;
                                case '|':
                                    TablaSimbolos.Add(new Token(Token.Tipo.or, String.Concat(c), linea, columna));
                                    break;
                                case ',':
                                    TablaSimbolos.Add(new Token(Token.Tipo.coma, String.Concat(c), linea, columna));
                                    break;
                                case '+':
                                    TablaSimbolos.Add(new Token(Token.Tipo.mas, String.Concat(c), linea, columna));
                                    break;
                                case '<':
                                    estado = 7;
                                    lexema += c;
                                    break;
                                case '>':
                                    TablaSimbolos.Add(new Token(Token.Tipo.mayor, String.Concat(c), linea, columna));
                                    break;
                                case '%':
                                    TablaSimbolos.Add(new Token(Token.Tipo.modulo, String.Concat(c), linea, columna));
                                    estado = 0;
                                    break;
                                case '/':
                                    lexema += c;
                                    estado = 10;
                                    break;
                                case '-':
                                    TablaSimbolos.Add(new Token(Token.Tipo.guion, String.Concat(c), linea, columna));
                                    estado = 0;
                                    break;
                                case '?':
                                    TablaSimbolos.Add(new Token(Token.Tipo.interrogacion, String.Concat(c), linea, columna));
                                    estado = 0;
                                    break;
                                case '{':
                                    TablaSimbolos.Add(new Token(Token.Tipo.llaveAbre, String.Concat(c), linea, columna));
                                    estado = 0;
                                    break;
                                case '}':
                                    TablaSimbolos.Add(new Token(Token.Tipo.llaveCierra, String.Concat(c), linea, columna));

                                    estado = 0;
                                    break;
                                case '$':
                                    TablaSimbolos.Add(new Token(Token.Tipo.dolar, String.Concat(c), linea, columna));

                                    estado = 0;
                                    break;
                                case '&':
                                    TablaSimbolos.Add(new Token(Token.Tipo.ampersand, String.Concat(c), linea, columna));
                                    estado = 0;
                                    break;
                                case '#':
                                    TablaSimbolos.Add(new Token(Token.Tipo.numeral, String.Concat(c), linea, columna));
                                    estado = 0;
                                    break;
                                case '\\':
                                    lexema += c;
                                    estado = 6;
                                    break;
                                case '[':
                                    lexema += c;
                                    estado = 12;
                                    break;
                                default:
                                    //aquí se marcan los errores lexicos
                                    Errores.Add(new Token(Token.Tipo.error, String.Concat(c), linea, columna));
                                    break;
                            }

                        }
                        break;
                    case 1:
                        if (Char.IsDigit(c))
                        {
                            lexema += c;
                            estado = 1;
                        } else if (c == '.')
                        {
                            estado = 2;
                            lexema += c;
                        }
                        else
                        {
                            estado = 0;
                            Token tk = new Token(Token.Tipo.numero, lexema, linea, columna);
                            TablaSimbolos.Add(tk);
                            lexema = "";
                            i--;
                        }
                        break;
                    case 2:
                        if (Char.IsDigit(c))
                        {
                            estado = 3;
                            lexema += c;
                        }
                        else
                        {
                            //error
                            estado = 1;
                            i--;
                        }
                        break;
                    case 3:
                        if (Char.IsDigit(c))
                        {
                            estado = 3;
                            lexema += c;
                        }
                        else
                        {
                            estado = 0;
                            Token tk = new Token(Token.Tipo.numero, lexema, linea, columna);
                            TablaSimbolos.Add(tk);
                            lexema = "";
                            i--;
                        }
                        break;
                    case 4:
                        if (c == '"')
                        {
                            lexema += c;
                            TablaSimbolos.Add(new Token(Token.Tipo.cadena, lexema, linea, columna));
                            lexema = "";
                            estado = 0;
                        }
                        else
                        {
                            lexema += c;
                            estado = 4;
                        }
                        break;
                    case 5:
                        if (Char.IsLetterOrDigit(c))
                        {
                            lexema += c;
                            estado = 3;
                        }
                        else
                        {
                            //validar palabra reservada
                            Reservada(lexema, linea, columna);
                            lexema = "";
                            estado = 0;
                            i--;
                        }
                        break;
                    case 6:
                        if (c == 'n')
                        {
                            lexema += c;
                            TablaSimbolos.Add(new Token(Token.Tipo.saltolinea, lexema, linea, columna));
                            lexema = "";
                            estado = 0;

                        } else if (c == 't')
                        {
                            lexema += c;
                            TablaSimbolos.Add(new Token(Token.Tipo.tabulacion, lexema, linea, columna));
                            lexema = "";
                            estado = 0;
                        } else if (c == '"')
                        {
                            lexema += c;
                            TablaSimbolos.Add(new Token(Token.Tipo.comillaDoble, lexema, linea, columna));
                            lexema = "";
                            estado = 0;
                        } else if (c == '\'')
                        {
                            lexema += c;
                            TablaSimbolos.Add(new Token(Token.Tipo.comillaSimple, lexema, linea, columna));
                            lexema = "";
                            estado = 0;
                        }
                        break;
                    case 7:
                        if (c == '!')
                        {
                            estado = 8;
                            lexema += c;
                        }
                        break;
                    case 8:
                        if (c == '!')
                        {
                            lexema += c;
                            estado = 9;
                        }
                        else
                        {
                            lexema += c;
                            estado = 8;
                        }
                        break;
                    case 9:
                        if (c == '>')
                        {
                            lexema += c;
                            TablaSimbolos.Add(new Token(Token.Tipo.comentarioMult, lexema, linea, columna));
                            lexema = "";
                            estado = 0;
                        }
                        break;
                    case 10:
                        if (c == '/')
                        {
                            lexema += c;
                            estado = 11;
                        }
                        break;
                    case 11:
                        if (c == '\n')
                        {
                            TablaSimbolos.Add(new Token(Token.Tipo.ComentarioSimple, lexema, linea, columna));
                            lexema = "";
                            estado = 0;
                            i--;
                        }
                        else
                        {
                            lexema += c;
                            estado = 11;
                        }
                        break;
                    case 12:
                        if (c == ':')
                        {
                            lexema += c;
                            estado = 13;
                        }
                        break;
                    case 13:
                        if (c == ':')
                        {
                            lexema += c;
                            estado = 14;
                        }
                        else if (c == '\\' && texto[i+1]=='n' )
                        {

                        }
                        else
                        {
                            lexema += c;
                            estado = 13;
                        }
                        break;
                    case 14:
                        if (c == ']')
                        {
                            lexema += c;
                            TablaSimbolos.Add(new Token(Token.Tipo.ConjTodo, lexema, linea, columna));
                            estado = 0;
                            lexema = "";
                        }
                        break;
                }
                columna++;
            }
        }

        private void Reservada(String lexema,int fila,int columna)
        {
            String lex = lexema.ToLower();
            switch (lex)
            {
                case "conj":
                    TablaSimbolos.Add(new Token(Token.Tipo.reservada, lexema, fila, columna));

                    break;
                default:
                    TablaSimbolos.Add(new Token(Token.Tipo.id, lexema, fila, columna));
                    break;
            }
        }

        private Dictionary<String,Object> CrearExpConj()
        {
            Dictionary<String, Object> expresiones = new Dictionary<String, Object>();
            int tam = TablaSimbolos.Count;
            if (tam != 0)
            {
                for (int x = 0; x < TablaSimbolos.Count; x++)
                {
                    Token tk = TablaSimbolos[x];
                    String nombre = tk.lexema.ToLower();
                    Tipo tipo = tk.tipo;
                    if(tipo.Equals(Tipo.reservada) && nombre.Equals("conj"))
                    {
                        //se empieza a buscar cada  conjunto
                        for (int i = x; i < tam; i++)
                        {
                            Token.Tipo tipo0 = TablaSimbolos[i].tipo;//Tipo del conjunto
                            if (tipo0.Equals(Token.Tipo.id))
                            {
                                String idConj = TablaSimbolos[i].lexema;
                                Token.Tipo tipo2 = TablaSimbolos[i + 1].tipo;
                                Token.Tipo tipo3 = TablaSimbolos[i + 2].tipo;
                                if (tipo2.Equals(Token.Tipo.guion) && tipo3.Equals(Token.Tipo.mayor))
                                {
                                    int a = i + 3;
                                    Dictionary<String,Token> ValC =new Dictionary<String, Token>();
                                    while (a < tam)
                                    {
                                        Token item = TablaSimbolos[a];
                                        Tipo tip = item.tipo;
                                        if (!tip.Equals(Tipo.puntoycoma))
                                        {//se van concatenando los items  del Conjunto
                                            if (!tip.Equals(Tipo.coma))
                                            {
                                               ValC[item.lexema] = item;
                                            }
                                        }
                                        else
                                        {
                                            x = a;
                                            break;
                                        }
                                        a++;
                                    }
                                  
                                    
                                    Conjunto conj = new Conjunto(idConj,ValC);
                                    //se agrega el conjunto con su id al Dictionary de expresiones
                                    expresiones[idConj]=conj;
                                    i = x;
                                    break;
                                }
                            }

                        }
                    }else if (tipo.Equals(Tipo.id))
                    {
                        String idExp = tk.lexema;

                        if (!expresiones.ContainsKey(idExp))
                        {
                            Token tk1 = TablaSimbolos[x + 1];
                            Token tk2 = TablaSimbolos[x + 2];
                            List<Token> lista = new List<Token>();
                            if (tk1.tipo.Equals(Token.Tipo.guion) && tk2.tipo.Equals(Token.Tipo.mayor))
                            {
                              
                                for (int i = x + 3; i < tam; i++)
                                {
                                    Token tk3 = TablaSimbolos[i];
                                    Tipo tip = TablaSimbolos[i].tipo;
                                    if (!tip.Equals(Token.Tipo.puntoycoma))
                                    {

                                        //switch ((int)tip)
                                        //{
                                        //    case 21:

                                        //    if (expresiones.ContainsKey(tk3.lexema))
                                        //     {
                                        //     Object obj = expresiones[tk3.lexema];
                                        //            if (obj is Conjunto)
                                        //            {
                                        //                lista.Add(tk3);
                                        //                break;
                                        //            }
                                        //    }           


                                        //   break;
                                        //   case 17:
                                        //   case 18:
                                        //   break;
                                        //   default:
                                        //       lista.Add(tk3);
                                        //   break;
                                        //}
                                        lista.Add(tk3);
                                    } else {
                                     x=i;
                                     break;
                                    }
                                }
                            }
                            //aqui se se guarda todos los datos de lista en un ABB
                            ABB expArbol = GenerarArbol(lista);
                            Expresion exp = new Expresion(idExp, expArbol);
                            //se guarda la expresion en el Dictionary
                            expresiones[idExp] = exp;
                        }
                        else
                        {
                            //en caso de que ya exista un id de Expresion con ese nombre, puede ser la cadena a validar de la expresion 

                        }
                    }
                }
            }
            return null;
        }
         private ABB GenerarArbol(List<Token> lista)
        { //esta funcion lo que hace es convertir la lista de tokens de una expresion regular a un arbol binario de busqueda
            List<String> errores = new List<String>();
            ABB arbol = new ABB();
            Pila pila = new Pila();
            if (lista.Count!=0)
            {
                for (int x = lista.Count - 1; x >= 0; x--)
                {
                   Token tk = lista[x];
                    
                    
                    switch ((int)tk.tipo)
                    {
                        case 2:
                            pila.Push(tk);
                            break;
                        case 12:
                            Object izq = pila.Pop().valor;
                            ABB arb = new ABB();
                            arb.Add(arb.raiz, tk);
                            if (izq is ABB){
                                ABB izq23 = (ABB)izq;
                                arb.Add(arb.raiz, izq23.raiz);
                            }else{
                                arb.Add(arb.raiz, izq);
                            }

                            pila.Push(arb);
                            break;
                        case 16:
                            Object izq2 = pila.Pop().valor;
                            ABB arb2 = new ABB();
                            arb2.Add(arb2.raiz, tk);
                            if (izq2 is ABB){
                                ABB izq23 = (ABB)izq2;
                                arb2.Add(arb2.raiz, izq23.raiz);
                            }else{
                                arb2.Add(arb2.raiz, izq2);
                            }
                            pila.Push(arb2);
                            break;
                        case 8:
                            Object izq1 = pila.Pop().valor;
                            ABB arb1 = new ABB();
                            arb1.Add(arb1.raiz, tk);
                            if (izq1 is ABB){
                                ABB izq23 = (ABB)izq1;
                                arb1.Add(arb1.raiz, izq23.raiz);
                            }else{
                                arb1.Add(arb1.raiz, izq1);
                            }
                            pila.Push(arb1);
                            break;
                        case 6:
                        case 10:
                            if (pila.size() > 1)
                            {
                                Object izquierdo = pila.Pop().valor;
                                Object der = pila.Pop().valor;
                                ABB tree = new ABB();
                                tree.Add(tree.raiz, tk);
                                if (izquierdo is ABB) {
                                    ABB izqe = (ABB)izquierdo;
                                    tree.Add(tree.raiz, izqe.raiz);
                                } else {
                                    tree.Add(tree.raiz, izquierdo);
                                }
                                if (der is ABB) {
                                    ABB der1 = (ABB)der;
                                    tree.Add(tree.raiz, der1.raiz);
                                } else {
                                    tree.Add(tree.raiz, der);
                                }
                                pila.Push(tree);

                            }
                            else
                            {
                                //error
                                errores.Add("La expresión tiene menos de 3 elementos. No se puede armar el arbol.");
                            }
                            break;
                            //                        default:
                            //                            //Error
                            //                            errores.add("Se ha detectado un error en la expresión regular con Id:"+id+". Linea: "+tk1.fila+" Columna: "+tk1.columna+". Simbolo: "+tk1.lexema);
                            //                            break;
                    }

                 
                //else if (tk instanceof Conjunto) {
                //    pila.Push(tk);
                //}
                //                
            }
        }
        if (pila.size() == 1) {
            Pila.Nodo aux = pila.Pop();
        Object obj = aux.valor;
            if (!(obj is ABB)) {
                ABB tree = new ABB();
        tree.Add(tree.raiz, obj);
                arbol=tree;
              
            }else{ 
                arbol = (ABB) obj;
}
            
        } else {
            //se ha detectado un error
            errores.Add("Expresion vacía o con mas de 1 elemento. Error no se puede continuar con el proceso.");
        }

        if (errores.Count!=0) {
            while (pila.size() != 0) {
                pila.Pop();
            }
        }
            return arbol;  
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void Abrir()
        {
            abrir = new OpenFileDialog();

            abrir.Filter = "Archivos de texto (*.LS)|*.LS|Archivos de texto (*.txt)|*.txt";
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                StreamReader arch = new StreamReader(abrir.FileName);
                text1.Text = arch.ReadToEnd();
                Dock = DockStyle.Fill;
                tabs.







                arch.Close();

            }
            String tex = text1.Text;
            text1.Clear();
        }
    }
}
