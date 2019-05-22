using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.IO;

namespace StokTakip
{
    public partial class satisekranı : Form
    {
        admingiris adm;
        public satisekranı(Form gelen)
        {
            adm = gelen as admingiris;
            InitializeComponent();
        }
        public satisekranı()
        {
            InitializeComponent();
        }

        string yapilan, tarih, saat;
        int ödenen;
        int fiyat, snc;
        int satmiktar, dbmiktar, guncelmiktar, kalanmiktarguncel;
        int urunmiktar, urunfiyat;
        string topkazanc;
        int f, m;
        int veriadet, satadet, kalan;
        int satis_id;
        // Satış ekranı tablosundaki verileri getirme ve datafridview e yazdırma
        public void bilgiler()
        {
            baglan.Open();
            DataTable tablo = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from SATIS_EKRANI", baglan);
            adp.Fill(tablo);
            dataGridView3.DataSource = tablo;
            satis_id = tablo.Rows.Count+1;
            baglan.Close();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");

        //Hareketler tablosuna yapılan işleri yazdırma
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

                MessageBox.Show("İşlem Hareketler Tablosuna Eklenemedi...");
            }
            
        }
        string yetkikontrol;
        public void satisyetkikontrol()
        {
            yetkikontrol = admingiris.kullanci;
            DataTable tb = new DataTable();
            baglan.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where kullanici_adi='" + yetkikontrol + "'", baglan);
            adp.Fill(tb);
            baglan.Close();
            if (tb.Rows[0][9].ToString() == "1")
            {
            }
            else if (tb.Rows[0][9].ToString() == "0")
            {
                MessageBox.Show("Satış Kontrol Yetkiniz Bulunmamaktadır.");
                button1.Enabled = false;
                button12.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button10.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button11.Enabled = false;
                dataGridView2.Enabled = false;
                dataGridView3.Enabled = false;
                flowLayoutPanel1.Enabled = false;
            }

        
        }
        //satış ekranı açıldığında bilgiler metodunu çağırma ve satış ekranına picturebox oluşturup ürünlere ait resimleri oluşturulan
        //pitureboxlara ekleme
        private void satisekranı_Load(object sender, EventArgs e)
        {
            
            satisyetkikontrol();
            bilgiler();
            txtsatisid.Text = satis_id.ToString();
            DataTable t = new DataTable();
            baglan.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from STOK_EKRANI",baglan);
            adp.Fill(t);
            baglan.Close();
            for (int i = 0; i < t.Rows.Count; i++)
            {
                PictureBox pcr = new PictureBox();
                pcr.Size = new Size(64, 57);
                pcr.Tag = i.ToString();
                pcr.SizeMode = PictureBoxSizeMode.StretchImage;
                pcr.BorderStyle = BorderStyle.FixedSingle;
                flowLayoutPanel1.Controls.Add(pcr);
                pcr.ImageLocation = Application.StartupPath + "/urunresim/" + t.Rows[i][10] + ".jpg";
                pcr.Click += new EventHandler(pcr_Click);
                pcr.MouseDown += new MouseEventHandler(pcr_MouseDown);
                pcr.MouseUp += new MouseEventHandler(pcr_MouseUp);
                pcr.MouseHover += new EventHandler(pcr_MouseHover);
            }
        }

        //Satış Ekle ekranındaki Oluşturulan resimlerin üzerine gelindiğinde o resime ait barkod no yazdırma
        void pcr_MouseHover(object sender, EventArgs e)
        {
            try
            {
                DataTable t = new DataTable();
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from STOK_EKRANI", baglan);
                adp.Fill(t);
                baglan.Close();
                PictureBox gelen = (sender as PictureBox);
                int a = Convert.ToInt32(gelen.Tag);
                toolTip1.SetToolTip(gelen, "" + t.Rows[a][10] + "");
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        //Satış Ekranındaki Resimlere Basılıp Çekildiği an da resmin eski boyutuna dönme işlemi
        void pcr_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox gelen = (sender as PictureBox);
            gelen.Size = new Size(64, 57);
        }

        //Satış Ekranında Resme Basıldığında Resmin Basılmış Süsü Verilmesi İçin Küçülme İşlemi Ve Basılan Resimdeki Ürün ' ün Adetini alma
        void pcr_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                PictureBox gelen = (sender as PictureBox);
                gelen.Size = new Size(60, 53);
                DataTable t = new DataTable();
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from STOK_EKRANI", baglan);
                adp.Fill(t);
                veriadet = Convert.ToInt32(t.Rows[Convert.ToInt16(gelen.Tag)][4].ToString());
                baglan.Close();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }


