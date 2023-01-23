using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stok_Takip
{
    public partial class FrmÜrünEkle : Form
    {
        public FrmÜrünEkle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=HACıOSMANOĞLU;Initial Catalog=Stok_Takip;Integrated Security=True");
        bool durum;
        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (TxtBarkodNo.Text==read["barkodno"].ToString()||TxtBarkodNo.Text=="")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                CmbKategori.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }
        private void FrmÜrünEkle_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }

        private void CmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbMarka.Items.Clear();
            CmbMarka.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgileri where kategori='"+CmbKategori.SelectedItem+ "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                CmbMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            barkodkontrol();
            if (durum==true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into urun(barkodno,kategori,marka,urunadi,miktar,alisfiyati,satisfiyati,tarih) values(@barkodno,@kategori,@marka,@urunadi,@miktar,@alisfiyati,@satisfiyati,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@barkodno", TxtBarkodNo.Text);
                komut.Parameters.AddWithValue("@kategori", CmbKategori.Text);
                komut.Parameters.AddWithValue("@marka", CmbMarka.Text);
                komut.Parameters.AddWithValue("@urunadi", TxtUrunAdi.Text);
                komut.Parameters.AddWithValue("@miktar", int.Parse(TxtMiktar.Text));
                komut.Parameters.AddWithValue("@alisfiyati", double.Parse(TxtAlisFiyati.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(TxtSatisFiyati.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Ürün Eklendi.");
            }
            else
            {
                MessageBox.Show("Böyle Bir Barkod No Var","Uyarı");
            }
            
            CmbMarka.Items.Clear();
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set miktari=miktari+'"+int.Parse(MiktarTxt.Text)+"' where barkodno='"+BarkodNoTxt.Text+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Var Olan Ürüne Ekleme Yapıldı");
        }

        private void BarkodNoTxt_TextChanged(object sender, EventArgs e)
        {
            if (BarkodNoTxt.Text == "")
            {
                LblMiktar.Text = "";
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun where barkod like '" + BarkodNoTxt.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                KategoriTxt.Text = read["kategori"].ToString();
                MarkaTxt.Text = read["marka"].ToString();
                UrunAdiTxt.Text = read["urunadi"].ToString();
                LblMiktar.Text = read["miktar"].ToString();
                AlisFiyatiTxt.Text = read["alisfiyati"].ToString();
                SatisFiyatiTxt.Text = read["satisfiyati"].ToString();

            }
            baglanti.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
