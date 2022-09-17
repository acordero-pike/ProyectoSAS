using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAnalyzer.Views
{
    public partial class Perfil : Form
    {
        PictureBox pictureBox1;
        Panel panelContenedor;
        public Perfil(PictureBox x, Panel panel)
        {
            InitializeComponent();
            pictureBox1 = x;
            panelContenedor = panel;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
           Views.Inicio fh = new Views.Inicio();
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();

            this.Close();
        }
    }
}