        // Satış Ekranıda ki Resime Tıklandığında O resimdeki Ürüne Ait Bilgilerin Yandaki Belirili TextBoxlara Yazdırma İşlemi
        void pcr_Click(object sender, EventArgs e)
        {
            foreach (Control a in panel1.Controls)
            {
                if (a is TextBox)
                {
                    a.Text = "";
                    txturunmiktar.Text = "0";
                    txttopurun.Text = "0";
                    txtbirimfiyat.Text = "0";
                    txttopfiyat.Text = "0";
                    txtodenenfiyat.Text = "0";
                    txtparaustu.Text = "0";
                    

                }
            }
            foreach (Control b in panel1.Controls)
            {
                if (b is ComboBox)
                {
                    b.Text = "";
                }
            }
            bilgiler();
            txtsatisid.Text = satis_id.ToString();
            try
            {
                DataTable t = new DataTable();
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from STOK_EKRANI", baglan);
                adp.Fill(t);
                baglan.Close();
                PictureBox gelen = (sender as PictureBox);
                txtbarkodno.Text = t.Rows[Convert.ToInt16(gelen.Tag)][10].ToString();
                txtbirimfiyat.Text = t.Rows[Convert.ToInt16(gelen.Tag)][8].ToString();
                txtsatistipi.Text = t.Rows[Convert.ToInt16(gelen.Tag)][3].ToString();
                txturunbilgileri.Text = t.Rows[Convert.ToInt16(gelen.Tag)][6].ToString();
                txturunmarka.Text = t.Rows[Convert.ToInt16(gelen.Tag)][5].ToString();
                txttopurun.Text = veriadet.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Bilgiler Getirilemedi Tekrar Deneyin...");
            }
            
        }
        
        //Satış Ekranında Seçilen Ürün'ün Adetini Satılan Adet İle Hesaplatma
        public void adethesap()
        {
            kalan = veriadet - satadet;
        }

        //Satış Ekranında Ürün Satıldıktan Sonra Kalan Adeti STOK EKRANI TAblosuna Yazdırma
        public void adetguncelle()
        {
            try
            {
                if (kalan == 0)
                {

                    MessageBox.Show("Ürün Adeti Tükenmiştir Satış İşlemi İçin Ürün Ekleyiniz");
                    File.Delete("urunresim/"+txtbarkodno.Text+".jpg");
                }
                
                    baglan.Open();
                    OleDbCommand guncelle = new OleDbCommand("UPDATE STOK_EKRANI SET urun_adet='" + kalan + "' where barkod_no='" + txtbarkodno.Text + "'", baglan);
                    guncelle.ExecuteNonQuery();
                    baglan.Close();
                
            }
            catch (Exception)
            {

                MessageBox.Show("Stok Adet Güncellenemedi...");
            }
            
        }

