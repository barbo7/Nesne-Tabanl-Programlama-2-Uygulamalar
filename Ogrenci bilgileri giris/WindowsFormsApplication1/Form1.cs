using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using BASARIHESAP;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        hesaplama hesap = new hesaplama();
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-SRT1JQQ;Initial Catalog=Ogrenci;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        string parola;
        int n;

        private void Form1_Load(object sender, EventArgs e)
        {
             Random rd =  new Random();
            for(int i=0;i<11;i++)
                parola += rd.Next(0,10).ToString();
            MessageBox.Show(parola);
            Registry.CurrentUser.CreateSubKey("Sifre").SetValue("Sifre", parola);
            
            n = int.Parse(Interaction.InputBox("Kaç öğrenci gireceksiniz", "Ogrenci sayısı"));
            
        }
        void yenile()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select ad,soyad,okulno,tc,dersAdi,VizeNotu,FinalNotu,BasariNotu from ogrencibil", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            yenile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ad, soyad, okulno, tc, dersadi, vize, final;
            for (int i = 0; i < n; i++)
            {
                string dogrulama = Interaction.InputBox("Şifrenizi giriniz", "Şifre");
                if (dogrulama == parola)
                {
                    ad = Interaction.InputBox("Öğrenci adı giriniz: ", "ad");
                    soyad = Interaction.InputBox("Öğrenci soyadı giriniz: ", "soyad");
                    okulno = Interaction.InputBox("Öğrenci no giriniz: ", "Okul No");
                    tc = Interaction.InputBox("TC no giriniz: ", "TC");
                    dersadi = Interaction.InputBox("Ders adı giriniz: ", "Ders adi");
                    vize = Interaction.InputBox("Vize notu giriniz: ", "Vize");
                    final = Interaction.InputBox("Final notu giriniz: ", "Final");

                    string bn = hesap.basarinotu(int.Parse(vize), int.Parse(final)).ToString().Replace(',', '.');
                    string sorgu = String.Format("Insert into ogrencibil(ad,soyad,okulno,tc,dersAdi,VizeNotu,FinalNotu,BasariNotu,Sifre)" +
                        "values('{0}','{1}',{2},'{3}','{4}',{5},{6},{7},'{8}')", ad, soyad, okulno, tc, dersadi, vize, final, bn, parola);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sorgu, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" || textBox3.Text != "")
            {
                con.Open();
                SqlCommand cmdd = new SqlCommand("delete from ogrencibil where tc=" + textBox4.Text + " or okulno=" + textBox3.Text, con);
                cmdd.ExecuteNonQuery();
                con.Close();
            }
            else MessageBox.Show("Lütfen Tc veyahut okul no giriniz.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                con.Open();
                SqlCommand cmddd =  new SqlCommand("update ogrencibil set")
            }

            else
                MessageBox.Show("TC giriniz.");
        }
    }
}
