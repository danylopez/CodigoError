using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodigoError
{
    class Practica2
    {
        public List<Valor> llenarvector(string linea)
        {
            int i, j, tam, aux, cont, validar = 0;
            List<Valor> valores = new List<Valor>();//Lista de valores
            List<String> codigos = new List<String>();//Lista del codigo

            //Inicializamos el codigo
            codigos.Add("11111");
            codigos.Add("11110");
            codigos.Add("11101");
            codigos.Add("11100");
            codigos.Add("11011");
            codigos.Add("11010");
            codigos.Add("11001");
            codigos.Add("11000");
            codigos.Add("10111");
            codigos.Add("10110");
            codigos.Add("10101");
            codigos.Add("10100");
            codigos.Add("10011");
            codigos.Add("10010");
            codigos.Add("10001");
            codigos.Add("10000");
            codigos.Add("01111");
            codigos.Add("01110");
            codigos.Add("01101");
            codigos.Add("01100");
            codigos.Add("01011");
            codigos.Add("01010");
            codigos.Add("01001");
            codigos.Add("01000");
            codigos.Add("00111");
            codigos.Add("00110");
            codigos.Add("00101");
            codigos.Add("00100");
            codigos.Add("00011");
            codigos.Add("00010");
            codigos.Add("00001");
            codigos.Add("00000");
            
            //Calcular los valores
            for (i = 0; i < linea.Length; i++)
            {
                //El primero se mete porque no hay datos aun
                if (valores.Count == 0)
                {
                    validar = validar + 1;
                    Valor valor = new Valor();
                    valor.nombre = linea[i];
                    valor.codigo = codigos[0];
                    codigos.Remove(valor.codigo);
                    cont = 0;
                    for (j = 0; j < valor.codigo.Length; j++)
                    {
                        if (valor.codigo[j].Equals('1') == true)
                        {
                            cont = cont + 1;
                        }

                    }
                    if (cont % 2 == 0)
                    {
                        valor.control = '0';
                    }
                    else
                    {
                        valor.control = '1';
                    }
                    valores.Add(valor);
                    if (validar == 32)
                    {
                        MessageBox.Show("Error! Supera el tamaño.");
                        return valores;
                    }
                }
                else
                {
                    //Se valida por medio de una bandera si el dato ya esta
                    tam = valores.Count;
                    aux = 0;
                    for (j = 0; j < tam; j++)
                    {
                        if (valores[j].nombre.Equals(linea[i]) == true)
                        {
                            aux = 1;
                            break;
                        }
                    }
                    //Si no esta el dato entra
                    if (aux == 0)
                    {
                        validar = validar + 1;
                        Valor valor = new Valor();
                        valor.nombre = linea[i];
                        valor.codigo = codigos[0];
                        codigos.Remove(valor.codigo);
                        cont = 0;
                        for (j = 0; j < valor.codigo.Length; j++)
                        {
                            if (valor.codigo[j].Equals('1') == true)
                            {
                                cont = cont + 1;
                            }

                        }
                        if (cont % 2 == 0)
                        {
                            valor.control = '0';
                        }
                        else
                        {
                            valor.control = '1';
                        }
                        valores.Add(valor);
                        if (validar == 32)
                        {
                            MessageBox.Show("Error! Supera el tamaño.");
                            return valores;
                        }
                    }
                }
            }


            return valores;
        }
        
        public String codificar(string linea, List<Valor> valores)
        {
            String enviado = "";
            int i, j, total, tam;
            Valor valor = new Valor();
            for (i = 0; i < linea.Length; i++)
            {
                for (j = 0; j < valores.Count; j++)
                {
                    valor = valores[j];
                    if (linea[i].Equals(valor.nombre) == true)
                    {
                        enviado = enviado + valor.codigo + " ";
                    }
                }
            }
            String enviado2;
            enviado2 = enviado.Replace(" ", String.Empty);
            tam = (enviado2.Length)/5;
            total = linea.Length;

            if (tam == total)
            {
                return enviado;
            }
            return enviado;
        }

        public String enviar(string linea, List<Valor> valores, Double alfa)
        {
            String recibido = "";
            int i, j;
            Random rand = new Random();
            Valor valor = new Valor();
            for (i = 0; i < linea.Length; i++)
            {
                for (j = 0; j < valores.Count; j++)
                {
                    valor = valores[j];
                    if (linea[i].Equals(valor.nombre) == true)
                    {
                        recibido = recibido + valor.codigo + " ";
                    }
                }
            }
            char x0 = '0', x1 = '1';
            for (i = 0; i < recibido.Length; i++)
            {
                if (recibido[i] == '1' || recibido[i] == '0')
                {
                    if (alfa > rand.NextDouble())
                    {
                        if (recibido[i] == '1')
                        {
                            StringBuilder sb = new StringBuilder(recibido);
                            sb[i] = x0;
                            recibido = sb.ToString();
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder(recibido);
                            sb[i] = x1;
                            recibido = sb.ToString();
                        }
                    }
                }
            }
            return recibido;
        }

        public String salir(string recibido, List<Valor> valores)
        {
            String salida = "";
            int i, aux = 0;
            recibido = recibido.Replace(" ", String.Empty);
            recibido = recibido + "x";
            while (recibido != "x")
            {
                for (i = 0; i < valores.Count; i++)
                {
                    aux = 0;
                    if (recibido.StartsWith(valores[i].codigo))
                    {
                        salida = salida + valores[i].nombre;
                        recibido = recibido.Substring(valores[i].codigo.Length);
                        aux = 1;
                        break;
                    }
                }
                if (aux == 0)
                {
                    salida = "$";
                    break;
                }
            }
            return salida;
        }

    }
}
