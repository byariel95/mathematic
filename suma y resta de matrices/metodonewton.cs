using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.JScript;
using Microsoft.JScript.Vsa; 

namespace suma_y_resta_de_matrices
{
    public partial class metodonewton : Form
    {
        int iteraciones;
        double numeroini, tolerancia, potencia;
        string funcionp, fderivada;
        public metodonewton()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            numeroini = System.Convert.ToDouble(textBox2.Text);
            tolerancia = System.Convert.ToDouble(textBox3.Text);
            iteraciones = System.Convert.ToInt32(textBox1.Text);
            dataGridView1.RowCount = iteraciones;
            funcionp = textBox7.Text;
            fderivada = textBox4.Text;
            funcionprinci();
            
        }
        private string EvalExpression(string expression)
        {
            VsaEngine engine = VsaEngine.CreateEngine();
            try
            {
                object o = Eval.JScriptEvaluate(expression, engine);
                return System.Convert.ToDouble(o).ToString();
            }
            catch
            {
                return "0";
            }
            engine.Close();
        }
        private void funcionprinci()
        
       {
            int cuenta=0;
            string resul,funciones,resul2;
            double valores;
            valores = System.Convert.ToDouble(numeroini);
            for (int contador = 1; contador <= iteraciones; contador++)
            {
                dataGridView1[0, cuenta].Value = valores;

               funciones = funcionp.Replace("x", dataGridView1[0, cuenta].Value.ToString());
                resul = EvalExpression(funciones);
               // funciones = fderivada.Replace("x", dataGridView1[0, cuenta].Value.ToString());
               // resul2 = EvalExpression(funciones);
               // dataGridView1[1, cuenta].Value = System.Convert.ToDouble(dataGridView1[0, cuenta].Value) - (System.Convert.ToDouble(EvalExpression(textBox7.Text.Replace("x",dataGridView1[0,cuenta].Value.ToString()))) / System.Convert.ToDouble(EvalExpression(textBox4.Text.Replace("x",dataGridView1[0,cuenta].Value.ToString()))));
                if ((resul == "∞") || (resul == "-∞") || (resul == "NaN"))
                {
                    resul = "0";
                    dataGridView1[2, cuenta].Value = resul;
                }
                else
                { dataGridView1[2, cuenta].Value = resul; }

                funciones = fderivada.Replace("x", dataGridView1[0, cuenta].Value.ToString());
                resul = EvalExpression(funciones);
                if ((resul == "∞") || (resul == "-∞") || (resul == "NaN"))
                {
                    resul = "0";
                    dataGridView1[3, cuenta].Value = resul;
                }
                else
                { dataGridView1[3, cuenta].Value = resul; }

                dataGridView1[1, cuenta].Value = (System.Convert.ToDouble(dataGridView1[0, cuenta].Value)) - (System.Convert.ToDouble(dataGridView1[2, cuenta].Value) / System.Convert.ToDouble(dataGridView1[3, cuenta].Value)); 

                
                dataGridView1[4, cuenta].Value = Math.Abs((System.Convert.ToDouble(dataGridView1[1, cuenta].Value)) - (System.Convert.ToDouble(dataGridView1[0, cuenta].Value)));
                valores = System.Convert.ToDouble(dataGridView1[1, cuenta].Value);

                if ((System.Convert.ToDouble(dataGridView1[4, cuenta].Value) != 0))
                {
                    if ((System.Convert.ToDouble(dataGridView1[4, cuenta].Value) < tolerancia))
                    {
                        label11.Text = contador.ToString();
                        MessageBox.Show("EL Resultado es: " + System.Convert.ToDouble(dataGridView1[1, cuenta].Value));
                        
                        break;
                    }
                }
                label11.Text = contador.ToString();
                cuenta++;

            }
           
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox7.Clear();
          
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }
      
    }
}
