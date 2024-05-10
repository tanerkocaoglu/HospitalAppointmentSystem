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
    public partial class Muayene : Form
    {
        public Muayene()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KJOAHFI\\SQLEXPRESS;Initial Catalog=hastaneOtomasyonu;Integrated Security=True");
        private void Muayene_Load(object sender, EventArgs e)
        {

          
            label2.Text = Form2.doktorTC;


            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select doktorNo from doktorlar where doktorTC='" + label2.Text + "'", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);

            label4.Text=dt.Rows[0][0].ToString();
            baglanti.Close();


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from randevular where randevuTarih='" + dateTimePicker1.Value.ToShortDateString() + "' and doktorNo=" + Convert.ToInt32(label4.Text) + "", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            baglanti.Close();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label6.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand ekle = new SqlCommand("insert into muayene values('"+label6.Text+ "','"+label2.Text+ "','"+dateTimePicker1.Value.ToShortDateString()+ "','"+richTextBox1.Text+ "','"+richTextBox2.Text+ "',"+numericUpDown1.Value+")", baglanti);
            if (ekle.ExecuteNonQuery() > 0)
            {

                MessageBox.Show("Muayene işlemi tamamlandı..");

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
