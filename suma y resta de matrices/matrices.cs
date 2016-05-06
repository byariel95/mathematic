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
    public partial class matrices : Form
    {
        public matrices()
        {
            InitializeComponent();
        }
        int columna, fila;
        double valor1, valor2, factor, valor;
        
        Random numeros = new Random();
        

        private void button1_Click(object sender, EventArgs e)
        {
            columna = Convert.ToInt32(textBox1.Text);
            fila = Convert.ToInt32(textBox2.Text);
            dataGridView1.ColumnCount = columna+1;
            dataGridView1.RowCount = fila;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void limpiar()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            listBox1.Items.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            valor = Convert.ToDouble(dataGridView1[0, 0].Value);
            if (valor == 0)
            {
                MessageBox.Show("El Sitema No Tiene Solucion");
                limpiar();
            }
            else
            {
                for (int cont = 0; cont < columna - 1; cont++)
                {
                    for (int fila2 = cont + 1; fila2 < columna; fila2++)
                    {
                        valor1 = Convert.ToDouble(dataGridView1[cont, fila2].Value);
                        valor2 = Convert.ToDouble(dataGridView1[cont, cont].Value);
                        factor = valor1 / valor2;
                        for (int oppe = cont; oppe <= columna; oppe++)
                        {
                            valor1 = Convert.ToDouble(dataGridView1[oppe, fila2].Value);
                            valor2 = Convert.ToDouble(dataGridView1[oppe, cont].Value);
                            valor = valor1 - (valor2 * factor);
                            dataGridView1[oppe, fila2].Value = valor;

                        }
                    }
                }
                valor = Convert.ToDouble(dataGridView1[columna - 1, fila - 1].Value);
                if (valor == 0)
                {
                    MessageBox.Show("El Sitema No Tiene Solucion");
                    limpiar();

                }
                else
                { sustituir(); }
            }
           
           
        }
      
        private void button3_Click(object sender, EventArgs e)
        {
            limpiar();
           
        }
        private void sustituir()
    {
        double[] resultado = new double[columna];
        valor1 = Convert.ToDouble(dataGridView1[columna, columna - 1].Value);
        valor2 = Convert.ToDouble(dataGridView1[columna - 1, columna - 1].Value);
        valor = valor1 / valor2;
        resultado[columna - 1] = valor;
        for (int conta2 = columna - 2; conta2 >= 0; conta2--)
        {
            double sum = 0;
            for (int conta3 = columna - 1; conta3 > conta2; conta3--)
            {
                valor1 = Convert.ToDouble(dataGridView1[conta3, conta2].Value);

                sum += valor1 * resultado[conta3];
            }
            valor2 = Convert.ToDouble(dataGridView1[columna, conta2].Value);
            valor = Convert.ToDouble(dataGridView1[conta2, conta2].Value);
            resultado[conta2] = (valor2 - sum) / valor;
        }
        for (int final = 0; final < columna; final++)
        {
           
            listBox1.Items.Add(resultado[final]);
        }
    }
    }
}
