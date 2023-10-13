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
using System.Windows.Forms.VisualStyles;
using System.ComponentModel.Design;

namespace BankaTest
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public int sayi;
        public int  RastgeleSayiUret()
        {
            Random rast = new Random();
            return rast.Next(100000, 1000000);
             
        }

        bool durum;
        //Eğer dr.Read() çağrısı bir kayıt okursa, durum değişkenine false atanır, aksi takdirde true atanır.
        //Bu, "HESAPNO" sütunu belirtilen değeri içeren bir kayıt bulunduğunda false, aksi takdirde true olur.
        
        void kontrol()
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select * from TBLKISILER Where HESAPNO=@P1",baglanti);
            komut1.Parameters.AddWithValue("@P1",mskHesapNo.Text);
            SqlDataReader dr = komut1.ExecuteReader();
            if (dr.Read())
            {
                durum = false;
            }
            else
            {
                durum = true;
            }
            baglanti.Close();
        }
        

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-BJO2DGU\SQLEXPRESS;Initial Catalog=DbBankaTest;Integrated Security=True");
      

        private void btnHesapNo_Click(object sender, EventArgs e)
        {
            sayi = RastgeleSayiUret();
            mskHesapNo.Text = sayi.ToString();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            kontrol();

            if (durum == true)

            {

                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLKISILER (AD,SOYAD,TC,TELEFON,HESAPNO,SIFRE) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
                komut.Parameters.AddWithValue("@p1", txAd.Text);
                komut.Parameters.AddWithValue("@p2", txSoyad.Text);
                komut.Parameters.AddWithValue("@p3", mskTc.Text);
                komut.Parameters.AddWithValue("@p4", mskTelefon.Text);
                komut.Parameters.AddWithValue("@p5", mskHesapNo.Text);
                komut.Parameters.AddWithValue("@p6", txSifre.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Müşteri Bilgileri Sisteme Kaydedildi");
            }
            else
            {
                MessageBox.Show("Yeniden Hesap No Alınız Lütfen.");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
