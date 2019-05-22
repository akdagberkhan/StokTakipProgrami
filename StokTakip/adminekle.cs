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
    public partial class adminekle : Form
    {
        admingiris admgir;
        public adminekle(Form gelen)
        {
            admgir = gelen as admingiris;
            InitializeComponent();
        }
        public adminekle()
        {
            InitializeComponent();
        }
        string cinsiyet,yapilan,tarih,saat;
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");

        //  Hareketler Tablosuna Yapılan İşlem Kaydetme    
        public void hareketler()
        {
            try
            {
                baglan.Open();
                OleDbCommand ekle = new OleDbCommand("insert into HAREKETLER(YAPILAN,TARIH,SAAT) values('" + yapilan + "','" + tarih + "','" + saat + "')", baglan);
                ekle.ExecuteNonQuery();
                baglan.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("İşlem Hareketler Tablosuna Eklenemedi");
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
        //Admin Tablosuna Yeni Admin Ekleme
        private void button1_Click(object sender, EventArgs e)
        {
            kuladkontrol();
            if (kuladtablo.Rows.Count==1)
            {
                MessageBox.Show("Böyle Bir Kullanıcı Kayıtlıdır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                if (txtad.Text == "" || txteposta.Text == "" || txtkulad.Text == "" || txtsifre.Text == "" || txtsoyad.Text == "" || rbtnbayan.Checked != true && rbtnerkek.Checked != true)
                {
                    MessageBox.Show("Boş Alan Bırakmayınız");
                }
                else
                {
                    try
                    {
                        yetkiler();
                        if (rbtnerkek.Checked)
                        {
                            cinsiyet = "erkek";
                        }
                        else if (rbtnbayan.Checked)
                        {
                            cinsiyet = "bayan";
                        }
                        else
                        {
                            cinsiyet = " ";
                        }
                        baglan.Open();
                        OleDbCommand ekle = new OleDbCommand("insert into ADMIN(ad,soyad,kullanici_adi,sifre,eposta,cinsiyet,stok_yetki,indirim_yetki,carihesap_yetki,satis_yetki,admin_yetki,rapor_yetki) values('" + txtad.Text + "','" + txtsoyad.Text + "','" + txtkulad.Text + "','" + txtsifre.Text + "','" + txteposta.Text + "','" + cinsiyet + "','" + stokyetki + "','" + indirimyetki + "','" + cariyetki + "','" + satisyetki + "','" + adminyetki + "','" + raporyetki + "')", baglan);
                        ekle.ExecuteNonQuery();
                        baglan.Close();
                        yapilan = "YETKİLİ EKLENDİ";
                        tarih = DateTime.Now.ToShortDateString().ToString();
                        saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                        hareketler();
                        MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            
        }

        private void adminekle_FormClosing(object sender, FormClosingEventArgs e)
        {
            adminekle admekle = new adminekle();
            admekle.Close();
         
        }
// Şifre Textboxundaki yazıyı Passwordchar özelliği ile görünür yapma yada * yapma işlemi
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            txtsifre.PasswordChar = '\0';
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            txtsifre.PasswordChar = '*';
        }


        //Yetki Verme işlemi için giriş formundan veri çekme
        string gelenkullanici;
        private void adminekle_Load(object sender, EventArgs e)
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
                    linkLabel1.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                    linkLabel1.Enabled = false;
                    MessageBox.Show("Yetkili Kişi Eklemek İçin Yetki Verilmiş Bir Kullanıcı İle Giriş Yapmak Zorundasınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception)
            {

                
            }
        }
        DataTable kuladtablo = new DataTable();
        public void kuladkontrol()
        {
            try
            {
                kuladtablo.Rows.Clear();
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where kullanici_adi='" + txtkulad.Text + "'", baglan);
                adp.Fill(kuladtablo);
                baglan.Close();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            adminraporlama adm = new adminraporlama();
            adm.Show();
        }
    }
}
