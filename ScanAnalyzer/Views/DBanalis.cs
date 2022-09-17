using ScanAnalyzer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAnalyzer.Views
{
    public partial class DBanalis : Form
    {
        PictureBox pictureBox1;
        Panel panelContenedor;
        public DBanalis(PictureBox menu, Panel panel)
        {
            InitializeComponent();
            pictureBox1 = menu;
            panelContenedor = panel;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
        }


        private void DBanalis_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Items.Clear();

            //jalar las bases de datos 
            string sCnn = "Server=" + textBox1.Text + "; database=master;user id=" + textBox2.Text + ";password=" + textBox3.Text + "; ";

            // La orden T-SQL para recuperar las bases de master
            string sel = "SELECT name FROM sysdatabases";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sel;
                SqlConnection cnn = new SqlConnection() { ConnectionString = sCnn };

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                cmd.Connection = cnn;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(0));
                    }

                }
                cnn.Close();
                comboBox2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" +ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string sCnn = "Server=" + textBox1.Text + "; database="+comboBox1.SelectedItem.ToString()+";user id=" + textBox2.Text + ";password=" + textBox3.Text + "; ";

            // La orden T-SQL para recuperar las bases de master
            string sel = "select TABLE_NAME from INFORMATION_SCHEMA.TABLES";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sel;
                SqlConnection cnn = new SqlConnection() { ConnectionString = sCnn };

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                cmd.Connection = cnn;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));
                    }

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }


        private void validar()
        {
            if(textBox1.Text!="" && textBox2.Text!="" && textBox3.Text!="")
            {
                comboBox1.Enabled = true;
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            validar();
        }
        

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            validar();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            validar();
        }
    }
}