        // Satış Ekranında Satış Yapılması
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtsatisid.Text==""||txtbarkodno.Text==""||txtmusteriad.Text==""||cboxodemetipi.Text==""||txturunmiktar.Text==""||txtodenenfiyat.Text==""||txturunmarka.Text==""||txturunbilgileri.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız.");
            }
            else
            {
                try
                {
                    satadet = Convert.ToInt16(txturunmiktar.Text);
                    adethesap();
                    if (kalan < 0)
                    {
                        MessageBox.Show("Stoklarımızda Bukadar Miktarda Ürün Bulunmamaktadır.");
                    }
                    else
                    {

                        baglan.Open();
                        OleDbCommand ekle = new OleDbCommand("insert into SATIS_EKRANI(satis_id,barkod_no,musteri_adi,odeme_tipi,urun_miktari,birim_fiyat,toplam_fiyat,bilgi,tarih) values('" + txtsatisid.Text + "','" + txtbarkodno.Text + "','" + txtmusteriad.Text + "','" + cboxodemetipi.Text + "','" + txturunmiktar.Text + "','" + txtbirimfiyat.Text + "','" + txttopfiyat.Text + "','" + txturunmarka.Text + "','" + dateTimePicker1.Value.ToString() + "')", baglan);
                        ekle.ExecuteNonQuery();
                        baglan.Close();
                        adetguncelle();
                        yapilan = "Ürün Satışı Yapıldı";
                        tarih = DateTime.Now.ToShortDateString().ToString();
                        saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                        hareketler();
                        MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Satış Yapılamadı Bilgileri Kontrol Ediniz");
                }
            }
            
            
        }
        

        //Ödenen Fiyat Textbox'una Bir Fiyat Girildiği Anda Oluşan Olaylar
        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtodenenfiyat.Text == null || txtodenenfiyat.Text == "")
                {
                    txtodenenfiyat.Text = "0";
                    txtparaustu.Text = "";
                }
                else
                {
                    fiyat = Convert.ToInt32(txttopfiyat.Text);
                    ödenen = Convert.ToInt32(txtodenenfiyat.Text);
                    snc = ödenen - fiyat;
                    txtparaustu.Text = snc.ToString();
                }
                if (ödenen < fiyat)
                {
                    txtparaustu.BackColor = Color.Red;
                    txtparaustu.ForeColor = Color.White;

                }
                else
                {
                    txtparaustu.BackColor = Color.Lime;
                    txtparaustu.ForeColor = Color.White;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
           
            
        }

        //İsteğe Bağlı Müşterinin MAil Adresine Satış İle İlgili Bilgi Gönderme
        int mailsifre = 123456789;
        string posta = "akdagyazilim@outlook.com";
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.mail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential(posta, mailsifre.ToString());
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(posta, mailsifre.ToString());
                mail.To.Add(txtmailadres.Text.ToString());
                mail.Subject = txtkonu.Text.ToString();
                mail.IsBodyHtml = true;
                mail.Body = txtmesaj.Text.ToString();
                sc.Send(mail);
                yapilan = "Müşteriye Mail Gönderildi";
                tarih =DateTime.Now.ToShortDateString().ToString();
                saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                hareketler();
            }
            catch (Exception)
            {

                MessageBox.Show("Mail Yollanamadı");
            }
           
        }

        //Tüm Satış ların id'lerini liste Dizisine Atma İşlemi
        ArrayList liste = new ArrayList();
        public void satisidal()
        {
            liste.Clear();
            DataTable tbl = new DataTable();
            baglan.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from SATIS_EKRANI", baglan);
            adp.Fill(tbl);
            dataGridView3.DataSource = tbl;
            baglan.Close();
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                liste.Add(tbl.Rows[i][0].ToString());
            }
        }


        /*Satış Güncellenirken Stok Tablosundan Belirtilen Ürünün Miktarını alıp satış ekranı tablosundan satılan miktarı alıp topluyoruz
          Böylece Satış Yapılmadan Önceki Ürün Miktarına Yeniden Kavuşuyoruz ve Ürün Miktarımızdan Yeni Girilen Miktarı Çıkartıyoruz.
        */
        
        public void stkadetal()
        {
            DataTable tbl = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from STOK_EKRANI where barkod_no='" + txtbarkodnoguncel.Text + "'", baglan);
            adp.Fill(tbl);
            satmiktar = Convert.ToInt16(tbl.Rows[0][4]);
            baglan.Close();
        }
        public void gunceladethesap()
        {
            guncelmiktar=Convert.ToInt16(txturunmiktarguncel.Text);
            stkadetal();
            baglan.Open();
            DataTable tbl = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from SATIS_EKRANI where satis_id='"+txtsatisidguncel.Text+"'", baglan);
            adp.Fill(tbl);
            dbmiktar=Convert.ToInt16(tbl.Rows[0][4]);
            kalanmiktarguncel = (dbmiktar + satmiktar)-guncelmiktar;
            baglan.Close();
            
        
        }
        public void stkadetiguncelle()
        {
             baglan.Open();
             OleDbCommand guncelle = new OleDbCommand("update STOK_EKRANI set urun_adet='"+kalanmiktarguncel+"' where barkod_no='"+txtbarkodnoguncel.Text+"'",baglan);
             guncelle.ExecuteNonQuery();
             baglan.Close();
        }
        //Satış ekranı tablosundaki verileri belirtilen ürüne göre güncelleme 
        private void button5_Click(object sender, EventArgs e)
        {
            if (txtbarkodnoguncel.Text==""||txtbirimfiyatguncel.Text==""||txtmusteriadguncel.Text==""||txtsatisidguncel.Text==""||txttopfiyatguncel.Text==""||txturunmiktarguncel.Text==""||cboxodemetipguncel.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız.");
            }
            else
            {
                try
                {
                    gunceladethesap();
                    if (kalanmiktarguncel >= 0)
                    {
                        stkadetiguncelle();
                        gunceladethesap();
                        baglan.Open();
                        OleDbCommand guncelle = new OleDbCommand("update SATIS_EKRANI set barkod_no='" + txtbarkodnoguncel.Text + "' , musteri_adi='" + txtmusteriadguncel.Text + "' , odeme_tipi='" + cboxodemetipguncel.Text + "' , urun_miktari='" + txturunmiktarguncel.Text + "' , birim_fiyat='" + txtbirimfiyatguncel.Text + "' ,toplam_fiyat='" + txttopfiyatguncel.Text + "', bilgi='" + txturunbilgiguncel.Text + "' , tarih='" + dateTimePicker2.Value.ToString() + "' where satis_id='" + txtsatisidguncel.Text + "' ", baglan);
                        guncelle.ExecuteNonQuery();
                        baglan.Close();
                        yapilan = "Satış İşlemi Bilgileri Güncellendi";
                        tarih = DateTime.Now.ToShortDateString().ToString();
                        saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                        hareketler();
                        MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Stokta Bu kadar Ürün Yok.");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Satış Güncellenemedi Bilgileri Kontrol Ediniz...");
                }
            
            }
           
            
        }

        //Satış ekranı Tablosundaki Bilgileri Datagridview e yazdırmak için bilgiler metod'unu çağırma
        private void button1_Click(object sender, EventArgs e)
        {
            bilgiler();
        }

        //Satış ekranı tablosundaki Belirtilen ürüne ait bütün bilgileri silme
        private void button7_Click(object sender, EventArgs e)
        {
            if (txtsatisidsil.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    baglan.Open();
                    OleDbCommand sil = new OleDbCommand("delete from SATIS_EKRANI where satis_id='" + txtsatisidsil.Text + "'", baglan);
                    sil.ExecuteNonQuery();
                    baglan.Close();
                    yapilan = "Satış İşlemi Silindi";
                    tarih = DateTime.Now.ToShortDateString().ToString();
                    saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    hareketler();
                    MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception)
                {

                    MessageBox.Show("Bilgiler Silinemedi");
                }
            }
            
            
        }


        //Satış_id ye göre Bilgi Arama
        private void button9_Click(object sender, EventArgs e)
        {
            if (txtsatisidara.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    baglan.Open();
                    DataTable tb = new DataTable();
                    OleDbDataAdapter adp = new OleDbDataAdapter("select * from SATIS_EKRANI where satis_id='" + txtsatisidara.Text + "'", baglan);
                    adp.Fill(tb);
                    dataGridView2.DataSource = tb;
                    baglan.Close();
                    yapilan = "Satış id İle Satış Bilgisi Arandı";
                    tarih = DateTime.Now.ToShortDateString().ToString();
                    saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    hareketler();
                }
                catch (Exception)
                {

                    MessageBox.Show("Satış id'yi Kontrol Ediniz...");
                }
            }
            
           
        }


        //Müşteri adına göre bilgi arama
        private void button11_Click(object sender, EventArgs e)
        {
            if (txtmusteriadara.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    baglan.Open();
                    DataTable tb = new DataTable();
                    OleDbDataAdapter adp = new OleDbDataAdapter("select * from SATIS_EKRANI where musteri_adi='" + txtmusteriadara.Text + "'", baglan);
                    adp.Fill(tb);
                    dataGridView2.DataSource = tb;
                    baglan.Close();
                    yapilan = "Müşteri Adına Göre Satış Bilgisi Arandı";
                    tarih = DateTime.Now.ToShortDateString().ToString();
                    saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    hareketler();
                }
                catch (Exception)
                {

                    MessageBox.Show("Müşteri Adının Doğruluğundan Emin Olun");
                }
            }
            
            
        }


        //Satış ara ekranındaki textboxları temizleme
        private void button8_Click(object sender, EventArgs e)
        {
            txtsatisidara.Text = "";
            txtmusteriadara.Text = "";
        }


        
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            txturunmiktar.MaxLength = Convert.ToInt16(txttopurun.Text.Length);
            try
            {
                if (txturunmiktar.Text == "" || txturunmiktar.Text == null)
                {
                    txturunmiktar.Text = "0";
                }
                urunmiktar = Convert.ToInt32(txturunmiktar.Text);
                urunfiyat = Convert.ToInt32(txtbirimfiyat.Text);
                txttopfiyat.Text = (urunmiktar * urunfiyat).ToString();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtbirimfiyatguncel.Text == "" || txtbirimfiyatguncel.Text == null)
                {
                    txtbirimfiyatguncel.Text = "0";
                }
                if (txturunmiktarguncel.Text == "" || txturunmiktarguncel.Text == null)
                {
                    txturunmiktarguncel.Text = "0";
                }
                f = Convert.ToInt16(txtbirimfiyatguncel.Text);
                m = Convert.ToInt16(txturunmiktarguncel.Text);
                txttopfiyatguncel.Text = (f * m).ToString();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }


        //Satış ekle ekranındaki Textboxları Temizleme
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control a in panel1.Controls)
            {
                if (a is TextBox)
                {
                    a.Text = "";
                    txturunmiktar.Text = "0";
                    txttopurun.Text = "0";
                    txtbirimfiyat.Text = "0";
                    txttopfiyat.Text = "0";
                    txtodenenfiyat.Text = "0";
                    txtparaustu.Text = "0";
                }
            }
            foreach (Control b in panel1.Controls)
            {
                if (b is ComboBox)
                {
                    b.Text = "";
                }
            }
            
        }


        //Satış Füncelle Ekranındaki Textboxları Temizleme
        private void button4_Click(object sender, EventArgs e)
        {
          
            foreach (Control a in this.Controls)
            {
                if (a is TextBox)
                {
                    a.Text = "";
                    txturunmiktarguncel.Text = "0";
                    txtbirimfiyatguncel.Text = "0";
                    txttopfiyatguncel.Text = "0";
                }
            }
            foreach (Control b in this.Controls)
            {
                if (b is ComboBox)
                {
                    b.Text = "";
                }
            }
        }


        //Satış Sil Ekranındaki Textboxu Temizleme
        private void button6_Click(object sender, EventArgs e)
        {
            txtsatisidsil.Text = "";
        }

        private void satisekranı_FormClosing(object sender, FormClosingEventArgs e)
        {
            satisekranı sts = new satisekranı();
            sts.Close();
        }

        
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                DataTable tablo = new DataTable();
                OleDbDataAdapter adp = new OleDbDataAdapter("select sum(toplam_fiyat) from SATIS_EKRANI", baglan);
                adp.Fill(tablo);
                topkazanc = tablo.Rows[0][0].ToString();
                baglan.Close();
                MessageBox.Show("Toplam Kazanç : " + topkazanc + " TL", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                throw;
            }
            
        }


        //Belirlenen Textboxlara Sadece Rakam Girilmesi
        private void txturunmiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtodenenfiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtsatisid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtbarkodno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtsatisidguncel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtbarkodnoguncel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txturunmiktarguncel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtbirimfiyatguncel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txttopfiyatguncel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtsatisidsil_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtsatisidara_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtsatisidguncel_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                bilgilerilistele();
                for (int i = 0; i < tabl.Rows.Count; i++)
                {
                    if (tabl.Rows[i][0].ToString()==txtsatisidguncel.Text)
                    {
                        txtbarkodnoguncel.Text = tabl.Rows[i][1].ToString();
                        txtbirimfiyatguncel.Text = tabl.Rows[i][5].ToString();
                        txtmusteriadguncel.Text = tabl.Rows[i][2].ToString();
                        txturunbilgiguncel.Text = tabl.Rows[i][7].ToString();
                        txturunmiktarguncel.Text = tabl.Rows[i][4].ToString();
                        cboxodemetipguncel.Text = tabl.Rows[i][3].ToString();
                        dateTimePicker2.Text = tabl.Rows[i][8].ToString();
                    }
                }
                
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        DataTable tabl = new DataTable();
        public void bilgilerilistele()
        {
            tabl.Rows.Clear();
            baglan.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from SATIS_EKRANI", baglan);
            adp.Fill(tabl);
            baglan.Close();
        }
        private void txturunmiktarguncel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtbirimfiyatguncel.Text == "" || txtbirimfiyatguncel.Text == null)
                {
                    txtbirimfiyatguncel.Text = "0";
                }
                if (txturunmiktarguncel.Text == "" || txturunmiktarguncel.Text == null)
                {
                    txturunmiktarguncel.Text = "0";
                }
                f = Convert.ToInt16(txtbirimfiyatguncel.Text);
                m = Convert.ToInt16(txturunmiktarguncel.Text);
                txttopfiyatguncel.Text = (f * m).ToString();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }
}
