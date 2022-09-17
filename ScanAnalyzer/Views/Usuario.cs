using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAnalyzer.Views
{
    public partial class Usuario : Form
    {
        PictureBox pictureBox;
        Panel panelContenedor;
        Modelo.ScanAnalyzeEntities1 scan= new Modelo.ScanAnalyzeEntities1();
        DataTable dt = new DataTable();
        public Usuario(PictureBox menu, Panel panel)
        {
            InitializeComponent();
            pictureBox = menu;
            panelContenedor = panel;
        }


        private void Usuario_Load(object sender, EventArgs e)
        {

                  

            dt.Columns.Add("#");
            dt.Columns.Add("Nombres");
            dt.Columns.Add("Apellidos");
            dt.Columns.Add("Usuario");
            dt.Columns.Add("Contraseña");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Activo");

            int x = 0;
            foreach (var i in scan.Usuario.ToList())
            {
                DataRow row = dt.NewRow();


                row["#"] =x+1;
                row["Nombres"] = i.Nombres;
                row["Apellidos"] = i.Apellidos;
                row["Usuario"] = i.usuario1;
                row["Contraseña"] = i.Contraseña;
                row["Tipo"] = i.Tipo;
                row["Activo"] = i.Activo;
                dt.Rows.Add(row);

                x++;
            }

           
            dataGridView1.DataSource = dt;
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.ColumnHeader);
            dataGridView1.Update();


            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox4.UseSystemPasswordChar = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox5.UseSystemPasswordChar = false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" || comboBox1.SelectedIndex > 0)
            {
                if (textBox4.Text == textBox5.Text)
                {
                    if (!scan.Usuario.ToList().Where(x => x.usuario1 == textBox3.Text).Any())
                    {
                        Modelo.Usuario us = new Modelo.Usuario();
                        us.UUID = Guid.NewGuid().ToString();
                        us.Nombres = textBox1.Text;
                        us.Apellidos = textBox2.Text;
                        us.usuario1 = textBox3.Text;
                        us.Contraseña = GetSHA256(textBox4.Text);
                        us.Tipo = comboBox1.SelectedItem.ToString();
                        us.Activo = true;

                       try
                        {
                            
                            scan.Usuario.Add(us);
                            scan.SaveChanges();
                            actualizar(us);
                            
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("ex : " + ex.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("El usuario ingresado ya se ha registrado anteriormente, por favor ingrese otro usuario y vuelva a intentar", "Error", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("La contraseña coincide, por favor ingrese nuevamente las contraseñas", "Error", MessageBoxButtons.OK);
                }
            }

            else
            {
                MessageBox.Show("Por favor llene todos los recuadros para ingreso de usuarios", "Error", MessageBoxButtons.OK);

            }

        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }


        public void actualizar(Modelo.Usuario us)
        {
           

            int x = scan.Usuario.ToList().Count();
           
                DataRow row = dt.NewRow();


                    row["#"] = x ;
                    row["Nombres"] = us.Nombres;
                    row["Apellidos"] = us.Apellidos;
                    row["Usuario"] = us.usuario1;
                    row["Contraseña"] = us.Contraseña;
                    row["Tipo"] = us.Tipo;
                    row["Activo"] = us.Activo;
                    dt.Rows.Add(row);
               
            


            dataGridView1.DataSource = dt;
            dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.ColumnHeader);
            dataGridView1.Update();
        }
    }
}

