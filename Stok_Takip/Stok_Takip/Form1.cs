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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void FormGetir(Form frm)
        {
            panel1.Controls.Clear();
            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panel1.Controls.Add(frm);
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmMüşteriEkle frm = new FrmMüşteriEkle();
            FormGetir(frm);
        }

        private void BtnMusteriListele_Click(object sender, EventArgs e)
        {
            FrmMüşteriListele listele = new FrmMüşteriListele();
            FormGetir(listele);
        }

        private void BtnUrunEkle_Click(object sender, EventArgs e)
        {
            FrmÜrünEkle ekle = new FrmÜrünEkle();
            FormGetir(ekle);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FrmSatis satis = new FrmSatis();
            FormGetir(satis);
        }

        private void BtnUrunEkle_Click_1(object sender, EventArgs e)
        {
            FrmÜrünEkle urunekle = new FrmÜrünEkle();
            FormGetir(urunekle);
        }

        private void BtnUrunListele_Click(object sender, EventArgs e)
        {
            ÜrünListeleme lis = new ÜrünListeleme();
            FormGetir(lis);
        }

        private void BtnSatisListele_Click(object sender, EventArgs e)
        {
            FrmSatisListeleme listele = new FrmSatisListeleme();
            FormGetir(listele);
        }

        private void BtnKategori_Click(object sender, EventArgs e)
        {
            FrmKatogori kat = new FrmKatogori();
            FormGetir(kat);
        }

        private void BtnMarka_Click(object sender, EventArgs e)
        {
            FrmMarka mar = new FrmMarka();
            FormGetir(mar);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Depo stk = new Depo();
            FormGetir(stk);
        }
    }
}
