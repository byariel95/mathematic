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
    public partial class puntofijo : Form
    {
        public puntofijo()
        {
            InitializeComponent();
        }
        int iteraciones;
        double numeroini, tolerancia,potencia;
        string fu, f1, f2, f3, f4, f5;
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            numeroini = System.Convert.ToDouble(textBox2.Text);
            tolerancia = System.Convert.ToDouble(textBox3.Text);
            potencia = System.Convert.ToDouble(textBox10.Text);
            tolerancia = Math.Pow(tolerancia, potencia);
            f1 = textBox4.Text;
            f2 = textBox5.Text;
            f3 = textBox6.Text;
            f4 = textBox8.Text;
            f5 = textBox9.Text;
            iteraciones = System.Convert.ToInt32(textBox1.Text);
            dataGridView1.RowCount = iteraciones;
            funcioprici();
        }
        private void funcioprici()
        {
            string resul,f11;
            
            dataGridView1[0, 0].Value = numeroini;
            dataGridView1[1, 0].Value = numeroini;
            dataGridView1[2, 0].Value = numeroini;
            dataGridView1[3, 0].Value = numeroini;
            dataGridView1[4, 0].Value = numeroini;
            int conta = 0;
            for (int contador = 1; contador < iteraciones; contador++)
            {
                
                
                f11 = f1.Replace("x",dataGridView1[0, conta].Value.ToString());
                resul = EvalExpression(f11);
                if ((resul == "∞") || (resul == "-∞") || (resul == "NaN"))
                {
                    resul = "0";
                    dataGridView1[5, conta].Value = resul; }
                else
                { dataGridView1[5, conta].Value = resul; }
                dataGridView1[10, conta].Value = Math.Abs((System.Convert.ToDouble(dataGridView1[5, conta].Value)) - (System.Convert.ToDouble(dataGridView1[0, conta].Value)));
                dataGridView1[0, conta+1].Value = resul;


                f11 = f2.Replace("x", dataGridView1[1, conta ].Value.ToString());
                resul = EvalExpression(f11);
                if ((resul == "∞") || (resul == "-∞") || (resul == "NaN"))
                {
                    resul = "0";
                    dataGridView1[6, conta].Value = resul;
                }
                else
                { dataGridView1[6, conta].Value = resul; }
                dataGridView1[11, conta].Value = Math.Abs((System.Convert.ToDouble(dataGridView1[6, conta].Value)) - (System.Convert.ToDouble(dataGridView1[1, conta].Value)));
                dataGridView1[1, conta+1].Value = resul;


                f11 = f3.Replace("x", dataGridView1[2, conta].Value.ToString());
                resul = EvalExpression(f11);
                if ((resul == "∞") || (resul == "-∞") || (resul == "NaN"))
                {
                    resul = "0";
                    dataGridView1[7, conta].Value = resul;
                }
                else
                { dataGridView1[7, conta].Value = resul; }
                dataGridView1[12, conta].Value = Math.Abs((System.Convert.ToDouble(dataGridView1[7, conta].Value)) - (System.Convert.ToDouble(dataGridView1[2, conta].Value)));
                dataGridView1[2, conta+1].Value = resul;


                f11 = f4.Replace("x", dataGridView1[3, conta].Value.ToString());
                resul = EvalExpression(f11);
                if ((resul == "∞") || (resul == "-∞") || (resul == "NaN"))
                {
                    resul = "0";
                    dataGridView1[8, conta].Value = resul;
                }
                else
                { dataGridView1[8, conta].Value = resul; }
                dataGridView1[13, conta].Value = Math.Abs((System.Convert.ToDouble(dataGridView1[8, conta].Value)) - (System.Convert.ToDouble(dataGridView1[3, conta].Value)));
                dataGridView1[3, conta+1].Value = resul;


                f11 = f5.Replace("x", dataGridView1[4, conta].Value.ToString());
                resul = EvalExpression(f11);
                if ((resul == "∞") || (resul == "-∞") || (resul == "NaN"))
                {
                    resul = "0";
                    dataGridView1[9, conta].Value = resul;
                }
                else
                { dataGridView1[9, conta].Value = resul; }
               dataGridView1[14, conta].Value = Math.Abs((System.Convert.ToDouble(dataGridView1[9, conta].Value)) - (System.Convert.ToDouble(dataGridView1[4, conta].Value)));
                dataGridView1[4, conta+1].Value = resul;


                if((System.Convert.ToDouble(dataGridView1[10, conta].Value)!= 0))
                {
                    if ((System.Convert.ToDouble(dataGridView1[10, conta].Value) <tolerancia))
                    {
                        MessageBox.Show("EL Resultado es: "+System.Convert.ToDouble(dataGridView1[10, conta].Value));
                          break;
                    }
                }

                if ((System.Convert.ToDouble(dataGridView1[11, conta].Value) != 0))
                {
                    if ((System.Convert.ToDouble(dataGridView1[11, conta].Value) < tolerancia))
                    {
                        MessageBox.Show("EL Resultado es: " + System.Convert.ToDouble(dataGridView1[11, conta].Value));
                        break;
                    }
                }

                if ((System.Convert.ToDouble(dataGridView1[12, conta].Value) != 0))
                {
                    if ((System.Convert.ToDouble(dataGridView1[12, conta].Value) < tolerancia))
                    {
                        MessageBox.Show("EL Resultado es: " + System.Convert.ToDouble(dataGridView1[12, conta].Value));
                        break;
                    }
                }

                if ((System.Convert.ToDouble(dataGridView1[13, conta].Value) != 0))
                {
                    if ((System.Convert.ToDouble(dataGridView1[13, conta].Value) < tolerancia))
                    {
                        MessageBox.Show("EL Resultado es: " + System.Convert.ToDouble(dataGridView1[13, conta].Value));
                        break;
                    }
                }

                if ((System.Convert.ToDouble(dataGridView1[14, conta].Value) != 0))
                {
                    if ((System.Convert.ToDouble(dataGridView1[14, conta].Value) < tolerancia))
                    {
                        MessageBox.Show("EL Resultado es: " + System.Convert.ToDouble(dataGridView1[14, conta].Value));
                        break;
                    }
                }
              
                label11.Text = contador+1.ToString();
                conta++;

            }
        }

        private void puntofijo_Load(object sender, EventArgs e)
        {

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
    }
}
