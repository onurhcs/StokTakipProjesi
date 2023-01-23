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
    public partial class FrmSatis : Form
    {
        public FrmSatis()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=HACıOSMANOĞLU;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();

        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from Sepet", baglanti);
            adtr.Fill(daset,"Sepet");
            dataGridView1.DataSource = daset.Tables["Sepet"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;

            baglanti.Close();

        }
        private void FrmSatis_Load(object sender, EventArgs e)
        {
            sepetlistele();
        }

        bool durum;
        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from sepet" , baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (TxtBarkodNo.Text==read["barkodno"].ToString())
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            barkodkontrol();
            if (durum==true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into sepet(tc,adsoyad,telefon,barkodno,urunadi,miktar,satisfiyati,toplamfiyati,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktar,@satisfiyati,@toplamfiyati,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@tc", TxtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", TxtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", TxtTelefon.Text);
                komut.Parameters.AddWithValue("@barkodno", TxtBarkodNo.Text);
                komut.Parameters.AddWithValue("@urunadi", TxtUrunAdi.Text);
                komut.Parameters.AddWithValue("@miktar", int.Parse(TxtMiktar.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(TxtSatisFiyatı.Text));
                komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(TxtToplamFiyati.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update sepet set miktar=miktar+'"+int.Parse(TxtMiktar.Text)+ "'  where barkodno='" + TxtBarkodNo.Text + "'", baglanti);
                komut2.ExecuteNonQuery();
                SqlCommand komut3 = new SqlCommand("update sepet set toplamfiyati=miktar*satisfiyati where barkodno='"+TxtBarkodNo.Text+"'", baglanti);
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }
            
            TxtMiktar.Text = "1";
            daset.Tables["Sepet"].Clear();
            sepetlistele();
            hesapla();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    if (item != TxtMiktar)
                    {
                        item.Text = "";
                    }
                }

            }

        }

        private void TxtTc_TextChanged(object sender, EventArgs e)
        {
            if (TxtTc.Text=="")
            {
                TxtAdSoyad.Text = "";
                TxtTelefon.Text = "";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from müşteri where tc  like '" + TxtTc.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                TxtAdSoyad.Text = read["adsoyad"].ToString();
                TxtTelefon.Text = read["telefon"].ToString();
            }
            baglanti.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void hesapla()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyati) from sepet", baglanti);
                label9.Text = komut.ExecuteScalar() + "TL";
                baglanti.Close();


            }
            catch (Exception)
            {

                ;
            }
        }

        private void TxtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            Temizle();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun where barkodno  like '" + TxtBarkodNo.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                TxtUrunAdi.Text = read["urunadi"].ToString();
                TxtSatisFiyatı.Text = read["satisfiyati"].ToString();
            }
            baglanti.Close();
        }

        private void Temizle()
        {
            if (TxtBarkodNo.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != TxtMiktar)
                        {
                            item.Text = "";
                        }
                    }

                }
            }
        }

        private void TxtMiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TxtToplamFiyati.Text =(double.Parse(TxtMiktar.Text) * double.Parse(TxtSatisFiyatı.Text)).ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void TxtSatisFiyatı_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TxtToplamFiyati.Text = (double.Parse(TxtMiktar.Text) * double.Parse(TxtSatisFiyatı.Text)).ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Sepet where barkodno='"+dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString()+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Sepetten Silinmiştir!!!");
            daset.Tables["Sepet"].Clear();
            sepetlistele();
            hesapla();

        }

        private void BtnSatisİptal_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Sepet ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürünler Sepetten Çıkarılmıştır !");
            daset.Tables["Sepet"].Clear();
            sepetlistele();
            hesapla();
        }

        private void BtnSatisYap_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into satis(tc,adsoyad,telefon,barkodno,urunadi,miktar,satisfiyati,toplamfiyati,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktar,@satisfiyati,@toplamfiyati,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@tc", TxtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", TxtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", TxtTelefon.Text);
                komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                komut.Parameters.AddWithValue("@urunadi", dataGridView1.Rows[i].Cells["urunadi"].Value.ToString());
                komut.Parameters.AddWithValue("@miktar", int.Parse(dataGridView1.Rows[i].Cells["miktar"].Value.ToString()));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(dataGridView1.Rows[i].Cells["satisfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(dataGridView1.Rows[i].Cells["toplamfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                SqlCommand komut2 = new SqlCommand("update urun set miktar=miktar-'" + int.Parse(dataGridView1.Rows[i].Cells["miktar"].Value.ToString()) + "' where barkodno='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "'", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();               
            }
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("delete from Sepet ", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["Sepet"].Clear();
            sepetlistele();
            hesapla();
        }
    }
}
