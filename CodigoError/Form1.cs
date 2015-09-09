using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodigoError
{
    public partial class Form1 : Form
    {
        static string linea = ""; //Valor que se lee
        static List<Valor> valores = new List<Valor>(); //Crea lista de valores

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Practica2 obj = new Practica2(); //Crea objeto de practica
            int i, aux;
            bool x = true; //Auxiliar para impresion
            string enviado, recibido, salida;

            /*
             *
             * LECTURA ARCHIVO
             * 
             */
            //Lectura Archivo si no se a elegido ningun archivo
            aux = linea.Length;//Auxiliar si ya se leyo un archivo
            //aux = 0;
            if (aux == 0)
            {
                //Mientras no se abra ningun archivo
                while (true)
                {
                    //Crea una ventana para abrir archivo
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        System.IO.StreamReader archivo = new System.IO.StreamReader(openFileDialog1.FileName);
                        //Lectura de la linea
                        linea = archivo.ReadLine();
                        //Si lee algo sale del ciclo infinito
                        if (linea.Length != 0)
                        {
                            break;
                        }

                        archivo.Close();
                    }
                }
            }

            linea = linea.Replace(" ", String.Empty);

            //Llama al metodo leer
            valores = obj.llenarvector(linea);

            //Llama el metodo que delvuelve el String para imprimir
            enviado = obj.codificar(linea, valores);

            //Llama el metodo que delvuelve el String con error
            recibido = obj.enviar(linea, valores, (double)numericUpDown1.Value);

            //Llama el metodo que delvuelve la salida
            salida = obj.salir(recibido, valores);

            /*
             * 
             * MOSTRAR DATOS LEIDOS
             * 
             */
            //Muestra los datos en el Form
            richTextBox1.Clear();
            richTextBox1.Text = linea;

            //Muestra el codigo enviado
            richTextBox2.Clear();
            richTextBox2.Text = enviado;

            //Muestra el codigo recibido (con ruido)
            richTextBox3.Clear();
            if (recibido[0].Equals(enviado[0]) == true)
            {
                x = true;
            }
            else
            {
                x = false;
            }
            for (i = 0; i < recibido.Length-1; i++)
            {
                if (x == true)
                {
                    AnexarTexto(this.richTextBox3, Color.Black, recibido[i].ToString());
                    if (recibido[i+1].Equals(enviado[i+1]) == false)
                    {
                        x = false;
                    }
                }
                else
                {
                    AnexarTexto(this.richTextBox3, Color.Red, recibido[i].ToString());
                    if (recibido[i + 1].Equals(enviado[i + 1]) == true)
                    {
                        x = true;
                    }
                }
            }

            //Muestra el mensaje decodificado
            linea = linea + " ";
            salida = salida + " ";
            richTextBox4.Clear();
            if (salida[0].Equals(linea[0]) == true)
            {
                x = true;
            }
            else
            {
                x = false;
            }
            for (i = 0; i < salida.Length - 1; i++)
            {
                if (x == true)
                {
                    AnexarTexto(this.richTextBox4, Color.Black, salida[i].ToString());
                    if (salida[i + 1].Equals(linea[i + 1]) == false)
                    {
                        x = false;
                    }
                }
                else
                {
                    AnexarTexto(this.richTextBox4, Color.Red, salida[i].ToString());
                    if (salida[i + 1].Equals(linea[i + 1]) == true)
                    {
                        x = true;
                    }
                }
            }
        }

        //Metodo Para dar color a RichBox
        void AnexarTexto(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;
            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear
        }

        private void Form1_Load(object sender, EventArgs e)
        {    
        }

        //submenu para elegir archivo
        private void elegirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Crea una ventana para abrir archivo
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader archivo = new System.IO.StreamReader(openFileDialog1.FileName);
                //Lectura de la linea
                linea = archivo.ReadLine();
                archivo.Close();
            }
        }
    }
}
