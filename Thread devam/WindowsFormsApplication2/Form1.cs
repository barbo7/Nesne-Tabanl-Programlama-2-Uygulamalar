using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Thread islem1;
        Thread islem2;
        Thread islem3;
        Thread islem4;
        Thread islem5;

        public Form1()
        {
            InitializeComponent();
        }
        int m1 = 1, m2_1 = 2021, m2_2 = 707001;
        private void Form1_Load(object sender, EventArgs e)
        {
            Form1.CheckForIllegalCrossThreadCalls = false;

            islem1 = new Thread(new ThreadStart(metot1));
            islem2 = new Thread(new ThreadStart(metot2));
            islem3 = new Thread(new ThreadStart(metot3));
            islem4 = new Thread(new ThreadStart(metot4));
            islem5 = new Thread(new ThreadStart(metot5));

            islem1.Start();
            islem2.Start();
            islem3.Start();
            islem4.Start();
            islem5.Start();
        }
        void metot1()
        {
            while(true)
            {
                listBox1.Items.Add(m1++);
                Thread.Sleep(1000);
            }
        }
        void metot2()
        {
            while (true)
            {
                listBox2.Items.Add(m2_2++);
                Thread.Sleep(1000);
                if (m2_2 + m2_1 >= 999999)
                {
                    islem1.Abort();
                    islem2.Abort();
                    islem3.Abort();
                    islem4.Abort();
                    islem5.Abort();
                }
                    
            }
        }
        void metot3()
        {
            List<string> program = new List<string>{"a","b","c","d","e","f","g","h","l","m","n"};
            string[,] programkopya = new String[10, 2] { { "a", "" }, };
            int i=9;
            Random rnd = new Random();

            if (i < 0) islem3.Abort();

            while(true)
            {
                int rast = rnd.Next(i);
                listBox3.Items.Add(program[rast]);
                Thread.Sleep(1000);
                program.RemoveAt(rast);
                i--;
            }
        }
        void metot4()
        {
            //metot3te devam et.

        }
        void metot5()
        {
            List<int> seksenbir = new List<int>();
            for (int i = 2; i < 81; i++)
                seksenbir.Add(i);
            Random rnd = new Random();
            while(true)
            {
                int rast = rnd.Next(seksenbir.Count);
                listBox5.Items.Add(rast);
                seksenbir.Remove(rast);
                Thread.Sleep(1000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            islem1.Abort();
            islem2.Abort();
            islem3.Abort();
            islem4.Abort();
            islem5.Abort();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString();
        }
    }
}
