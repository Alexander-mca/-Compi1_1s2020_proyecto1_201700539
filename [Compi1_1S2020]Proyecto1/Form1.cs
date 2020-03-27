using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        int img = 0;
        OpenFileDialog abrir;
        SaveFileDialog save;
        List<String> valores = new List<String>();
        Dictionary<String, Object> Expresiones = new Dictionary<String, Object>();
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
                            estado = 5;
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
                                            
                                            x= a;
                                            break;
                                        }
                                        a++;
                                    }
                                  
                                    
                                    Conjunto conj = new Conjunto(idConj,ValC);
                                    //se agrega el conjunto con su id al Dictionary de expresiones
                                    expresiones[idConj]=conj;
                                    i=x;
                                    break;
                                }
                            }

                        }
                    }else if (tipo.Equals(Tipo.id))
                    {
                        String idExp = tk.lexema;

                        if (!expresiones.ContainsKey(idExp))
                        {//aca swe se obtiene la expresion
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
                                        if (!tip.Equals(Tipo.coma))
                                        {
                                            lista.Add(tk3);
                                        }
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
                            //se genera el AFN por thompson, se genera una EDD 
                            valores.Add(idExp+".png");
                            Thompson tom = new Thompson(expArbol);
                            tom.GetArbol();
                            //se crea la imagen del afn
                            String contenido=tom.Graficar(idExp);
                            StreamWriter file = new StreamWriter(idExp+".txt");
                            

                            try
                            {
                                String []data = contenido.Split('\n');
                                foreach (String item in data)
                                {
                                    file.WriteLine(item);
                                }
                               
                                file.Close();
                            }
                            catch (Exception e)
                            {
                                
                            }

                            tom.executeCommand("dot -Tpng " + idExp + ".txt -o "+idExp+".png");
                            try
                            {

                                System.Diagnostics.Process.Start(idExp + ".png");
                                pictureBox1.Image = Image.FromFile(idExp + ".png");
                                //archivo.delete();

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            //en caso de que ya exista un id de Expresion con ese nombre, puede ser la cadena a validar de la expresion 
                            Token cadena = TablaSimbolos[x + 2];
                            if (cadena.tipo.Equals(Tipo.cadena))
                            {

                            }

                        }
                    }
                }
            }
            return expresiones;
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
                    
                    
                    switch (tk.tipo)
                    {
                        case Tipo.id:
                        case Tipo.cadena:
                            pila.Push(tk);
                            break;
                        case Tipo.mas:
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
                        case Tipo.interrogacion:
                            Object izq2 = pila.Pop().valor;
                            ABB arb2 = new ABB();
                            arb2.Add(arb2.raiz, tk);
                            if (izq2 is ABB){
                                ABB izq23 = (ABB)izq2;
                                arb2.Add(arb2.raiz, izq23.raiz);
                                //arb2.Add(arb2.raiz, izq23.raiz.izq);
                            }
                            else{
                                arb2.Add(arb2.raiz, izq2);
                            }
                            pila.Push(arb2);
                            break;
                        case Tipo.asterisco:
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
                        case Tipo.punto:
                        case Tipo.or:
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
            Abrir();

        }
        private void Abrir()
        {
            abrir = new OpenFileDialog();

            abrir.Filter = "Archivos de texto (*.er)|*.er|Archivos de texto (*.txt)|*.txt";
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                StreamReader arch = new StreamReader(abrir.FileName);
                TabPage page = new TabPage(abrir.SafeFileName);
                page.SetBounds(0, 0, pestañas.Width, pestañas.Height);
                RichTextBox text = new RichTextBox();
                text.SetBounds(0, 0, page.Width - 2, page.Height-2);
                text.Text = arch.ReadToEnd();
                page.Controls.Add(text);
                pestañas.TabPages.Add(page);
                Dock = DockStyle.Fill;
                arch.Close();

            }
            //String tex = text1.Text;
            //comando.Clear();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (abrir == null)
            {
               
                MessageBox.Show("El archivo no existe.", "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                StreamWriter w = new StreamWriter(abrir.FileName);
                w.WriteLine(getRTB().Text);
                w.Close();
                MessageBox.Show("Archivo guardado.", "Mensaje de Confirmación.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (abrir != null)
            {
                save = new SaveFileDialog();
                save.FileName = abrir.FileName;

                // filtros
                save.Filter = "Archivos de texto (*.er)|*.er|Archivos de texto (*.txt)|*.txt";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    RichTextBox w = new RichTextBox();
                    w =getRTB();
                    Dock = DockStyle.Fill;
                    w.SaveFile(save.FileName, RichTextBoxStreamType.UnicodePlainText);
                }


            }
            else
            {
                save = new SaveFileDialog();


                // filtros
                save.Filter = "Archivos de texto (*.LS)|*.LS|Archivos de texto (*.txt)|*.txt";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    RichTextBox w = new RichTextBox();
                    w = getRTB();
                    Dock = DockStyle.Fill;
                    w.SaveFile(save.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult boton = MessageBox.Show("¿Esta seguro?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (boton == DialogResult.OK)
            {
                this.Close();
            }
        }

        private RichTextBox getRTB()
        {

            TabPage c = pestañas.SelectedTab;
            Control.ControlCollection x = c.Controls;
            foreach (Control item in x)
            {
                if (item is RichTextBox)
                {
                    RichTextBox text = (RichTextBox)item;
                    return text;
                }
            }
            return null;
        }

        private String ReporteTS()
        {
            String data = "<ListaTokens>\n";
            foreach(Token item in TablaSimbolos)
            {
                data+="<Token>\n" +
                    "<Nombre>" +item.tipo.ToString()+
                    "</Nombre>\n" +
                    "<Valor>" +item.lexema+
                    "</Valor>\n" +
                    "<Fila>" +item.linea+
                    "</Fila>\n" +
                    "<Columna>" +item.columna+
                    "</Columna>\n"+
                    "</Token>\n";
            }
            data += "</ListaTokens>\n";
            return data;
        }

        private String ReporteErrores()
        {
            String data = "<ListaErrores>\n";
            foreach (Token item in Errores)
            {
                data += "<Error>\n" +                   
                    "<Valor>" + item.lexema +
                    "</Valor>\n" +
                    "<Fila>" + item.linea +
                    "</Fila>\n" +
                    "<Columna>" + item.columna +
                    "</Columna>\n" +
                    "</Error>\n";
            }
            data += "</ListaErrores>\n";
            return data;
        }

        public void ErroresHtml()
        {


            String Contenido;
            Contenido = "<html>" +
            "<body>" +
            "<h1 align='center'>Lista de Errores</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +
            "<td><strong>No." +
            "</strong></td>" +

            "<td><strong>Lexema" +
            "</strong></td>" +
            //"<td><strong>Tipo" +
            //"</strong></td>" +

           "<td><strong>Fila" +
            "</strong></td>" +

            "<td><strong>Columna" +
            "</strong></td>" +

            // "<td><strong>Token" +
            //"</strong></td>" +

            "</tr>";

            String CadTokens = "";
            String tempotk;

            for (int i = 0; i < Errores.Count; i++)
            {
                tempotk = "";
                tempotk = "<tr>" +
                "<td><strong>" + Convert.ToString(i + 1) +
                "</strong></td>" +
                "<td>" + Errores.ElementAt(i).lexema +
                "</td>" +

                //"<td>"
                //+ Errores.ElementAt(i).descripcion +
                //"</td>" +

                "<td>" + Errores.ElementAt(i).linea +
                "</td>" +

                "<td>" + Errores.ElementAt(i).columna +
                "</td>" +

                //"<td>" + Tokens.ElementAt(i).token +
                //"</td>" +

                "</tr>";
                CadTokens = CadTokens + tempotk;

            }

            Contenido = Contenido + CadTokens +
            "</table>" +
            "</body>" +
            "</html>";


            /*creando archivo html*/
            File.WriteAllText("Reporte de Errores.html", Contenido);
            System.Diagnostics.Process.Start("Reporte de Errores.html");


        }

        private void tablaDeErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Errores.Count!=0)
            {
                ErroresHtml();
            }
            else
            {
                MessageBox.Show("No hay errores en el texto.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cargarThompsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Expresiones.Clear();
            TablaSimbolos.Clear();
            Errores.Clear();
            Analizar(getRTB().Text);
            if (Errores.Count != 0)
            {
                  MessageBox.Show("Hay errores en el texto.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Expresiones = CrearExpConj();
            }
            

        }

        private void guardarTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String reporte = ReporteTS();
            File.WriteAllText("Tokes.xml", reporte);
            System.Diagnostics.Process.Start("Tokens.xml");
        }

        private void guardarErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String err = ReporteErrores();
            File.WriteAllText("Errores.xml", err);
            System.Diagnostics.Process.Start("Errores.xml");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            img--;
            if (img < 0)
            {
                this.img = valores.Count - 1;
            }
            pictureBox1.Image = Image.FromFile(valores[img]);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            img++;
            if (img == valores.Count)
            {
                this.img = 0;
            }
            pictureBox1.Image = Image.FromFile(valores[img]);
            
           
        }
    }
}