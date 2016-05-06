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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int columna, fila, producto;
        Random numero = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            if ((textcolumna.Text=="") || (textfila.Text ==""))
            {
                MessageBox.Show("Ingrese Cantidades");
               
            }
            else
            {

           
                columna = Convert.ToInt32(textcolumna.Text);
                fila = Convert.ToInt32(textfila.Text);
                dataGridView1.ColumnCount = columna;
                dataGridView1.RowCount = fila;
                dataGridView2.ColumnCount = columna;
                dataGridView2.RowCount = fila;
                dataGridView3.ColumnCount = columna;
                dataGridView3.RowCount = fila;
                for (int conta = 0; conta < columna; conta++)
                {
                    dataGridView1.Columns[conta].Width = 35;
                    dataGridView2.Columns[conta].Width = 35;
                    dataGridView3.Columns[conta].Width = 35;
                    for (int conta2 = 0; conta2 < fila; conta2++)
                    {
                        dataGridView1[conta, conta2].Value = numero.Next(0, 20);
                        dataGridView2[conta, conta2].Value = numero.Next(0, 20);

                    }

                }
             

            }
            
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int valor, valor2,resul;
      
            // sumadorr
           for(int cont =0;cont < columna;cont++)
            {
                for (int cont2 = 0; cont2 < fila;cont2++ )
                {
                    valor = Convert.ToInt32(dataGridView1[cont, cont2].Value.ToString());
                    valor2 = Convert.ToInt32(dataGridView2[cont, cont2].Value.ToString());
                    resul = valor + valor2;
                    dataGridView3[cont, cont2].Value = resul;
                }
                   
            }
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = true;
            dataGridView3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            groupBox1.Enabled = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView3.Visible = true;
            label4.Visible = false;
            label5.Visible = false;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == ""))
            {
                MessageBox.Show("Ingrese Cantidades");

            }
            else
            {

                columna = Convert.ToInt32(textBox2.Text);
                fila = Convert.ToInt32(textBox3.Text);
                producto = Convert.ToInt32(textBox1.Text);
                dataGridView2.ColumnCount = columna;
                dataGridView2.RowCount = fila;
                dataGridView3.ColumnCount = columna;
                dataGridView3.RowCount = fila;
                label9.Text = textBox1.Text;
                for (int conta = 0; conta < columna; conta++)
                {
                    dataGridView2.Columns[conta].Width = 35;
                    dataGridView3.Columns[conta].Width = 35;
                    for (int conta2 = 0; conta2 < fila; conta2++)
                    {
                        dataGridView2[conta, conta2].Value = numero.Next(0, 20);

                    }

                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int valor, valor2, resul;

            // restador
            for (int cont = 0; cont < columna; cont++)
            {
                for (int cont2 = 0; cont2 < fila; cont2++)
                {
                    valor = Convert.ToInt32(dataGridView1[cont, cont2].Value.ToString());
                    valor2 = Convert.ToInt32(dataGridView2[cont, cont2].Value.ToString());
                    resul = valor - valor2;
                    dataGridView3[cont, cont2].Value = resul;
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int  valor2 ,resul;

            // producto
            for (int cont = 0; cont < columna; cont++)
            {
                for (int cont2 = 0; cont2 < fila; cont2++)
                {
                    valor2 = Convert.ToInt32(dataGridView2[cont, cont2].Value.ToString());
                    resul = producto * valor2;
                    dataGridView3[cont, cont2].Value = resul;
                }

            }
        }
    }
}
