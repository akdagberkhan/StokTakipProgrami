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
    public partial class adminsil : Form
    {
        public adminsil()
        {
            InitializeComponent();
        }

       
        void adsoyad()
        {
            try
            {
                baglan.Open();
                OleDbCommand sil = new OleDbCommand("delete from ADMIN where ad='" + txtad.Text + "' and soyad='" + txtsoyad.Text + "'", baglan);
                sil.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Yetkili Kişi Sistemden Silinmiştir.");
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        void kulad()
        {
            try
            {
                baglan.Open();
                OleDbCommand sil = new OleDbCommand("delete from ADMIN where kullanici_adi='" + txtkulad.Text + "'", baglan);
                sil.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Yetkili Kişi Sistemden Silinmiştir.");
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
        void eposta()
        {
            try
            {
                baglan.Open();
                OleDbCommand sil = new OleDbCommand("delete from ADMIN where eposta='" + txteposta.Text + "' ", baglan);
                sil.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Yetkili Kişi Sistemden Silinmiştir.");
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        string yapilan, tarih, saat;
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
                
                throw;
            }
            
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (rbtnadsoyad.Checked)
                {
                    secilenislem = 0;
                    if (txtad.Text==""||txtsoyad.Text=="")
                    {
                        MessageBox.Show("Boş Alan Bırakmayınız");
                    }
                    else
                    {
                        veriler();
                        kulhatirlasil();
                        adsoyad();
                        yapilan = "YETKİLİ SİLİNDİ";
                        tarih = DateTime.Now.ToShortDateString().ToString();
                        saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                        hareketler();
                        MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    
                }
                if (rbtnkulad.Checked)
                {
                    secilenislem = 1;
                    if (txtkulad.Text=="")
                    {
                        MessageBox.Show("Boş Alan Bırakmayınız");
                    }
                    else
                    {
                        veriler();
                        kulhatirlasil();
                        kulad();
                        yapilan = "YETKİLİ SİLİNDİ";
                        tarih = DateTime.Now.ToShortDateString().ToString();
                        saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                        hareketler();
                        MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                   
                }
                if (rbtneposta.Checked)
                {
                    secilenislem = 2;
                    if (txteposta.Text=="")
                    {
                        MessageBox.Show("Boş Alan Bırakmayınız");
                    }
                    else
                    {
                        veriler();
                        kulhatirlasil();
                        eposta();
                        yapilan = "YETKİLİ SİLİNDİ";
                        tarih = DateTime.Now.ToShortDateString().ToString();
                        saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                        hareketler();
                        MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                   
                }
            }
            catch (Exception)
            {

                MessageBox.Show("İşlem Yapılamadı.");
            }
            
        }
        string gelenkullanici;
        private void adminsil_Load(object sender, EventArgs e)
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

                }
                else
                {
                    rbtnadsoyad.Enabled = false;
                    rbtnkulad.Enabled = false;
                    rbtneposta.Enabled = false;
                    button1.Enabled = false;
                    MessageBox.Show("Yetkili Kişi Silmek İçin Yetki Verilmiş Bir Kullanıcı İle Giriş Yapmak Zorundasınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception)
            {
                
                
            }
           
        }

        private void rbtnadsoyad_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void rbtnkulad_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void rbtneposta_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void adminsil_FormClosing(object sender, FormClosingEventArgs e)
        {
            adminsil adm = new adminsil();
            adm.Close();
         
        }

        int secilenislem;
        public void kulhatirlasil()
        {
            baglan.Open();
            if (secilenislem==0)
            {
               
                    OleDbCommand sil = new OleDbCommand("delete from KULLANICI_HATIRLA where kul_adi='"+kul_ad+"'",baglan);
                    sil.ExecuteNonQuery();
                
                
            }
            if (secilenislem==1)
            {
                
                    OleDbCommand sil = new OleDbCommand("delete from KULLANICI_HATIRLA where kul_adi='" + kul_ad + "'", baglan);
                    sil.ExecuteNonQuery();  
 
            }
            if (secilenislem == 2)
            {

                    OleDbCommand sil = new OleDbCommand("delete from KULLANICI_HATIRLA where kul_adi='" + kul_ad + "'", baglan);
                    sil.ExecuteNonQuery();

            }
            baglan.Close();
        }
        DataTable tablo = new DataTable();
        string ad, soyad, kul_ad,posta;
        public void veriler()
        {
            if (secilenislem==0)
            {
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where ad='" + txtad.Text + "' and soyad='" + txtsoyad.Text + "'", baglan);
                adp.Fill(tablo);
                baglan.Close();
                ad = tablo.Rows[0][0].ToString();
                soyad = tablo.Rows[0][1].ToString();
                kul_ad = tablo.Rows[0][2].ToString();
                posta = tablo.Rows[0][4].ToString();
            }
            if (secilenislem == 1)
            {
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where kullanici_adi='"+txtkulad.Text+"' ", baglan);
                adp.Fill(tablo);
                baglan.Close();
                ad = tablo.Rows[0][0].ToString();
                soyad = tablo.Rows[0][1].ToString();
                kul_ad = tablo.Rows[0][2].ToString();
                posta = tablo.Rows[0][4].ToString();
            }
            if (secilenislem == 2)
            {
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where eposta='"+txteposta.Text+"'", baglan);
                adp.Fill(tablo);
                baglan.Close();
                ad = tablo.Rows[0][0].ToString();
                soyad = tablo.Rows[0][1].ToString();
                kul_ad = tablo.Rows[0][2].ToString();
                posta = tablo.Rows[0][4].ToString();
            }
            
        }
    }
}
