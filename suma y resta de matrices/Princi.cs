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
    public partial class Princi : Form
    {
        public Princi()
        {
            InitializeComponent();
        }

        private void sumaYRestaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 matriz = new Form1();
            matriz.MdiParent= this ;
            matriz.Show();
        }

        private void Princi_Load(object sender, EventArgs e)
        {
            MdiClient ctlMDI;

           
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;

                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = this.BackColor;
                }
                catch (InvalidCastException )
                {
                    // Catch and ignore the error if casting failed.
                }
            }
        }

        private void ecuacionesNxmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matrices opp = new matrices ();
            opp.MdiParent= this;
            opp.Show();
        }

        private void determinanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            determinante deter = new determinante();
            deter.MdiParent = this;
            deter.Show();
        }

        private void adjuntaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adjunta adjun = new adjunta();
            adjun.MdiParent = this;
            adjun.Show();
        }

        private void biseccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            biseccion bise = new biseccion();
            bise.MdiParent = this;
            bise.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void puntoFijoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puntofijo fijo = new puntofijo();
            fijo.MdiParent = this;
            fijo.Show();
        }

        private void metodoNewtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            metodonewton newton = new metodonewton();
            newton.MdiParent = this;
            newton.Show();

        }

        private void metodoStefensonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            metodosteff stef = new metodosteff();
            stef.MdiParent = this;
            stef.Show();
        }

        private void metodoTrapecioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Integral netodo = new Integral();
            netodo.MdiParent = this;
            netodo.Show();

        }

        private void simpsomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simpsom metodo = new simpsom();
            metodo.MdiParent = this;
            metodo.Show();
        }

        private void puntoMedioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puntomedio puntomed = new puntomedio();
            puntomed.MdiParent = this;
            puntomed.Show();
        }

        private void graficadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graficafun graficador = new graficafun();
            graficador.MdiParent = this;
            graficador.Show();
        }

        private void derivadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            derivadas deri = new derivadas();
            deri.MdiParent = this;
            deri.Show();
        }
    }
}
