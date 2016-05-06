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
    public partial class determinante : Form
    {
        public determinante()
        {
            InitializeComponent();
        }
        int columna, fila;
       
        private void button1_Click(object sender, EventArgs e)
        {
            columna = Convert.ToInt32(textBox1.Text);
            fila = Convert.ToInt32(textBox2.Text);
            dataGridView1.ColumnCount = columna;
            dataGridView1.RowCount = fila;

        }
        private void para2 ()
        {
            double valor,valor2;
            double[,] matrix2 = new double [2,2];
            for (int colu = 0; colu<columna;colu++)
            {
                for (int fil = 0; fil <fila;fil++)
                {
                    valor = Convert.ToDouble(dataGridView1[colu, fil].Value);
                    matrix2[fil,colu] = valor;
                }
            }
            valor2 = ((matrix2[0, 0] * matrix2[1, 1]) - (matrix2[0, 1] * matrix2[1, 0]));
            MessageBox.Show("La Determinate es :" + valor2);
        }
        static double para3(double[,]matriz)
        {
            double valor2;
            valor2 = ((matriz[0, 0] * matriz[1, 1] * matriz[2, 2]) + (matriz[0, 1] * matriz[1, 2] * matriz[2, 0]) + (matriz[0, 2] * matriz[1, 0] * matriz[2, 1]) - (matriz[0, 2] * matriz[1, 1] * matriz[2, 0]) - (matriz[0, 0] * matriz[1, 2] * matriz[2, 1]) - (matriz[0, 1] * matriz[1, 0] * matriz[2, 2]));
            return valor2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (columna == 2)
            {
                para2();
            }
            if (columna == 3)
            {
                double valor=0;
                double[,] matrix2 = new double[3, 3];
                for (int colu = 0; colu < columna; colu++)
                {
                    for (int fil = 0; fil < fila; fil++)
                    {
                        valor = Convert.ToDouble(dataGridView1[colu, fil].Value);
                        matrix2[fil, colu] = valor;
                    }
                }
                valor = para3(matrix2);
                MessageBox.Show("La Determinate es :"+valor);
               
            }
            if (columna == 7)
            {
                double valor;
                double[,] m = new double[fila, columna];
                for (int colu = 0; colu < columna; colu++)
                {
                    for (int fil = 0; fil < fila; fil++)
                    {
                        valor = Convert.ToDouble(dataGridView1[colu, fil].Value);
                        m[fil, colu] = valor;
                    }
                }
                para4(m);
            }
            if (columna >=4)
            {
                decimal valor;
                decimal[,] m = new decimal[fila, columna];
                for (int colu = 0; colu < columna; colu++)
                {
                    for (int fil = 0; fil < fila; fil++)
                    {
                        valor = Convert.ToDecimal(dataGridView1[colu, fil].Value);
                        m[fil, colu] = valor;
                    }
                }
               valor = Determinante(m);
               MessageBox.Show("La Derminante es " + valor);
            }
        }
       
        private decimal[,] ElimFilCol(decimal[,] a, int fila, int column)
        {
            decimal[,] result = new decimal[a.GetLength(0) - 1, a.GetLength(1) - 1];
            bool fil = false;
            bool col = false;
            for (int i = 0; i < result.GetLength(0); i++)
            {
                col = false;
                if (i == fila) { fil = true; }
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    if (j == column) { col = true; }
                    if (!fil && !col) { result[i, j] = a[i, j]; }
                    if (!fil && col) { result[i, j] = a[i, j + 1]; }
                    if (fil && !col) { result[i, j] = a[i + 1, j]; }
                    if (fil && col) { result[i, j] = a[i + 1, j + 1]; }

                }
            }
            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
           
        }
        private void para4(double [,] matriz4)
        {
            
            double[] valores = new double[columna];
            double[] determinantes = new double[columna];
            double[,] de3 = new double[3, 3];
            for (int fil3= 0;fil3<columna-1;fil3++)
            {
                 
                for (int col3=0;col3 <columna-1;col3++)
                {
                    
                    for (int fil4 = 0; fil4 < columna; fil4++)
                    {
                        valores[fil4] = matriz4[0, fil4];  
                        for (int col4 = 0; col4 < columna;col4++ )
                        {
                            if (fil4 != fil3)
                            {
                                de3[fil3, col3] = matriz4[fil4 ,col4+1 ];
                            }
                            else
                            {
                               de3[fil3, col3] = matriz4[fil4 + 1, col4+1];
                            }
                        }
                            
                    }
                    
                   
                }
            }
        }
        private decimal Determinante(decimal[,] m)
        {
            decimal determinante = 0;


            if (m.Length == 1)
                return m[0, 0];

            else
            {
                for (int i = 0; i < m.GetLength(0); i++)
                {
                    determinante += (decimal)Math.Pow(-1, i) * m[i, 0] * Determinante(ElimFilCol(m, i, 0));
                    
                }
            }
            return determinante;
            
        }
       
    }
}
