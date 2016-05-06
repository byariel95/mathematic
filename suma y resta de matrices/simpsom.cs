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
    public partial class simpsom : Form
    {
        double numero, puntoa, puntob, h, total,ax;
        string funcion, funcio;
        public simpsom()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            puntoa = Convert.ToDouble(textBox1.Text);
            puntob = Convert.ToDouble(textBox2.Text);
            numero = Convert.ToDouble(textBox4.Text);
            funcion = textBox3.Text;
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
            bool sies = true;
            double suma = 0, def, dea, deb, respu;
            h = (puntob - puntoa) / numero;
            ax = h / 3;
            funcio = funcion.Replace("x", puntoa.ToString());
            evalfuncio.Parse(funcio);
            dea = evalfuncio.RespuestaNumerica;
            funcio = funcion.Replace("x", puntob.ToString());
            evalfuncio.Parse(funcio);
            deb = evalfuncio.RespuestaNumerica;
            for (int conta = 1; conta < numero; conta++)
            {
                def = puntoa + conta * h;
                funcio = funcion.Replace("x", def.ToString());
                evalfuncio.Parse(funcio);
                respu = evalfuncio.RespuestaNumerica;
                if (sies == true)
                {
                    respu = 4 * respu;
                    suma = suma + respu;
                    sies = false;
                }
                else
                {
                    respu = 2 * respu;
                    suma = suma + respu;
                    sies = true;
                }

            }
            total = ax * (dea + suma + deb);
            label11.Text = total.ToString();
        }
       
        

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
       
    }
}
