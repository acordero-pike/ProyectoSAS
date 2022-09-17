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
    public partial class Reporte : Form
    {
        PictureBox pictureBox1;
        Panel panelContenedor;
        public Reporte(PictureBox menu, Panel panel)
        {
            InitializeComponent();
            pictureBox1 = menu;
            panelContenedor = panel;
        }


        private void Reporte_Load(object sender, EventArgs e)
        {

        }
    }
}
