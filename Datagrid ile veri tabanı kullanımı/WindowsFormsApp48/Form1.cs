using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp48
{
    public partial class Form1 : Form
    {   //veritabanı yeri
        SqlConnection con = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=deneme;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveLast();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from personel",con);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            bindingSource1.DataSource = dt1;
            dataGridView1.DataSource = bindingSource1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into personel(id,a) values(" + textBox1.Text + ",'" + textBox2.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into maas(id,b) values(" + textBox1.Text + ",'" + textBox2.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into cv(id,c) values(" + textBox1.Text + ",'" + textBox2.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from personel where id=" + textBox1.Text,con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from maas where id=" + textBox1.Text,con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from cv where id=" + textBox1.Text, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }



        private void button15_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select * from personel", con);
            SqlDataAdapter da2 = new SqlDataAdapter("select * from maas", con);
            SqlDataAdapter da3 = new SqlDataAdapter("select * from cv", con);

            DataTable dt1 = new DataTable("Personel");
            DataTable dt2 = new DataTable("maas");
            DataTable dt3 = new DataTable("cv");

            da1.Fill(dt1);
            da2.Fill(dt2);
            da3.Fill(dt3);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);

            DataRelation dr = new DataRelation("ABC", dt1.Columns["id"], dt2.Columns["id"]);
            ds.Relations.Add(dr);
            DataRelation dr1 = new DataRelation("ABCD", dt2.Columns["id"], dt3.Columns["id"]);
            ds.Relations.Add(dr1);

            dataGrid1.DataSource = ds;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update personel set a='" + textBox2.Text + "' where id=" + textBox1.Text, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update maas set b='" + textBox2.Text + "' where id=" + textBox1.Text, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update cv set c='" + textBox2.Text + "' where id=" + textBox1.Text, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select * from maas", con);
            DataTable dt2 = new DataTable();
            da1.Fill(dt2);
            bindingSource1.DataSource = dt2;
            dataGridView1.DataSource = bindingSource1;

        }

        private void button17_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select * from cv", con);
            DataTable dt3 = new DataTable();
            da1.Fill(dt3);
            bindingSource1.DataSource = dt3;
            dataGridView1.DataSource = bindingSource1;
        }
    }
}
