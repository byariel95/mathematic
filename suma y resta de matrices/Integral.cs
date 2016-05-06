using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathExpressionsCSharp.mathparser;


namespace suma_y_resta_de_matrices
{
    public partial class Integral : Form
    {
        double numero, puntoa, puntob,h,final,total,respues,resultadoa,resultadob;
        string funcion,funcio;
        public Integral()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            puntoa = System.Convert.ToDouble(textBox1.Text);
            puntob = System.Convert.ToDouble(textBox2.Text);
            numero = System.Convert.ToDouble(textBox4.Text);
            funcion = textBox3.Text;

            if (textBox3.Text == "")
            {
                MessageBox.Show("Ingrese Una Funcion ");
            }

            else
            {
                normall();
            }
          
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            label11.Text = "0";  
        }
        private void normall()
        {
            Parser evalfuncio = new Parser();
            double suma = 0, def;
            h = (puntob - puntoa) / numero;
            funcio = funcion.Replace("x", puntoa.ToString());
            evalfuncio.Parse(funcio);
            resultadoa = evalfuncio.RespuestaNumerica;
          
            funcio = funcion.Replace("x", puntob.ToString());
            evalfuncio.Parse(funcio);
            resultadob = evalfuncio.RespuestaNumerica;
           
            final = resultadoa + resultadob;
            final = final / 2;
            for (int conta = 1; conta < numero; conta++)
            {
                def = puntoa + conta * h;
                funcio = funcion.Replace("x", def.ToString());
                evalfuncio.Parse(funcio);
                respues = evalfuncio.RespuestaNumerica;
                suma = suma + respues;
            }

            total = h * (final + suma);
            label11.Text = total.ToString();

        }
       
    
    }
}
