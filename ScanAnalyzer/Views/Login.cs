
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ScanAnalyzer.Views
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       
    
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar)
            {
                textBox2.UseSystemPasswordChar = false;

            }
            else
            {
                textBox2.UseSystemPasswordChar = true;

            }
        }



        //

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" || textBox2.Text!="")
            {
                string haspas = GetSHA256(textBox2.Text);

                        Modelo.ScanAnalyzeEntities1 scan= new Modelo.ScanAnalyzeEntities1();

                var us = scan.Usuario.ToList().Where(x => x.usuario1 == textBox1.Text && x.Contraseña == haspas);
                if(us.Any())
                {
                    Principal x = new Principal(us.First());
                    x.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas porfavor ingrese otra vez", "Error", MessageBoxButtons.OK);

                }

            }
            else
            {
                MessageBox.Show("Por favor ingrese ambos campos","Error",MessageBoxButtons.OK);
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

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
