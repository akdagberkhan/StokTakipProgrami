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
    public partial class sifremiunuttum : Form
    {
        public sifremiunuttum()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");
        private void sifremiunuttum_FormClosing(object sender, FormClosingEventArgs e)
        {
            sifremiunuttum sf = new sifremiunuttum();
            sf.Close();
        }
        string kulad, sifre, ad, soyad;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtad.Text != "" && txtkulad.Text != "" && txtsifre.Text != "" && txtsifretekrar.Text != "" && txtsoyad.Text != "")
                {
                    if (txtsifre.Text == txtsifretekrar.Text)
                    {
                        kulad = txtkulad.Text;
                        ad = txtad.Text;
                        soyad = txtsoyad.Text;
                        sifre = txtsifre.Text;
                        baglan.Open();
                        OleDbCommand guncelle = new OleDbCommand("update ADMIN set sifre='" + sifre + "' where kullanici_adi='" + kulad + "' and ad='" + ad + "' and soyad='" + soyad + "'", baglan);
                        guncelle.ExecuteNonQuery();
                        baglan.Close();
                        kulhatirla();
                        MessageBox.Show("İşlem Başarılı");
                    }

                }
                else
                {
                    MessageBox.Show("Boş Alan Bırakmayınız");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
            
        }
        public void kulhatirla()
        {
            try
            {
                baglan.Open();
                OleDbCommand guncelle = new OleDbCommand("update KULLANICI_HATIRLA set sifre='" + sifre + "' where kul_adi='" + kulad + "'", baglan);
                guncelle.ExecuteNonQuery();
                baglan.Close();
                Application.Restart();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void txtsifretekrar_TextChanged(object sender, EventArgs e)
        {
            if (txtsifre.Text==txtsifretekrar.Text)
            {
                label6.ForeColor = Color.Green;
                label6.Text = "Kullanılabilir";
            }
            else
            {
                label6.ForeColor = Color.Red;
                label6.Text = "Şifreler"+"\n"+"Uyuşmuyor";
            }
            if (txtsifretekrar.Text==""||txtsifre.Text=="")
            {
                label6.Text = "";
            }
        }

        private void txtsifre_TextChanged(object sender, EventArgs e)
        {
            if (txtsifre.Text == txtsifretekrar.Text)
            {
                label6.ForeColor = Color.Green;
                label6.Text = "Kullanılabilir";
            }
            else
            {
                label6.ForeColor = Color.Red;
                label6.Text = "Şifreler" + "\n" + "Uyuşmuyor";
            }
            if (txtsifre.Text == "" || txtsifre.Text == "")
            {
                label6.Text = "";
            }
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            txtsifre.PasswordChar = '\0';
            txtsifretekrar.PasswordChar = '\0';
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            txtsifre.PasswordChar = '*';
            txtsifretekrar.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
