using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stok_Takip
{
    public partial class FrmKullaniciGiris : Form
    {
        public FrmKullaniciGiris()
        {
            InitializeComponent();
        }

        private void FrmKullaniciGiris_Load(object sender, EventArgs e)
        {

        }

        private void TxtKullaniciGiris_Click(object sender, EventArgs e)
        {
            TxtKullaniciGiris.Text = "";
        }

        private void TxtKullaniciAdi_Click(object sender, EventArgs e)
        {
            TxtKullaniciAdi.Text = "";
        }


        private void girisButton_Click(object sender, EventArgs e)
        {

            if (TxtKullaniciAdi.Text == "" || TxtKullaniciGiris.Text == "")
            {
                MessageBox.Show("Kullanıcı adı veya şifre boş geçilemez..", "Uyarı");
            }
            else
            {
                if (TxtKullaniciAdi.Text == "OnurHcs" && TxtKullaniciGiris.Text == "5353")
                {
                    Form1 frm = new Form1();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı Veya Şifre Yanlış.");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtKullaniciAdi_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtKullaniciGiris_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
