using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Thread islem1;
        Thread islem2;
        Thread islem3;
        //Yapılacak işlem sayısı kadar thread tipinde nesne/değişken tanımlanmalıdır burada 3 işlem miçin 3 farklı değişken tanımlanmıştır.

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1.CheckForIllegalCrossThreadCalls = false;
            //formların thread özelliği true olarak geldiği için kullanıma müsade etmez. Bu izinin alınabilmesi için mutlaka formun checkforillegalcrossthreadcalls özelliğine fals değeri atanmalıdır.
            //Burada dikkat edilmesi gereken en önemli husus thread hangi form üzerinde kullanılıyorsa o form yazılmalıdır ve o formdan izin alınmalıdır.
            islem1 = new Thread(new ThreadStart(metot1));
            islem2 = new Thread(new ThreadStart(metot2));
            islem3 = new Thread(new ThreadStart(metot3));
            /*islem1,2,3 olarak tanımlanmış nesne veya değişkenlere eşitliğin öbür tarafından yeni bir değer thread olarak atanmalıdır.
            Bunun için ThreadStar metot'unun parametresine aşağıda tanımlanacak olan metotlar(Tek parametreli) yazılır.
           Burada metotlar sonradan tanımlanacaksa henüz metotlara atama yapılmadığı için altı çizili hata gibi gösterebilir. Metot tanımlanınca bu hata ortadan kalkar. */
            islem1.Start();
            islem2.Start();
            islem3.Start();
            /* islem1,2,3 üzerine atılan işlemlerden sonra read işleminin başlatılabilmesi için bu nesne/değişken'in start metotu ile çalıştırılması gerekir. Bu işlem formun loadına yazılmıştır
             * ama istenirse buton clicke de yazılabilir. Bundan sonra yapılması gereken yukarıda parametre olarak verilen metotların tanımlanmasıdır.
             */

        }
        int i = 100;
        int j = 100;
        //metotlar içerisinde kullanılacak değerlere sabit değer atanmış olup değerler int olarak tanımlanmıştır.
        void metot1()
        {
            while(true)// değer true olduğu sürece dönsün istenmiş.
            {
                listBox1.Items.Add(i++); //listBoxa i adındaki değer 1er 1er arttırarak yazılmak istenmiş.
                Thread.Sleep(5000);     //Thread nesnesinin sleep metotu kullanılmış, saniyenin 1000'de 1'i olarak görüntülensin istenmiş.
            }
        }
        void metot2()
        {
            while(true)
            {
                listBox2.Items.Add(3 * i++);
                Thread.Sleep(1000);
            }
        }
        void metot3()
        {
            while (true)
                label1.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Butonun click'ine şunları yazınız.
            islem1.Abort();
            islem2.Abort();
            islem3.Abort();
            //işlemi durdurmak için abort kullanılır.
        }
    }
}
