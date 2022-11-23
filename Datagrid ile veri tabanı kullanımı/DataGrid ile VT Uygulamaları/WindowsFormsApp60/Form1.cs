using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp60
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=deneme;Integrated Security=True");
        BindingSource bs = new BindingSource();
        DataTable dt;
        SqlDataAdapter da;
        SqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            yenileDG();
        }
        private void yenileDG()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select * from cv", con);
            SqlDataAdapter da2 = new SqlDataAdapter("select * from personel", con);
            SqlDataAdapter da3 = new SqlDataAdapter("select * from maas", con);

            DataTable dt1 = new DataTable("cv");
            DataTable dt2 = new DataTable("personel");
            DataTable dt3 = new DataTable("maas");

            da1.Fill(dt1);
            da2.Fill(dt2);
            da3.Fill(dt3);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);

            DataRelation dr = new DataRelation("Personel", dt1.Columns["id"], dt2.Columns["id"]);
            ds.Relations.Add(dr);
            DataRelation dr1 = new DataRelation("Maaş", dt2.Columns["id"], dt3.Columns["id"]);
            ds.Relations.Add(dr1);

            dataGrid1.DataSource = ds;
        }

        private void button5_Click(object sender, EventArgs e)
        {
             da = new SqlDataAdapter("select * from personel", con);
             dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("select * from cv", con);
            dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("select * from maas", con);
            dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
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

        private void button8_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("delete from personel where id ="+textBox1.Text, con);
            SqlCommand cmd1 = new SqlCommand("delete from maas where id =" + textBox1.Text, con);
            SqlCommand cmd2 = new SqlCommand("delete from cv where id =" + textBox1.Text, con);

            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();

            con.Close();
            yenileDG();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("Insert into personel(id,a) values(" + textBox1.Text + ",'" + textBox2.Text + "')", con);
            SqlCommand cmd1 = new SqlCommand("Insert into maas(id,b) values("+textBox1.Text+",'"+textBox3.Text+"')", con);
            SqlCommand cmd2 = new SqlCommand("Insert into cv(id,c) values(" + textBox1.Text + ",'" + textBox4.Text + "')", con);

            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();

            con.Close();
            yenileDG();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("Update personel set a="+textBox2.Text+" where id="+textBox1.Text, con);
            SqlCommand cmd1 = new SqlCommand("Update maas set b=" + textBox3.Text + " where id=" + textBox1.Text, con);
            SqlCommand cmd2 = new SqlCommand("Update cv set c=" + textBox4.Text + " where id=" + textBox1.Text, con);

            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();

            con.Close();
            yenileDG();
        }
    }
}
