using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace suma_y_resta_de_matrices
{
    public partial class biseccion : Form
    {
        public biseccion()
        {
            InitializeComponent();
        }
        public static string funcion2(string texto)
        {
            Microsoft.JScript.Vsa.VsaEngine myEngine = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();
            return Microsoft.JScript.Eval.JScriptEvaluate(texto, myEngine).ToString(); 
        }
       
              
        double numa, numb, iteraciones,tolerancia;
        private void button1_Click(object sender, EventArgs e)
        {
            //string valor,valor2;
            double potencia,p,funciondep,tole2,funciondea;
            numa = Convert.ToDouble(textBox1.Text);
            numb = Convert.ToDouble(textBox2.Text);
            iteraciones = Convert.ToInt32(textBox5.Text);
            tolerancia = Convert.ToDouble(textBox4.Text);
            potencia = Convert.ToDouble(textBox6.Text);
            tolerancia = Math.Pow(tolerancia, potencia);
            for (int contador = 1; contador <= iteraciones; contador++)
            {
                listBox1.Items.Add(numa);
                listBox2.Items.Add(numb);
                p = numa + ((numb - numa) / 2);
                listBox3.Items.Add(p);
               funciondep = funcion(p);
               
               // valor = funcion2(textBox3.Text.Replace("x", p.ToString()));
              
                listBox5.Items.Add(funciondep);
              //  listBox5.Items.Add(funcion2(textBox3.Text.Replace("x", p.ToString())));
               if ((funciondep==0) || ((numb - numa) / 2)<tolerancia)
                {
                   MessageBox.Show("se acabo");
                   label15.Text = contador.ToString();
                   break;

                  
                }
                funciondea = funcion(numa);
               //valor2 = funcion2(textBox3.Text.Replace("x", numa.ToString()));
                listBox4.Items.Add(funciondea);
                //listBox4.Items.Add(valor2);
                listBox7.Items.Add(Convert.ToDouble(listBox4.Items[contador - 1]) * Convert.ToDouble(listBox5.Items[contador - 1]));
                tole2 = toleran(numa, numb);
                listBox6.Items.Add(tole2);
                if ((Convert.ToDouble(listBox7.Items[contador-1])>0))
                {
                    numa=Convert.ToDouble(listBox3.Items[contador-1]);
                }
                else
                { numb = Convert.ToDouble(listBox3.Items[contador - 1]); }


                label15.Text = contador.ToString();
            }

        }
        static double funcion(double p)
        {
            double resul;
             // funcion x^2+3x-8
            resul = ((Math.Pow(p, 3)) +(Math.Pow(p, 2)) +2 * p - 10);
          
            return resul;
        }
        static double toleran(double a,double b)
        {
            double resulto;
            resulto = (b - a) / 2;
            return resulto;
        }

        private void biseccion_Load(object sender, EventArgs e)
        {

        }


    }
}
