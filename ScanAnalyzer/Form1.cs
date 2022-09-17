using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace ScanAnalyzer
{
    public partial class Principal : Form
    {

        private bool end=true;

        PictureBox current;
        public Principal(Modelo.Usuario rol)
        {
            InitializeComponent();
            this.label1.Text = rol.usuario1;
            Validar(rol.Tipo);
        }

        private void Validar(string rol)
        {
            if (rol!= "Administrador")
            {
                pictureBox4.Visible = false;

                pictureBox5.Location = new Point(26, 120);
                pictureBox6.Location = new Point(26, 240);
                pictureBox7.Location = new Point(26, 340);
            }
            
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Views.Inicio());
        }
        private void AbrirFormInPanel(object Formhijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           if (end)
            {
                DialogResult res = MessageBox.Show("Enserio desea salir del Aplicativo?", "Warning", MessageBoxButtons.OKCancel);

                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    end = false;
                    salir();
                }
               
            }
           



        }

        private void salir()
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            colorreturn();
            pictureBox3.BackColor = Color.LightBlue;
            current = pictureBox3;
            AbrirFormInPanel(new Views.Perfil(pictureBox3, panelContenedor));
        }

      

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            colorreturn();
            pictureBox4.BackColor = Color.LightBlue;
            current = pictureBox4;
            AbrirFormInPanel(new Views.Usuario(pictureBox4, panelContenedor));

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            colorreturn();
            pictureBox5.BackColor = Color.LightBlue;
            current = pictureBox7;
            AbrirFormInPanel(new Views.Portanalisis(pictureBox5, panelContenedor));
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            colorreturn();
            pictureBox6.BackColor = Color.LightBlue;
            current = pictureBox6;
            AbrirFormInPanel(new Views.DBanalis(pictureBox6, panelContenedor));
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            colorreturn();
            pictureBox7.BackColor = Color.LightBlue;
            current = pictureBox7;
            AbrirFormInPanel(new Views.Reporte(pictureBox7, panelContenedor));
        }


        private void colorreturn()
        {
            if (current!=null)
            {
                current.BackColor = Color.Transparent;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dr =  MessageBox.Show("Desea Salir del sistema?", "Warnign", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    Views.Login log = new Views.Login();
                    log.Show();
                    this.Hide();
                    break;
                case DialogResult.No:

                    break;
            }
        }
    }
}
