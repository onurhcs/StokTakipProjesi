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
    public partial class ÜrünListeleme : Form
    {
        public ÜrünListeleme()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=HACıOSMANOĞLU;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();
        private void ÜrünListeleme_Load(object sender, EventArgs e)
        {
            ÜrünListele();
        }

        private void ÜrünListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from urun",baglanti);
            adtr.Fill(daset,"urun");
            dataGridView1.DataSource = daset.Tables["urun"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BarkodNoTxt.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            KategoriTxt.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            MarkaTxt.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            UrunAdiTxt.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            MiktarTxt.Text = dataGridView1.CurrentRow.Cells["miktar"].Value.ToString();
            AlisFiyatiTxt.Text = dataGridView1.CurrentRow.Cells["alisfiyati"].Value.ToString();
            SatisFiyatiTxt.Text = dataGridView1.CurrentRow.Cells["satisfiyati"].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set urunadi=@urunadi,miktar=@miktar,alisfiyat=@alisfiyat,satisfiyati=@satisfiyati where barkodno=@barkodno ", baglanti);
            komut.Parameters.AddWithValue("@barkodno", BarkodNoTxt.Text);
            komut.Parameters.AddWithValue("@urunadi", UrunAdiTxt.Text);
            komut.Parameters.AddWithValue("@miktar", MiktarTxt.Text);
            komut.Parameters.AddWithValue("@alisfiyati", AlisFiyatiTxt.Text);
            komut.Parameters.AddWithValue("@satisfiyati", SatisFiyatiTxt.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme yapıldı");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void BtnMarkaGuncelle_Click(object sender, EventArgs e)
        {
            if (BarkodNoTxt.Text!="")
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update urun set kategori=@kategori,marka=@marka where barkodno=@barkodno ", baglanti);
                komut.Parameters.AddWithValue("@barkodno", BarkodNoTxt.Text);
                komut.Parameters.AddWithValue("@kategori", cmbKategori.Text);
                komut.Parameters.AddWithValue("@marka", CmbMarka.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme yapıldı");
            }
            
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
    }
}
