using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp46
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=aaa;Integrated Security=True"); //Database'in bulunduğu yer
        BindingSource bs = new BindingSource(); //Daha hızlı veri akışı için kullandığımız tool

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM kimlik", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;

            dataGridView1.DataSource = bs;
            /*textBox1.DataBindings.Add("Text", bindingSource1,"okulno");
            textBox2.DataBindings.Add("Text", bindingSource1, "Ad");
            textBox3.DataBindings.Add("Text", bindingSource1, "Soyad");
            textBox4.DataBindings.Add("Text", bindingSource1, "Tc");
            textBox5.DataBindings.Add("Text", bindingSource1, "Adres");*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bs.AddNew(); // By işlem sadece form üzeirndeki nesnelerde yeni kayıt alanı açmak için kullanılır. 
            textBox1.Focus();                        //Asla veri tabanına müdale değildir.
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bindingSource1.EndEdit(); //Yeni açılan nesnelere girilen kayıtların form üzerindeki datagrid nesnesine akarmk için kullanılır.
            bindingSource1.AddNew(); //Bu şekilde kullanım bizi yanıltabilir sebebi ise ikinci bir komut kullanmamızı bekler.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bs.MoveFirst(); //İlk kayıt'a gitmek için kullanılır.
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bs.MovePrevious(); //Önceki kayıt'a gitmek için kullanılır.
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bs.MoveLast(); //Son kayıt'a gitmek için kullanılır.
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bs.MoveNext(); //Sonraki kayıt'a gitmek için kullanılır.
        }

        private void button7_Click(object sender, EventArgs e)
        {   
            con.Open();
            SqlCommand kaydet = new SqlCommand("Insert into kimlik(Ad,Soyad,Tc,Adres,Sınıf,Şube,TelNo) values('" + textBox2.Text + "','" + textBox3.Text + "'," + textBox4.Text + ",'" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7 + "'," + textBox8.Text + ")",con);
            kaydet.ExecuteNonQuery();
            con.Close();

        }
        // bu işlemlerin hiçbirisi veritabanı işlemi değildir. Bunların hepsi sadece sanal kayıt'ta gösterilen olaylardır.
        //Girilen kayıtların veritabanına işlenebilmesi için mutlaka insert komutu kullanılmalıdır.
    }
}
