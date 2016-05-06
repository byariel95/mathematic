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
    public partial class adjunta : Form
    {
        public adjunta()
        {
            InitializeComponent();
        }
        int columna, fila;
        double determinate;
        private void button1_Click(object sender, EventArgs e)
        {
            Crear();
        }
        private void limpiacrea()
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView2.ColumnCount = columna;
            dataGridView2.RowCount = fila;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            limpiar();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            if (columna==3)
            {
                
                double valor = 0;
                double[,] matrixde3 = new double[3, 3];
                for (int colu = 0; colu < columna; colu++)
                {
                    for (int fil = 0; fil < fila; fil++)
                    {
                        valor = Convert.ToDouble(dataGridView1[colu, fil].Value);
                        matrixde3[fil, colu] = valor;
                    }
                }
                determinate = deterde3(matrixde3);
                llenar(matrixde3);
              for (int colu2 = 0; colu2 < columna; colu2++)
               {
                   for (int fil2 = 0; fil2 < fila; fil2++)
                    {
                        dataGridView2.Rows.RemoveAt(fil2);
                        dataGridView2.Columns.RemoveAt(colu2);
                        deterde2();
                        llenar(matrixde3);
                  }
              }
              cofactor();
              adjunt();
              inversaa();
               
            }

        }
        static double deterde3(double[,] matriz)
        {
            double valor2;
            valor2 = ((matriz[0, 0] * matriz[1, 1] * matriz[2, 2]) + (matriz[0, 1] * matriz[1, 2] * matriz[2, 0]) + (matriz[0, 2] * matriz[1, 0] * matriz[2, 1]) - (matriz[0, 2] * matriz[1, 1] * matriz[2, 0]) - (matriz[0, 0] * matriz[1, 2] * matriz[2, 1]) - (matriz[0, 1] * matriz[1, 0] * matriz[2, 2]));
            return valor2;
        }
        private void deterde2()
        {
            double valor2;
            valor2 = ((Convert.ToDouble(dataGridView2[0,0].Value) * Convert.ToDouble(dataGridView2[1, 1].Value)) - (Convert.ToDouble(dataGridView2[0, 1].Value) * Convert.ToDouble(dataGridView2[1, 0].Value)));
            listBox1.Items.Add(valor2);
        }
        private void llenar(double[,]matriz3llenar)
        {
            double valor;
            limpiacrea();
            
            for (int colu = 0; colu < columna; colu++)
            {
                for (int fil = 0; fil < fila; fil++)
                {
                    valor = matriz3llenar[fil, colu];
                    dataGridView2[colu, fil].Value = valor;
                    
                }
            }
            
        }
        private void cofactor()
        {
            List<double> vector = new List<double>();
            double numero,cofactor;
            for ( int colu3 = 1; colu3 <= 3;colu3++ )
            {

                for (int fil3 = 1; fil3 <= 3; fil3++)
                {
                    numero = Math.Pow(-1,colu3+fil3);
                    vector.Add(numero);
                }
            }
            
            for (int conta = 0; conta <9; conta++)
            {
                cofactor = (Convert.ToDouble(listBox1.Items[conta]) * vector[conta]);
                listBox2.Items.Add(cofactor);
            }
               
        }
        private void Crear()
        {
            columna = Convert.ToInt32(textBox1.Text);
            fila = Convert.ToInt32(textBox2.Text);
            dataGridView1.ColumnCount = columna;
            dataGridView1.RowCount = fila;
        }
        private void limpiar()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
        }
        private void adjunt()
        {
            int conta=0;
            dataGridView3.ColumnCount = columna;
            dataGridView3.RowCount = fila;
            for (int fila4=0; fila4<columna;fila4++)
            {
                for (int colu4 = 0; colu4 < columna; colu4++)
                {
                    dataGridView3[colu4, fila4].Value = listBox2.Items[conta];
                    conta++;
                }

            }
        }
        private void inversaa()
    {
        limpiacrea();
         double valorfi,valor2;
            valorfi= 1/determinate;
        for (int fila5 = 0; fila5 < columna; fila5++)
        {
            for (int colu5 = 0; colu5< columna; colu5++)
            {
                valor2 = Convert.ToDouble(dataGridView3[colu5, fila5].Value);
                dataGridView2[colu5, fila5].Value = valor2 * valorfi;
                
            }

        }

    }

    }
}
