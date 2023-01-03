using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;
using ClassLibrary1;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection bgl = new SqlConnection("Data Source=DESKTOP-SRT1JQQ;Initial Catalog=OGRENCI;Integrated Security=True");
        Hesapla hesap = new Hesapla();

        string sifre;
        byte sayac = 1, yanlis = 0;
        string ogrsayi;

        private void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBLOGRENCI", bgl);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Temizle()
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtBasariNotu.Clear();
            txtDersAdi.Clear();
            txtFinal.Clear();
            txtOkulNo.Clear();
            txtTC.Clear();
            txtVize.Clear();
            txtAd.Select();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            byte[] sayi = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 1; i <= 11; i++)
            {
                sifre += sayi[rnd.Next(0, 9)].ToString();
            }
            bgl.Open();

            SqlCommand sifreEkle = new SqlCommand("insert into TBLSIFRE (SIFRE) values ('" + sifre + "')", bgl);
            sifreEkle.ExecuteNonQuery();
            Registry.CurrentUser.CreateSubKey("Parola").SetValue("Parola", sifre);

            bgl.Close();
            MessageBox.Show("Şifreniz : " + sifre);
            try 
	        {
                ogrsayi = Microsoft.VisualBasic.Interaction.InputBox("Kaç öğrenci gireceksiniz?", "Girilecek Öğrenci Sayısı", "");
                int result;
                if (int.TryParse(ogrsayi, out result))
                {
                    if (Convert.ToByte(ogrsayi) > 0 & Convert.ToByte(ogrsayi) < 250)
                        txtAd.Enabled = true;
                    else
                    {
                        MessageBox.Show("Hatalı Giriş.");
                        Application.Restart();
                    } 
                }
                else
                {
                    MessageBox.Show("Girdiğiniz Karakter Sayısal Değil !!!");
                    Application.Restart();
                }
	        }
	        catch (Exception)
	        {
    
	        }
            Listele();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Microsoft.VisualBasic.Interaction.InputBox("Şifre giriniz", "Şifre", "") == Registry.CurrentUser.OpenSubKey("Parola").GetValue("Parola").ToString())
            {
                if (sayac <= Convert.ToByte(ogrsayi))
                {
                    if ((txtAd.Text.Length <= 30 & txtAd.Text.Length > 0) & (txtSoyad.Text.Length <= 30 & txtSoyad.Text.Length > 0) & (txtOkulNo.Text.Length <= 10 & txtOkulNo.Text.Length > 0) & (txtTC.Text.Length <= 11 & txtTC.Text.Length > 0) & (txtDersAdi.Text.Length <= 30 & txtDersAdi.Text.Length > 0) & (Convert.ToByte(txtVize.Text) <= 100 & Convert.ToByte(txtVize.Text) >= 0) & (Convert.ToByte(txtFinal.Text) <= 100 & Convert.ToByte(txtFinal.Text) >= 0))
                    {
                        bgl.Open();
                        SqlCommand ekle = new SqlCommand("insert into TBLOGRENCI (AD, SOYAD, OKULNO, TC, DERSAD, VIZE, FINAL, BASARINOTU) values ('" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtOkulNo.Text + "','" + txtTC.Text + "','" + txtDersAdi.Text + "','" + txtVize.Text + "','" + txtFinal.Text + "','" + hesap.BasariHesapla(Convert.ToByte(txtVize.Text), Convert.ToByte(txtFinal.Text)) + "')", bgl);
                        txtBasariNotu.Text = hesap.BasariHesapla(Convert.ToByte(txtVize.Text), Convert.ToByte(txtFinal.Text)).ToString();
                        ekle.ExecuteNonQuery();
                        sayac++;
                        bgl.Close();
                        Listele();
                        Temizle(); 
                    }
                    else
                        MessageBox.Show("Ad, soyad ve ders adı 30 karakter olmalı, okul no 10, TC 11 vize ve final 0 ve 100 arasında olmalı");
                }
                else
                {
                    MessageBox.Show("Daha fazla öğrenci girişi yapamazsınız.");
                    Temizle();
                }
            }
            else
            {
                yanlis++;
                MessageBox.Show("Şifre Yanlış");
                if (yanlis == 3)
                {
                    bgl.Open();
                    SqlCommand sil = new SqlCommand("delete from TBLOGRENCI", bgl);
                    sil.ExecuteNonQuery();
                    bgl.Close();
                    MessageBox.Show("Tüm Veriler Silindi");
                    Listele();
                    Temizle();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtOkulNo.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtTC.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtDersAdi.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtVize.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtFinal.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtBasariNotu.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Microsoft.VisualBasic.Interaction.InputBox("Şifre giriniz", "Şifre", "") == Registry.CurrentUser.OpenSubKey("Parola").GetValue("Parola").ToString())
            {
                if (txtAd.Text.Length <= 30 & txtAd.Text.Length > 0 & txtSoyad.Text.Length <= 30 & txtSoyad.Text.Length > 0 & txtOkulNo.Text.Length <= 10 & txtOkulNo.Text.Length > 0 & txtTC.Text.Length <= 11 & txtTC.Text.Length > 0 & txtDersAdi.Text.Length <= 30 & txtDersAdi.Text.Length > 0 & Convert.ToByte(txtVize.Text) <= 100 & Convert.ToByte(txtVize.Text) >= 0 & Convert.ToByte(txtFinal.Text) <= 100 & Convert.ToByte(txtFinal.Text) >= 0)
                {
                    bgl.Open();
                    SqlCommand guncelle = new SqlCommand("update TBLOGRENCI set AD= '" + txtAd.Text + "' , SOYAD = '" + txtSoyad.Text + "', OKULNO = '" + txtOkulNo.Text + "', TC= '" + txtTC.Text + "' , DERSAD = '" + txtDersAdi.Text + "', VIZE = '" + txtVize.Text + "', FINAL = '" + txtFinal.Text + "', BASARINOTU = '" + hesap.BasariHesapla(Convert.ToByte(txtVize.Text), Convert.ToByte(txtFinal.Text)) + "' where TC = '" + txtTC.Text + "' ", bgl);
                    guncelle.ExecuteNonQuery();
                    MessageBox.Show("İşlem tamamlandı");
                    Temizle();
                    Listele();
                    bgl.Close();
                }
                else
                    MessageBox.Show("Ad, soyad ve ders adı 30 karakter olmalı, okul no 10, TC 11 vize ve final 0 ve 100 arasında olmalı");
            }
            else
            {
                yanlis++;
                MessageBox.Show("Şifre Yanlış");
                if (yanlis == 3)
                {
                    bgl.Open();
                    SqlCommand sil = new SqlCommand("delete from TBLOGRENCI", bgl);
                    sil.ExecuteNonQuery();
                    bgl.Close();
                    MessageBox.Show("Tüm Veriler Silindi");
                    Listele();
                    Temizle();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Microsoft.VisualBasic.Interaction.InputBox("Şifre giriniz", "Şifre", "") == Registry.CurrentUser.OpenSubKey("Parola").GetValue("Parola").ToString())
            {
                bgl.Open();
                SqlCommand sil = new SqlCommand("delete from TBLOGRENCI where TC = '" + txtTC.Text + "' ", bgl);
                sil.ExecuteNonQuery();
                MessageBox.Show("İşlem tamamlandı");
                Temizle();
                Listele();
                bgl.Close();
            }
            else
            {
                yanlis++;
                MessageBox.Show("Şifre Yanlış");
                if (yanlis == 3)
                {
                    bgl.Open();
                    SqlCommand sil = new SqlCommand("delete from TBLOGRENCI", bgl);
                    sil.ExecuteNonQuery();
                    bgl.Close();
                    MessageBox.Show("Tüm Veriler Silindi");
                    Listele();
                    Temizle();
                }
            }
        }
    }
}
