using System;
using System.Collections;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApp51
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=Yeni;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        Hashtable deneme = new Hashtable();
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            int n = int.Parse(Interaction.InputBox("Kaç defa veri girileceğini yazınız!!", ""));

            do
            {
                string key = Interaction.InputBox("Key değerini giriniz(int):");
                string val = Interaction.InputBox("Value değerini giriniz:");

                deneme[key] = val;
            }
            while (n-- > 1);
            yenile();

            foreach (var i in deneme.Keys)
                listBox1.Items.Add("id = " + i);
            foreach (var i in deneme.Values)
                listBox2.Items.Add("ad = " + i);
        }
 
        void kaydet(string key, string val)
        {
            con.Open();
            cmd = new SqlCommand("Insert Into tablo(okul_no,ad_soyad)  values(" + key + ",'" + val + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            yenile();
        }
        private void yenile()
        {
            con.Open();
            cmd = new SqlCommand("Select * from tablo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DictionaryEntry i in deneme)
                kaydet(i.Key.ToString(), i.Value.ToString());
            yenile();
        }


    }
}
