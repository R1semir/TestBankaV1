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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-BJO2DGU\SQLEXPRESS;Initial Catalog=DbBankaTest;Integrated Security=True");
        private void linkKayıtol_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLKISILER where HESAPNO=@P1 AND SIFRE=@P2",baglanti);
            komut.Parameters.AddWithValue("@P1",mskHesapNo.Text);
            komut.Parameters.AddWithValue("@P2", txSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form2 fr2 = new Form2();
                fr2.hesap = mskHesapNo.Text;
                fr2.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Bilgi Sistemden Çıkılıyor...");
                Application.Exit();
            }
            baglanti.Close();
        }
    }
}
