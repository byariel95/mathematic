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
    public partial class graficafun : Form
    {
        string funcio,funcionn;
        double inicio, fin,resp;
        int conta;
        public graficafun()
        {
            InitializeComponent();
        }

        private void graficafun_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            inicio = Convert.ToDouble(textBox1.Text);
            fin = Convert.ToDouble(textBox2.Text);
            funcionn = textBox3.Text;
            for (double cont = inicio; cont <= fin; cont++)
            {
                conta++;
            }
            dataGridView1.RowCount = conta;
            calcular();
            graficar();
            conta = 0;
        }
        private void calcular ()
        {
            int contador=0;
            Parser evalfuncio = new Parser();
           for (double i = inicio; i <= fin ; i++)
			{
                try
                {
                    funcio = funcionn.Replace("x", i.ToString());
                    evalfuncio.Parse(funcio);
                    resp = evalfuncio.RespuestaNumerica;
                    dataGridView1[1, contador].Value = resp;
                    dataGridView1[0, contador].Value = i;
                    contador++;
                }
                catch 
                {

                    MessageBox.Show("La Funcion Calculada Se Indefine en el Punto: " + i);
                } 
            
			}
            
        }
        private void graficar()
        {
            Parser evalfuncio = new Parser();
            chart1.Series[0].Points.Clear();
            for (double i = inicio; i <= fin; i++)
            {
                try
                {
                    funcio = funcionn.Replace("x", i.ToString());
                    evalfuncio.Parse(funcio);
                    resp = evalfuncio.RespuestaNumerica;
                    if ((resp.ToString() == "∞") || (resp.ToString() == "-∞") || (resp.ToString() == "NaN"))
                    {
                        resp = 0;
                        MessageBox.Show("No se puede Graficar!! La Funcion en el Punto "+i+" Se Indefine");
                        chart1.Series[0].Points.AddXY(i, resp);
                    }
                    else
                    { chart1.Series[0].Points.AddXY(i, resp); }
                    
                }
                catch 
                {

                    MessageBox.Show("Funcion No se Puede Graficar Se Indefine en el punto: " + inicio);
                }
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
    }
}
