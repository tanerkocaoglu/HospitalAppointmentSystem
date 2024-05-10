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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KJOAHFI\\SQLEXPRESS;Initial Catalog=hastaneOtomasyonu;Integrated Security=True");
        public static string hastaTC="";
        public static string doktorTC = "";
        private void button1_Click(object sender, EventArgs e)
        {
           
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from yetkili where kullaniciAdi='"+textBox3.Text+"' and kullaniciSifre='"+textBox4.Text+"'", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                Form1 frm = new Form1();
                frm.Show();
                this.Hide();

            }

            else
            {
                MessageBox.Show("Hatalı Giriş !!!");

            }


            baglanti.Close();

           
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into hastalar values('" + textBox1.Text + "','"+textBox2.Text+"','"+textBox5.Text+ "','"+textBox6.Text+ "','"+textBox7.Text+ "','"+textBox8.Text+"')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Close();
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from hastalar where hastaTC='" + textBox9.Text + "' and sifre='" + textBox10.Text + "'", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // MessageBox.Show("Hasta Girişi Başarılı");
                hastaTC = textBox9.Text;
                Randevu randevuFormu = new Randevu();
                randevuFormu.Show();
                this.Hide();

            }
            else
            {
                 MessageBox.Show("Hatalı giriş, ya da sisteme kayıt olmalısınız");

            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Close();
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from doktorlar where doktorTC='" + textBox11.Text + "' and doktorSifre='" + textBox12.Text + "'", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                doktorTC = textBox11.Text;
                Muayene formMuayene = new Muayene();
                formMuayene.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Hatalı giriş, ya da sisteme kayıt olmalısınız");

            }




           
        }
    }
}
