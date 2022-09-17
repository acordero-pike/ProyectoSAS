using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAnalyzer.Views
{
    public partial class Portanalisis : Form
    {

        PictureBox pictureBox1;
        Panel panelContenedor;
        private class results
        {
            public string port { get; set; }
            public bool TCP { get; set; }
            public bool UDP { get; set; }
        }
        public Portanalisis(PictureBox x, Panel panel)
        {
            InitializeComponent();
            pictureBox1 = x;
            panelContenedor = panel;
        }
        private List<results> res = new List<results>();
        private static int[] Ports = new int[]
   {
        8080,
        51372,
        31146,
        4145,
        80,
        22,
        20,
        443,
        21
   };
        private void button1_Click(object sender, EventArgs e)
        {

            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            String IP = Convert.ToString(localIPs[3]);



            foreach (int s in Ports)
            {
                results r = new results();
                r.port = s.ToString();
                r.TCP = TCPAnalyzer(IP, s);
                r.UDP = UDPAnalyzer(IP, s);
                res.Add(r);
            }


            Mostrar();

        }


        private void Mostrar()
        {
            string show = "";
            int x = 0;

            DataTable dt = new DataTable();
            dt.Columns.Add("Contador");
            dt.Columns.Add("Puerto");
            dt.Columns.Add("TCP");
            dt.Columns.Add("UDP");
            dt.Columns.Add("Comentario");



            foreach (var i in res)
            {
                DataRow row = dt.NewRow();


                row["Contador"] = x + 1;
                row["Puerto"] = i.port;
                row["TCP"] = i.TCP;
                row["UDP"] = i.UDP;
                row["Comentario"] = "";
                dt.Rows.Add(row);

                x++;
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Update();



        }

        private bool TCPAnalyzer(string IP, int s)
        {


            using (TcpClient Scan = new TcpClient())
            {
                try
                {
                    Scan.Connect(IP, s);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

        private bool UDPAnalyzer(string IP, int s)
        {

            using (UdpClient Scan = new UdpClient())
            {
                try
                {
                    Scan.Connect(IP, s);

                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* Process cmd = new Process();
             cmd.StartInfo.FileName = "cmd.exe";
             cmd.StartInfo.RedirectStandardInput = true;
             cmd.StartInfo.RedirectStandardOutput = true;
             cmd.StartInfo.CreateNoWindow = true;
             cmd.StartInfo.UseShellExecute = false;
             cmd.Start();

             cmd.StandardInput.WriteLine("Netstat -an");
             cmd.StandardInput.Flush();
             cmd.StandardInput.Close();
             StreamReader reader = cmd.StandardOutput;
             string output = reader.ReadToEnd();
             string output1 = output.Replace("]","");
             string[] output12 = output1.Split(':');


             MessageBox.Show(output);*/

            DateTime hora = DateTime.Now;
            string fcha_ttal = Convert.ToDateTime(hora).Day + "/" + Convert.ToDateTime(hora).Month + "/" + Convert.ToDateTime(hora).Year;


            Document doc = new Document(PageSize.A4, 9, 9, 10, 10);
            string filename = "C:\\Users\\acordero\\Desktop\\Ports.pdf";
            iTextSharp.text.Chunk encab = new iTextSharp.text.Chunk(" LISTA DE Puertos Analizados en " + fcha_ttal + "", FontFactory.GetFont("COURIER", 12));


            try
            {
                FileStream file = new FileStream(filename, FileMode.OpenOrCreate);

                doc.Open();


                doc.Add(new Paragraph(encab));
                GenerarDocumentos(doc);

                Process.Start(filename);
                doc.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                doc.Close();
            }

        }
        //Función que genera el documento Pdf
        public void GenerarDocumentos(Document document)
        {
            //se crea un objeto PdfTable con el numero de columnas del dataGridView 
            PdfPTable datatable = new PdfPTable(dataGridView1.ColumnCount);

            //asignamos algunas propiedades para el diseño del pdf 
            datatable.DefaultCell.Padding = 1;
            float[] headerwidths = GetTamañoColumnas(dataGridView1);

            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 1;

            //DEFINIMOS EL COLOR DE LAS CELDAS EN EL PDF
            datatable.DefaultCell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;

            //DEFINIMOS EL COLOR DE LOS BORDES
            datatable.DefaultCell.BorderColor = iTextSharp.text.BaseColor.BLACK;

            //LA FUENTE DE NUESTRO TEXTO
            iTextSharp.text.Font fuente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA);

            Phrase objP = new Phrase("A", fuente);

            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            //SE GENERA EL ENCABEZADO DE LA TABLA EN EL PDF 
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {

                objP = new Phrase(dataGridView1.Columns[i].HeaderText, fuente);
                datatable.HorizontalAlignment = Element.ALIGN_CENTER;

                datatable.AddCell(objP);

            }
            datatable.HeaderRows = 2;

            datatable.DefaultCell.BorderWidth = 1;

            //SE GENERA EL CUERPO DEL PDF
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    objP = new Phrase(dataGridView1[j, i].Value.ToString(), fuente);
                    datatable.AddCell(objP);
                }
                datatable.CompleteRow();
            }
            document.Add(datatable);
        }

        //Función que obtiene los tamaños de las columnas del datagridview
        public float[] GetTamañoColumnas(DataGridView dg)
        {
            //Tomamos el numero de columnas
            float[] values = new float[dg.ColumnCount];
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                //Tomamos el ancho de cada columna
                values[i] = (float)dg.Columns[i].Width;
            }
            return values;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Green;
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
