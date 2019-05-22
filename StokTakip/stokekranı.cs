using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace StokTakip
{
    public partial class stokekranı : Form
    {
        admingiris adm;
        public stokekranı(Form gelen)
        {
            adm = gelen as admingiris;
            InitializeComponent();
        }
        public stokekranı()
        {
            InitializeComponent();
        }
        string yapilan,tarih,saat;
        string silinecek;
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");

        //Hareketler talosuna Yapılan İşi Ekleme 
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

        //Butona Basıldığında Açılan Pencereden Seçilen REsmi Picturebox a Aktarma
        private void button10_Click(object sender, EventArgs e)
        {     
              /* OpenFileDialog sec = new OpenFileDialog();
                sec.Filter = "Resim|* .jpg|Png|*.png";
                sec.ShowDialog();
                pictureBox1.ImageLocation = sec.FileName;
                sec.Dispose();*/
                openFileDialog1.Filter = "Resim|* .jpg";
                openFileDialog1.ShowDialog();
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                openFileDialog1.Dispose();
           
  
        }


        //Ürünresim kloserine ürün 'ün resmini ekleme Ve Stok Ekranı Tablosuna Ürün Ekleme
        private void button2_Click(object sender, EventArgs e)
        {
            brkdkontrol();
            if (brkdtablo.Rows.Count == 1)
            {
                MessageBox.Show("Bu Barkod No Veri Tabanında Kayıtlıdır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                if (txtbarkodno.Text == "" || cboxbarkodtip.Text == "" || cboxolcubirim.Text == "" || txturunad.Text == "" || txturunadet.Text == "" || txturunmarka.Text == "" || txturunhakkinda.Text == "" || txtalisfiyat.Text == "" || txtsatisfiyat1.Text == "" || txtsatisfiyat2.Text == "")
                {
                    MessageBox.Show("Boş Alan Bırakmayınız");
                }
                else
                {
                    try
                    {
                        pictureBox1.Image.Save(Application.StartupPath + "/urunresim/" + txtbarkodno.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch (Exception)
                    {

                        pictureBox1.ImageLocation = "#";
                    }
                    try
                    {
                        baglan.Open();
                        OleDbCommand ekle = new OleDbCommand("insert into STOK_EKRANI(barkod_no,barkod_tipi,urun_adi,olcu_birimi,urun_adet,urun_markasi,urun_hakkinda,alis_fiyat,satis_fiyat,satis_fiyat2,resim_ad) values('" + txtbarkodno.Text + "','" + cboxbarkodtip.Text + "','" + txturunad.Text + "','" + cboxolcubirim.Text + "','" + txturunadet.Text + "','" + txturunmarka.Text + "','" + txturunhakkinda.Text + "','" + txtalisfiyat.Text + "','" + txtsatisfiyat1.Text + "','" + txtsatisfiyat2.Text + "','" + txtbarkodno.Text + "')", baglan);
                        ekle.ExecuteNonQuery();
                        baglan.Close();
                        stokekranı stk = new stokekranı();
                        a.Refresh();
                        yapilan = "Ürün Kaydı Yapıldı";
                        tarih = DateTime.Now.ToShortDateString().ToString();
                        saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                        hareketler();
                        MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Boş Alan Bırakmayın Veya Bilgilerin Doğruluğunu Kontrol Ediniz...");
                    }
                }
            }
            
            
            
        }
        DataTable brkdtablo = new DataTable();
        public void brkdkontrol()
        {
            try
            {
                brkdtablo.Rows.Clear();
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from STOK_EKRANI where barkod_no='" + txtbarkodno.Text + "'", baglan);
                adp.Fill(brkdtablo);
                baglan.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }

        //İndirim İçin Yetki Kontrol
        string yetkikontrol;
        public void indirimyetkikontrol()
        {
            yetkikontrol = admingiris.kullanci;
            DataTable tb = new DataTable();
            baglan.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where kullanici_adi='"+yetkikontrol+"'", baglan);
            adp.Fill(tb);
            baglan.Close();
            if (tb.Rows[0][7].ToString() == "1")
            {
                button14.Enabled = true;
            }
            else if (tb.Rows[0][7].ToString() == "0")
            {
                button14.Enabled = false;
                dataGridView3.Enabled = false;
                panelbilgi.Visible = true;
                button13.Enabled = false;
            }
           
        }
        public void stokyetkikontrol()
        {
            yetkikontrol = admingiris.kullanci;
            DataTable tb = new DataTable();
            baglan.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where kullanici_adi='" + yetkikontrol + "'", baglan);
            adp.Fill(tb);
            baglan.Close();
            if (tb.Rows[0][6].ToString() == "1")
            {
            }
            else if (tb.Rows[0][6].ToString() == "0")
            {
                MessageBox.Show("Stok Kontrol Yetkiniz Bulunmamaktadır.");
                button1.Enabled = false;
                dataGridView1.Enabled = false;
                button10.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button11.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            }

        }
        //Stok Ekranı Penceresi Açıldığı anda Stok Ekranı Tablosundaki Verileri datagridview e yazdırma
        int renklendirme;
        private void stokekranı_Load(object sender, EventArgs e)
        {
            DataTable tb = new DataTable();
            baglan.Open();  
            OleDbCommand sil = new OleDbCommand("delete from STOK_EKRANI where urun_adet="+0+"",baglan);
            sil.ExecuteNonQuery();
            OleDbDataAdapter veri = new OleDbDataAdapter("select * from STOK_EKRANI",baglan);
            veri.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView3.DataSource = tb;
            baglan.Close();
            indirimyetkikontrol();
            stokyetkikontrol();
           
            
        }

        // Stok Ekranı Tablosundaki Verileri datagridview e yazdırma
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable tb = new DataTable();
            baglan.Open();
            OleDbDataAdapter veri = new OleDbDataAdapter("select * from STOK_EKRANI", baglan);
            veri.Fill(tb);
            dataGridView1.DataSource = tb;
            baglan.Close();
        }


        //Belirtilen Ürünün Fotoğrafını Sili Yeni Fotoğrafını Kaydetmek Ve Bilgilerini Gücellemek 
        private void button5_Click(object sender, EventArgs e)
        {
            if (txtsatisfiyat2guncel.Text==""||txturunadetguncel.Text==""||txturunadiguncel.Text==""||txturunhakkindaguncel.Text==""||txturunmarkaguncel.Text==""||cboxbarkodtipguncel.Text==""||cboxolcubirimguncel.Text==""||txtsatisfiyatguncel.Text==""||txtbarkodnoguncel.Text==""||txtbarkodnoguncelle.Text==""||txtalisfiyatguncel.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                silinecek = Application.StartupPath + "/urunresim/" + txtbarkodnoguncelle.Text + ".jpg";
                File.Delete(silinecek);
                try
                {
                    pictureBox2.Image.Save(Application.StartupPath + "/urunresim/" + txtbarkodnoguncel.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception)
                {

                    pictureBox1.ImageLocation = "#";
                }
                try
                {
                    baglan.Open();
                    OleDbCommand guncelle = new OleDbCommand("update STOK_EKRANI set barkod_no='" + txtbarkodnoguncel.Text + "',barkod_tipi='" + cboxbarkodtipguncel.Text + "',urun_adi='" + txturunadiguncel.Text + "',olcu_birimi='" + cboxolcubirimguncel.Text + "',urun_adet='" + txturunadetguncel.Text + "',urun_markasi='" + txturunmarkaguncel.Text + "',urun_hakkinda='" + txturunhakkindaguncel.Text + "',alis_fiyat='" + txtalisfiyatguncel.Text + "',satis_fiyat='" + txtsatisfiyatguncel.Text + "',satis_fiyat2='" + txtsatisfiyat2guncel.Text + "' ,resim_ad='" + txtbarkodnoguncel.Text + "' where barkod_no='" + txtbarkodnoguncelle.Text + "'", baglan);
                    guncelle.ExecuteNonQuery();
                    baglan.Close();
                    yapilan = "Ürün Bilgileri Güncellendi";
                    tarih = DateTime.Now.ToShortDateString().ToString();
                    saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    hareketler();
                    MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception)
                {

                    MessageBox.Show("Bilgiler Güncelenemedi Lütfen Bilgileri Kontrol Edin");
                }
            
            }
            
        }


        //Stok Güncelle Ekranında Resim sEçme ve Pictureboxa yazdırma
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog sec = new OpenFileDialog();
                sec.Filter = "Resim|* .jpg|Png|*.png";
                sec.ShowDialog();
                pictureBox2.ImageLocation = sec.FileName;
                sec.Dispose();

            }
            catch (Exception)
            {

            }
            
        }

        //Belirtilen Ürüne Ait resmi Ve bilgileri Silme
        
        private void button7_Click(object sender, EventArgs e)
        {
            if (txtbarkodnosil.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    silinecek = Application.StartupPath + "/urunresim/" + txtbarkodnosil.Text + ".jpg";
                    baglan.Open();
                    OleDbCommand sil = new OleDbCommand("delete from STOK_EKRANI where barkod_no='" + txtbarkodnosil.Text + "'", baglan);
                    sil.ExecuteNonQuery();
                    baglan.Close();
                    File.Delete(silinecek);
                    yapilan = "Ürün Silindi";
                    tarih = DateTime.Now.ToShortDateString().ToString();
                    saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    hareketler();
                    MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception)
                {

                    MessageBox.Show("Dosyalar Silinmedi Bilgileri Kontrol Ediniz.");
                }
            }
            
            
        }


        //Ürün Araması 
        private void button9_Click(object sender, EventArgs e)
        {
            if (txtbarkodnoara.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    DataTable tb = new DataTable();
                    baglan.Open();
                    OleDbDataAdapter adp = new OleDbDataAdapter("select * from STOK_EKRANI where barkod_no='" + txtbarkodnoara.Text + "'", baglan);
                    adp.Fill(tb);
                    dataGridView2.DataSource = tb;
                    baglan.Close();
                    pictureBox3.ImageLocation = Application.StartupPath + "/urunresim/" + txtbarkodnoara.Text + ".jpg";
                    yapilan = "Barkod No Göre Ürün Araması Yapıldı";
                    tarih = DateTime.Now.ToShortDateString().ToString();
                    saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    hareketler();
                }
                catch (Exception)
                {

                    MessageBox.Show("Ürün Bulunamadı Bilgileri Kontrol Ediniz...");
                }
            
            }
            
        }

        private void stokekranı_FormClosing(object sender, FormClosingEventArgs e)
        {
            stokekranı stk = new stokekranı();
            stk.Close();
            
        }

        //stok ekle alanındaki tüm textboxları ve comboboxları temizleme ve picturebox'u temizleme
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control a in groupBox1.Controls)
            {
                if (a is TextBox)
                {
                    a.Text = "";
                }
                if (a is ComboBox)
                {
                    a.Text = "";
                }
            }
            foreach (Control b in groupBox2.Controls)
            {
                if (b is ComboBox )
                {
                    b.Text = "";
                }
                if (b is TextBox)
                {
                    b.Text = "";
                }
            }
            txturunmarka.Text = "Belirsiz(Bilinmiyor)";
            pictureBox1.ImageLocation = "";
        }


        //Belirlenen Textboxlara sadece sayı girebilme komutu.
        private void txtbarkodno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txturunadet_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtalisfiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtsatisfiyat1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtsatisfiyat2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtbarkodnoguncelle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtbarkodnoguncel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txturunadetguncel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtalisfiyatguncel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtsatisfiyatguncel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtsatisfiyat2guncel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtbarkodnosil_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtbarkodnoara_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }



        //stok güncelle alanındaki tüm textboxları ve comboboxları temizleme ve pictureboxu temizleme
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control a in groupBox4.Controls)
            {
                if (a is TextBox)
                {
                    a.Text = "";
                }
                if (a is ComboBox)
                {
                    a.Text = "";
                }
            }
            foreach (Control b in groupBox3.Controls)
            {
                if (b is TextBox)
                {
                    b.Text = "";
                }
               
            }
            pictureBox2.ImageLocation = "";
            txturunmarkaguncel.Text = "Belirsiz(Bilinmiyor)";
            txtbarkodnoguncel.Text = "";

        }


        // Stok Sil Ekranındaki Textboxu Temizleme
        private void button6_Click(object sender, EventArgs e)
        {
            txtbarkodnosil.Text = "";
        }


        //Stok Ara Ekranındaki Textboxu ve Picturebox'u temizler
        private void button8_Click(object sender, EventArgs e)
        {
            txtbarkodnoara.Text = "";
            pictureBox3.ImageLocation = "";
        }


        //Stok Ekranı Alanındaki İndirim Yapmak İçin seçilen ürünün barkodno'sunu texboxa yazdırma ve % hesap
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int satir = dataGridView3.CurrentRow.Index;
            txtbarkodnoindirim.Text = dataGridView3.Rows[satir].Cells["barkod_no"].Value.ToString();
        }
        public void stkfiyatal()
        {
            try
            {
                DataTable tb = new DataTable();
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from STOK_EKRANI where barkod_no='" + txtbarkodnoindirim.Text + "'", baglan);
                adp.Fill(tb);
                baglan.Close();
                stkfiyat = Convert.ToInt16(tb.Rows[0][8]);
            }
            catch (Exception)
            {
                
                throw;
            }
            
            
            
        }
        int stkfiyat, indirimlifiyat,indirim;
        private void button14_Click(object sender, EventArgs e)
        {
            if (txtbarkodnoindirim.Text==""||txtindirim.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    stkfiyatal();
                    indirim = Convert.ToInt16(txtindirim.Text);
                    indirimlifiyat = stkfiyat - ((stkfiyat * indirim) / 100);
                    baglan.Open();
                    OleDbCommand guncelle = new OleDbCommand("update STOK_EKRANI set satis_fiyat=" + indirimlifiyat + " where barkod_no='" + txtbarkodnoindirim.Text + "'", baglan);
                    guncelle.ExecuteNonQuery();
                    baglan.Close();
                    listele();
                }
                catch (Exception)
                {

                    MessageBox.Show("İşlem Tamamlanamadı Bilgileri Kontrol Ediniz");
                }
            
            }
            
        }
        public void listele()
        {
            DataTable tb = new DataTable();
            baglan.Open();
            OleDbDataAdapter veri = new OleDbDataAdapter("select * from STOK_EKRANI", baglan);
            veri.Fill(tb);
            dataGridView3.DataSource = tb;
            baglan.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            panelbilgi.Visible = false;
        }

        private void txtbarkodnoguncelle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bilgilerilistele();
                for (int i = 0; i < tabl.Rows.Count; i++)
                {
                    if (tabl.Rows[i][0].ToString() == txtbarkodnoguncelle.Text)
                    {
                        txtbarkodnoguncel.Text = tabl.Rows[i][0].ToString();
                        txtalisfiyatguncel.Text = tabl.Rows[i][7].ToString();
                        txtsatisfiyat2guncel.Text = tabl.Rows[i][9].ToString();
                        txtsatisfiyatguncel.Text = tabl.Rows[i][8].ToString();
                        txturunadetguncel.Text = tabl.Rows[i][4].ToString();
                        txturunadiguncel.Text = tabl.Rows[i][2].ToString();
                        txturunhakkindaguncel.Text = tabl.Rows[i][6].ToString();
                        txturunmarkaguncel.Text = tabl.Rows[i][5].ToString();
                        cboxbarkodtipguncel.Text = tabl.Rows[i][1].ToString();
                        cboxolcubirimguncel.Text = tabl.Rows[i][3].ToString();
                        pictureBox2.ImageLocation = "urunresim/" + tabl.Rows[i][10].ToString() + ".jpg";
                    }
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            
        } 
        DataTable tabl = new DataTable();

        private void txtbarkodno_TextChanged(object sender, EventArgs e)
        {

        }

        public void bilgilerilistele()
        {
           
            baglan.Open();
            OleDbDataAdapter veri = new OleDbDataAdapter("select * from STOK_EKRANI", baglan);
            veri.Fill(tabl);
            baglan.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            txtbarkodnoindirim.Text = "";
            txtindirim.Text = ""; 
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            int k = 0;
            try
            {

                DataTable tb = new DataTable();
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from STOK_EKRANI", baglan);
                adp.Fill(tb);
                baglan.Close();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (txtbarkodno.Text == tb.Rows[i][0].ToString())
                    {
                        MessageBox.Show("Bu Barkod No Stoklarımızda Eklidir.");
                        k = 1;
                    }


                }
                if (k == 0)
                {
                    MessageBox.Show("Kullanılabilir Barkod No");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void label41_Click(object sender, EventArgs e)
        {
            int k = 0;
            try
            {

                DataTable tb = new DataTable();
                baglan.Open();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from STOK_EKRANI", baglan);
                adp.Fill(tb);
                baglan.Close();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (txtbarkodno.Text == tb.Rows[i][0].ToString())
                    {
                        MessageBox.Show("Bu Barkod No Stoklarımızda Eklidir.");
                        k = 1;
                    }


                }
                if (k == 0)
                {
                    MessageBox.Show("Kullanılabilir Barkod No");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }
}
