using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp50
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=Bora;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ogrenci", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                listBox1.Items.Add(dr[0]);
                listBox2.Items.Add(dr[1]);
                listBox3.Items.Add(dr[2]);
                listBox4.Items.Add(dr[3]);

                comboBox1.Items.Add(dr[0]);
                comboBox2.Items.Add(dr[1]);
                comboBox3.Items.Add(dr[2]);
                comboBox4.Items.Add(dr[3]);

                richTextBox1.Text += dr.GetValue(0).ToString() + "\n";
                richTextBox2.Text += dr.GetValue(1).ToString() + "\n";
                richTextBox3.Text += dr.GetValue(2).ToString() + "\n";
                richTextBox4.Text += dr.GetValue(3).ToString() + "\n";
            }
            con.Close();
        }
    }
}
