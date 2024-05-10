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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KJOAHFI\\SQLEXPRESS;Initial Catalog=hastaneOtomasyonu;Integrated Security=True");

        void comboDoldur()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from klinikler order by klinikAdi asc",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.ValueMember = "klinikNo";
            comboBox1.DisplayMember = "klinikAdi";
            comboBox1.DataSource = dt;
            baglanti.Close();

        }

        void comboDoldur2()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from klinikler order by klinikAdi asc", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.ValueMember = "klinikNo";
            comboBox2.DisplayMember = "klinikAdi";
            comboBox2.DataSource = dt;
            baglanti.Close();
                

        }
        void gridDoktorDoldur()
        {
            baglanti.Close();
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from doktorlar where doktorKlinik="+comboBox2.SelectedValue+"", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource=dt;
            baglanti.Close();


        }
        void gridTumDoktorDoldur()
        {
            baglanti.Close();
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from doktorlar ", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            baglanti.Close();


        }

		private void Kliniksayisi()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select count(*)from klinikler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            label9.Text = "klinik sayısı" + dt.Rows[0][0].ToString();
            baglanti.Close();
        }
        void Doktorsayisi()
        {
            baglanti.Open();
            SqlDataAdapter dam = new SqlDataAdapter("select count(*)from Doktorlar", baglanti);
            DataTable dtm = new DataTable();
            dam.Fill(dtm);
            label10.Text = "doktor sayısı" + dtm.Rows[0][0].ToString();

            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into klinikler values('"+textBox1.Text+"')",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            comboDoldur();
            comboDoldur2();
            Kliniksayisi();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from klinikler where klinikNo="+comboBox1.SelectedValue+"", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            comboDoldur();
            comboDoldur2();
            Kliniksayisi();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into doktorlar values('" + textBox2.Text + "','" + textBox3.Text + "','"+textBox4.Text+"',"+Convert.ToInt32(textBox5.Text)+",'"+textBox6.Text+"',"+comboBox2.SelectedValue+")", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            Doktorsayisi();
            gridDoktorDoldur();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label10.Text = "";
            label9.Text = "";
            comboDoldur();
            comboDoldur2();
            Kliniksayisi();
            Doktorsayisi();
            gridTumDoktorDoldur();
           
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridDoktorDoldur();
           
          
            baglanti.Close();
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select count(*)from doktorlar where doktorKlinik=" + comboBox2.SelectedValue + "", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            label10.Text = "doktor sayısı" + dt.Rows[0][0].ToString();

            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from doktorlar where doktorNo=" + dataGridView1.CurrentRow.Cells[0].Value + "", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            gridDoktorDoldur();
            Doktorsayisi();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update doktorlar set doktorTC='" + textBox2.Text+"', doktorAdiSoyadi='"+textBox3.Text+ "', doktorTel='"+textBox4.Text+ "',doktorDeneyim="+Convert.ToInt32( textBox5.Text)+ ",doktorSifre='"+textBox6.Text+ "',doktorKlinik="+comboBox2.SelectedValue+" where doktorNo="+doktor+"", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            gridDoktorDoldur();
        }
        int doktor;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            doktor =Convert.ToInt32( dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox2.Text =dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text =dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text =dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text =dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text =dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from doktorlar where doktorAdiSoyadi like '%"+textBox7.Text+"%'",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            baglanti.Close();
        }
    }
}
