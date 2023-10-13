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

namespace BankaTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-BJO2DGU\SQLEXPRESS;Initial Catalog=DbBankaTest;Integrated Security=True");
        public string hesap;
        private void Form2_Load(object sender, EventArgs e)
        {
            lblhesapNo.Text = hesap;
            baglanti.Open();
            SqlCommand komutgetir = new SqlCommand("select * from TBLKISILER where HESAPNO=@P1",baglanti);
            komutgetir.Parameters.AddWithValue("@P1", hesap);
            SqlDataReader dr = komutgetir.ExecuteReader();
            while (dr.Read())
            {
                lblAd.Text = dr[1] + " " + dr[2];
                lblTelefon.Text = dr[4].ToString();
                lbltc.Text = dr[3].ToString();
            }
            baglanti.Close();
             

        }

        private void btnGönder_Click(object sender, EventArgs e)
        {
            // GÖNDERİLEN HESABIN PARA ARTIŞI
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("update TBLHESAP set BAKIYE=BAKIYE+@P1 where HESAPNO=@P2",baglanti);
            komut3.Parameters.AddWithValue("@P1",decimal.Parse(txTutar.Text));
            komut3.Parameters.AddWithValue("@P2",mskHesapNo.Text);
            komut3.ExecuteNonQuery();
            baglanti.Close();
           
           

            // GÖNDEREN HESABIN PARA AZALIŞI
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("update TBLHESAP set BAKIYE=BAKIYE-@K1 where HESAPNO=@K2",baglanti);
            komut4.Parameters.AddWithValue("@K1",decimal.Parse(txTutar.Text));
            komut4.Parameters.AddWithValue("@K2",hesap);
            komut4.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İşlem Gerçekleşti ");
           
        } 
    }
}
