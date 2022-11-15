using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace WindowsFormsApp53
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=dene;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            datagridd();
        }

        private void datagridd()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from ogrenci",con);
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from ders", con);
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from sinav", con);

            DataTable dt1 = new DataTable("ogrenci");
            DataTable dt2 =  new DataTable("ders");
            DataTable dt3 =  new DataTable("sinav");

            da1.Fill(dt1);
            da2.Fill(dt2);
            da3.Fill(dt3);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);

            DataRelation dr = new DataRelation("abc", dt1.Columns["no"], dt2.Columns["ders_no"]);
            dr = new DataRelation("abc", dt2.Columns["ders_no"], dt3.Columns["ders_no"]);

            ds.Relations.Add(dr);

            dataGrid1.DataSource = ds;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgview();

            Hashtable ht = new Hashtable();
            ht.Add("ad", Interaction.InputBox("Ad giriniz"));
            ht.Add("soyad", Interaction.InputBox("Soyad giriniz"));
            ht.Add("telefon", Interaction.InputBox("telefon no giriniz"));

            string anahtar = "", deger = "";
            foreach (var i in ht.Keys)
                anahtar += i + ",";

            foreach (var k in ht.Values)
                deger += "'" + k + "',";
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into AltinElma(" + anahtar.Trim(',') + ") values(" + deger.Trim(',') + ")", con);
            cmd.ExecuteNonQuery();
            con.Close();

            dgview();
        }
        private void dgview()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from AltinElma", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            al.Add(textBox2.Text);
            al.Add(textBox3.Text);
            al.Add(textBox4.Text);

            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into AltinElma(ad,soyad,telefon) values('"+al[0] +"','"+al[1]+"',"+al[2]+")", con);
            cmd.ExecuteNonQuery();
            con.Close();
            dgview();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select " + textBox1.Text + " from AltinElma", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from ogrenci where ad='" + textBox2.Text+"'",con);
            cmd.ExecuteNonQuery();
            con.Close();
            datagridd();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Insert Into ogrenci(ad,soyad,no) values('" + textBox2.Text + "','" + textBox3.Text + "'," + textBox4.Text + ")",con);
            SqlCommand cmd2 = new SqlCommand("Insert Into ders(ders_no,ders_adi) values(" + textBox4.Text + ",'" + textBox5.Text + "')", con);
            SqlCommand cmd3 = new SqlCommand("Insert Into sinav(ders_no,vize1,vize2,final) values(" + textBox4.Text + "," + textBox6.Text + "," + textBox7.Text + "," + textBox8.Text + ")", con);
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            con.Close();
            datagridd();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update sinav set vize2=" + textBox7.Text + " where ders_no=" + textBox4.Text, con);
            cmd.ExecuteNonQuery();
            con.Close();
            datagridd();
        }
    }
}
