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
    public partial class puntomedio : Form
    {
        double numero, puntoa, puntob, h, total, ax;
        string funcion, funcio;
        public puntomedio()
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
            double suma = 0, def, respu;
            h = (puntob - puntoa) / numero;
            for (int conta = 0; conta < numero; conta++)
            {
                def = puntoa + (conta * h) + (h / 2);
                funcio = funcion.Replace("x", def.ToString());
                evalfuncio.Parse(funcio);
                respu = evalfuncio.RespuestaNumerica;
                suma = suma + respu;

            }
            total = suma*h;
            label11.Text = total.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
