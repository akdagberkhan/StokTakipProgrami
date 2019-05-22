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
    public partial class adminguncelle : Form
    {
        public adminguncelle()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");
        DataTable tb = new DataTable();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bilgilerigetir();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i][2].ToString() == txtkuladguncel.Text)
                    {
                        txtad.Text = tb.Rows[i][0].ToString();
                        txtsoyad.Text = tb.Rows[i][1].ToString();
                        txtkulad.Text = tb.Rows[i][2].ToString();
                        txtsifre.Text = tb.Rows[i][3].ToString();
                        txteposta.Text = tb.Rows[i][4].ToString();
                        if (tb.Rows[i][5].ToString() == "erkek")
                        {
                            rbtnerkek.Select();
                        }
                        else
                        {
                            rbtnbayan.Select();
                        }
                        if (tb.Rows[i][6].ToString() == "1")
                        {
                            cboxstokyetki.Checked = true;
                        }
                        else
                        {
                            cboxstokyetki.Checked = false;
                        }
                        if (tb.Rows[i][7].ToString() == "1")
                        {
                            cboxurunyetki.Checked = true;
                        }
                        else
                        {
                            cboxurunyetki.Checked = false;
                        }
                        if (tb.Rows[i][8].ToString() == "1")
                        {
                            cboxcariyetki.Checked = true;
                        }
                        else
                        {
                            cboxcariyetki.Checked = false;
                        }
                        if (tb.Rows[i][9].ToString() == "1")
                        {
                            cboxsatisyetki.Checked = true;
                        }
                        else
                        {
                            cboxsatisyetki.Checked = false;
                        }
                        if (tb.Rows[i][10].ToString() == "1")
                        {
                            cboxadminyetki.Checked = true;
                        }
                        else
                        {
                            cboxadminyetki.Checked = false;
                        }
                        if (tb.Rows[i][11].ToString() == "1")
                        {
                            cboxraporyetki.Checked = true;
                        }
                        else
                        {
                            cboxraporyetki.Checked = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
            
        }
        string gelenkullanici;
        private void adminguncelle_Load(object sender, EventArgs e)
        {
            bilgilerigetir();
            yetkikontrol();
        }
        public void bilgilerigetir()
        {
            baglan.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN", baglan);
            adp.Fill(tb);
            baglan.Close();
        }
        public void yetkikontrol()
        {
            try
            {
                gelenkullanici = admingiris.kullanci;
                baglan.Open();
                DataTable tb = new DataTable();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where kullanici_adi='" + gelenkullanici + "'", baglan);
                adp.Fill(tb);
                baglan.Close();
                if (tb.Rows[0][10].ToString() == "1")
                {
                    button1.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                    MessageBox.Show("Yetkili Kişi Eklemek İçin Yetki Verilmiş Bir Kullanıcı İle Giriş Yapmak Zorundasınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception)
            {


            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            txtsifre.PasswordChar = '*';
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            txtsifre.PasswordChar = '\0';
        }

        string sifre;
        public void kulhatirla()
        {
            sifre = txtsifre.Text;
            baglan.Open();
            OleDbCommand guncelle = new OleDbCommand("update KULLANICI_HATIRLA set sifre='" + sifre + "' where kul_adi='" + txtkuladguncel.Text + "'", baglan);
            guncelle.ExecuteNonQuery();
            baglan.Close();
        }
        string cinsiyet;
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtkuladguncel.Text=="")
            {
                MessageBox.Show("Güncellenecek Kullanıcı Adını Giriniz");
            }
            else
            {
                if (txtad.Text!=""&&txtsoyad.Text!=""&&txtkulad.Text!=""&&txtsifre.Text!=""&&txteposta.Text!="")
                {
                    try
                    {
                        if (rbtnbayan.Checked)
                        {
                            cinsiyet = "bayan";
                        }
                        if (rbtnerkek.Checked)
                        {
                            cinsiyet = "erkek";
                        }
                        yetkiler();
                        baglan.Open();
                        OleDbCommand gcmd = new OleDbCommand("update ADMIN set ad='" + txtad.Text + "',soyad='" + txtsoyad.Text + "',kullanici_adi='" + txtkulad.Text + "',sifre='" + txtsifre.Text + "',eposta='" + txteposta.Text + "',cinsiyet='" + cinsiyet + "',stok_yetki='" + stokyetki + "',indirim_yetki='" + indirimyetki + "',carihesap_yetki='" + cariyetki + "',satis_yetki='" + satisyetki + "',admin_yetki='" + adminyetki + "',rapor_yetki='" + raporyetki + "' where ad='" + txtkuladguncel.Text + "'", baglan);
                        gcmd.ExecuteNonQuery();
                        baglan.Close();
                        kulhatirla();
                        MessageBox.Show("İşlem Başarıyla Tamamlandı.");
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("İşlem Yapılamadı Bilgileri Kontrol Edin");
                    }
                }
                else
                {
                    MessageBox.Show("Boş Alan Bırakmayınız.");
                }

                
            }
            
            

        }
        //yetki verme
        string adminyetki = "0", stokyetki = "0", satisyetki = "0", cariyetki = "0", indirimyetki = "0", raporyetki = "0";
        public void yetkiler()
        {
            if (cboxadminyetki.Checked)
            {
                adminyetki = "1";
            }
            else
            {
                adminyetki = "0";
            }
            if (cboxcariyetki.Checked)
            {
                cariyetki = "1";
            }
            else
            {
                cariyetki = "0";
            }
            if (cboxraporyetki.Checked)
            {
                raporyetki = "1";
            }
            else
            {
                raporyetki = "0";
            }
            if (cboxsatisyetki.Checked)
            {
                satisyetki = "1";
            }
            else
            {
                satisyetki = "0";
            }
            if (cboxstokyetki.Checked)
            {
                stokyetki = "1";
            }
            else
            {
                stokyetki = "0";
            }
            if (cboxurunyetki.Checked)
            {
                indirimyetki = "1";
            }
            else
            {
                indirimyetki = "0";
            }

        }

        private void adminguncelle_FormClosing(object sender, FormClosingEventArgs e)
        {
            adminguncelle a = new adminguncelle();
            a.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            adminraporlama adm = new adminraporlama();
            adm.Show();
        }
    }
}
