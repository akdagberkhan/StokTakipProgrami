using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace StokTakip
{
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");

        //Stok Ekranı Penceresini MenuStrip İle Aç
        private void stokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stokekranı stkekran = new stokekranı();
            stkekran.Show();
        }

        //Cari Ekranını MenuStrip İle Açma
        private void cariToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cariekranı cekran = new cariekranı();
            cekran.Show();
        }
        //Programı Yaban Kişi Hakkında Bilgi
        private void kişiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu Program Berkhan Akdağ Tarafından Proje Ödevi Olarak Hazırlanmıştır","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            
        }

        //Admin Ekle Penceresini MenuStrip İle Aç
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adminekle admek = new adminekle();
            admek.Show();
        }

        //Satış Ekranını MenuStrip İle Aç
        private void satışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            satisekranı stsekran = new satisekranı();
            stsekran.Show();
        }

        //Hareket Ekranını MenuStrip İle Aç
        private void hareketlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hareketlerekranı hekran = new hareketlerekranı();
            hekran.Show();
        }
        private void adminSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adminsil admsil = new adminsil();
            admsil.Show();
        }
        int kapaniss = 0;
        private void anasayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            kapaniss += 1;
            if (kapaniss==1)
            {
                admingiris admg = new admingiris();
                admg.Show();
                this.Close();
            }
            
        }

        private void kullanımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kullanim kl = new kullanim();
            kl.Show();
        }

        private void raporlamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            raporlama rp = new raporlama();
            rp.Show();
        }


        //Uygulamanın Sürümünün VeriTabanından Değiştirildiğinde Anasayfada Yazdırma İşlemi
        private void anasayfa_Load(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToLongDateString();
            label7.Text = DateTime.Now.ToLongTimeString();
            label6.Text = admingiris.kullanci;
            baglan.Open();
            DataTable tb = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from SURUM",baglan);
            adp.Fill(tb);
            baglan.Close();
            string surum = tb.Rows.Count.ToString();
            label3.Text = "Sürüm : " + tb.Rows[Convert.ToInt16(surum)-1][0].ToString();
            
        }

        //Programı Kapatma
        int kapaniss2 = 0;
        private void çıkışToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            kapaniss2 += 1;
            if (kapaniss2 == 1)
            {
                admingiris admg = new admingiris();
                admg.Show();
                this.Close();
            }
        }

        private void yetkiliGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adminguncelle admguncelle = new adminguncelle();
            admguncelle.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToLongDateString();
            label7.Text = DateTime.Now.ToLongTimeString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "/calc1.exe");
            }
            catch (Exception)
            {


            }
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            stokekranı stkekran = new stokekranı();
            stkekran.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            cariekranı cekran = new cariekranı();
            cekran.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            hareketlerekranı hekran = new hareketlerekranı();
            hekran.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            satisekranı stsekran = new satisekranı();
            stsekran.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            raporlama rp = new raporlama();
            rp.Show();
        }
        int kapaniss3 = 0;
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            kapaniss3 += 1;
            if (kapaniss3 == 1)
            {
                admingiris admg = new admingiris();
                admg.Show();
                this.Close();
            }
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            label9.ForeColor = Color.White;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            label9.ForeColor = Color.Black;
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            label10.ForeColor = Color.White;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            label10.ForeColor = Color.Black;
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            label11.ForeColor = Color.White;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            label11.ForeColor = Color.Black;
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            label12.ForeColor = Color.White;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            label12.ForeColor = Color.Black;
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            label13.ForeColor = Color.White;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            label13.ForeColor = Color.Black;
        }
      
    }
}
