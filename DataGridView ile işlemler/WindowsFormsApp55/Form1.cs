using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WindowsFormsApp55
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=aaa;Integrated Security=True");
        BindingSource bs = new BindingSource();
        SqlCommand cmd = new SqlCommand();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            doldur();
        }
        
        void doldur()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from kimlik", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
        }

        void kayitSil(int num)
        {
            con.Open();
            cmd = new SqlCommand("delete from kimlik where okulno=" + num, con);
            cmd.ExecuteNonQuery();
            con.Close();
            doldur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Interaction.InputBox("Kaç numaralı kişiyi sileceksiniz?", "Kayıt silme"), out int id))
            {
                string devam = Interaction.InputBox("Devam etmek istiyorsanız 'E' tuşlayınız", "Silmek üzeresiniz");
                if ("E" == devam.ToString()) kayitSil(id);
            }
            else
                Interaction.MsgBox("Yanlış tuşlama yaptınız. Tekrar deneyiniz"); 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int num = Convert.ToInt32(drow.Cells[0].Value);
                kayitSil(num);
            }
            doldur();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //string secili = dataGridView1.SelectedCells[0].Value.ToString(); //Seçili hücrenin değerini verir.
            string sat =dataGridView1.CurrentRow.Cells[0].Value.ToString(); //Seçili satırdaki ilk hücre değerini almamızı sağlar.
            string alanA= dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name.ToString(); //Seçili hücrenin alan adını almamızı sağlar.
            string yeni = Interaction.InputBox("Yeni değer giriniz:");
            con.Open();
            cmd = new SqlCommand("update kimlik set " + alanA + "='"+yeni +"' where okulno=" + sat, con);
            cmd.ExecuteNonQuery();
            con.Close();
            doldur();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("Insert into kimlik(ad,soyad,tc,adres) values('"+textBox1.Text+"','"+textBox2.Text+"',"+textBox3.Text+",'"+textBox4.Text+"')",con);
            cmd.ExecuteNonQuery();
            con.Close();
            doldur();
        }
    }
}
