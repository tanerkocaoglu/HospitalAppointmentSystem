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

namespace hastaneOtomasyonuProjesi
{
    public partial class Randevu : Form
    {
        public Randevu()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KJOAHFI\\SQLEXPRESS;Initial Catalog=hastaneOtomasyonu;Integrated Security=True");

        private void Randevu_Load(object sender, EventArgs e)
        {
            label1.Text = Form2.hastaTC;



            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from klinikler order by klinikAdi asc", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.ValueMember = "klinikNo";
            comboBox1.DisplayMember = "klinikAdi";
            comboBox1.DataSource = dt;
            baglanti.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Close();
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from doktorlar where doktorKlinik=" + comboBox1.SelectedValue + "", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            listBox1.ValueMember = "doktorNo";
            listBox1.DisplayMember = "doktorAdiSoyadi";
            listBox1.DataSource = dt;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into randevular values('" + label1.Text + "'," + listBox1.SelectedValue + ",'" + dateTimePicker1.Value.ToShortDateString() + "')", baglanti);
            if (komut.ExecuteNonQuery() > 0)
            {
                MessageBox.Show(dateTimePicker1.Value.ToShortDateString()+" Randevunuz Oluşturuldu....");
                button1.Enabled = false;



            }
            baglanti.Close();



            

        }

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
